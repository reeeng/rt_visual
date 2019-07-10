// Fill out your copyright notice in the Description page of Project Settings.


#include "RtRestSvc.h"
#include "RtGameMode.h"
#include "RtGameState.h"
#include "ModuleManager.h"
#include "HttpModule.h"
#include "IHttpResponse.h"
#include "JsonUtilities/Public/JsonUtilities.h"
#include "Engine/World.h"

// Sets default values
ARtRestSvc::ARtRestSvc()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = false;
	Http = &FModuleManager::LoadModuleChecked<FHttpModule>("HTTP");

}

// Called when the game starts or when spawned
void ARtRestSvc::BeginPlay()
{
	Super::BeginPlay();
	ARtGameState* GameState = GetWorld()->GetGameState<ARtGameState>();

	if (GameState)
	{
		APIBaseUrl = GameState->GetAPIBaseUrl();
	}
}

TSharedRef<IHttpRequest> ARtRestSvc::RequestWithRoute(FString Subroute)
{
	TSharedRef<class IHttpRequest> HttpRequest = Http->CreateRequest();
	HttpRequest->SetURL(APIBaseUrl + Subroute);
	HttpRequest->SetHeader(TEXT("User-Agent"), TEXT("X-UnrealEngine-Agent"));
	HttpRequest->SetHeader(TEXT("Content-Type"), TEXT("application/json"));
	HttpRequest->SetHeader(TEXT("Accepts"), TEXT("application/json"));
	return HttpRequest;
}

TSharedRef<IHttpRequest> ARtRestSvc::GetRequest(FString Subroute)
{
	TSharedRef<class IHttpRequest> HttpRequest = RequestWithRoute(Subroute);
	HttpRequest->SetVerb("GET");
	return HttpRequest;
}

TSharedRef<IHttpRequest> ARtRestSvc::PostRequest(FString Subroute, FString ContentJsonString)
{
	TSharedRef<class IHttpRequest> HttpRequest = RequestWithRoute(Subroute);
	HttpRequest->SetVerb("POST");
	HttpRequest->SetContentAsString(ContentJsonString);
	return HttpRequest;
}

void ARtRestSvc::Send(TSharedRef<IHttpRequest>& Request)
{
	Request->ProcessRequest();
}

bool ARtRestSvc::ResponseIsValid(FHttpResponsePtr Response, bool bWasSuccessful)
{
	if ((!bWasSuccessful) || (!Response.IsValid()))
		return false;

	if (EHttpResponseCodes::IsOk(Response->GetResponseCode()))
		return true;

	UE_LOG(LogTemp, Warning, TEXT("Http Response returned error code: %d"), Response->GetResponseCode());
	return false;
}

void ARtRestSvc::OnGetItemInfo(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful)
{
	if(ResponseIsValid(Response, bWasSuccessful))
	{	
		TArray<FRtItemInfo> Items;
		FJsonObjectConverter::JsonArrayStringToUStruct<FRtItemInfo>(Response->GetContentAsString(), &Items, 0, 0);

		ARtGameMode* GameMode = GetWorld()->GetAuthGameMode<ARtGameMode>();

		if(GameMode)
		{
			GameMode->OnRefreshItems(Items);
		}
	}
}

void ARtRestSvc::GetItemInfo()
{
	// Get status update on all items
	TSharedRef<IHttpRequest> Request = GetRequest("item");
	Request->OnProcessRequestComplete().BindUObject(this, &ARtRestSvc::OnGetItemInfo);
	Send(Request);
}