// Fill out your copyright notice in the Description page of Project Settings.


#include "RtRestSvc.h"
#include "ModuleManager.h"
#include "HttpModule.h"
#include "IHttpResponse.h"

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

// Called every frame
void ARtRestSvc::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

