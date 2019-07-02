// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "RtGameMode.h"
#include "RtHUD.h"
#include "RtCharacter.h"
#include "RtGameState.h"
#include "UObject/ConstructorHelpers.h"
#include "TimerManager.h"

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

void ARtGameMode::PreInitializeComponents()
{
	ARtGameState* RtGameState = GetGameState<ARtGameState>();
	
	if(RtGameState)
	{
		GetWorldTimerManager().SetTimer(TimerHandle_RefreshItems, this, &ARtGameMode::RefreshItems, RtGameState->GetPollRate(), true);
	}
	
}

void ARtGameMode::RefreshItems()
{
}
