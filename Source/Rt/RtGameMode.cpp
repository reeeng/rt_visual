// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "RtGameMode.h"
#include "RtHUD.h"
#include "RtCharacter.h"
#include "RtGameState.h"
#include "UObject/ConstructorHelpers.h"

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
