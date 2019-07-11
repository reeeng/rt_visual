// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "RtGameMode.h"
#include "RtHUD.h"
#include "RtGameState.h"
#include "RtRestSvc.h"
#include "RtItemManager.h"
#include "UObject/ConstructorHelpers.h"
#include "TimerManager.h"
#include "Engine/World.h"

ARtGameMode::ARtGameMode()
	: Super()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnClassFinder(TEXT("/Game/FirstPersonCPP/Blueprints/FirstPersonCharacter"));
	DefaultPawnClass = PlayerPawnClassFinder.Class;

	// use our custom HUD class
	HUDClass = ARtHUD::StaticClass();

	GameStateClass = ARtGameState::StaticClass();
}

void ARtGameMode::InitGame(const FString& MapName, const FString& Options, FString& ErrorMessage)
{
	Super::InitGame(MapName, Options, ErrorMessage);
	SpawnGameplayActors();
}

void ARtGameMode::PreInitializeComponents()
{
	Super::PreInitializeComponents();
	ARtGameState* RtGameState = GetGameState<ARtGameState>();
	
	if(RtGameState)
	{
		GetWorldTimerManager().SetTimer(TimerHandle_RefreshItems, this, &ARtGameMode::RefreshItems, RtGameState->GetPollRate(), true);
	}
	
}

void ARtGameMode::SpawnGameplayActors()
{
	RESTService = GetWorld()->SpawnActor<ARtRestSvc>();
	ItemManager = GetWorld()->SpawnActor<ARtItemManager>(GameStateClass);
}

void ARtGameMode::RefreshItems()
{
	RESTService->GetItemInfo();
}

void ARtGameMode::OnRefreshItems(TArray<FRtItemInfo> Items)
{
	ARtGameState* RtGameState = GetGameState<ARtGameState>();

	if (RtGameState)
	{
		RtGameState->SetItems(Items);
	}

	ItemManager->RefreshItems();
}
