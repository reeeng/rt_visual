// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "RtItemManager.h"
#include "RtGameMode.generated.h"

class ARtRestSvc;
struct FRtItemInfo;

UCLASS(minimalapi)
class ARtGameMode : public AGameModeBase
{
	GENERATED_BODY()

public:
	ARtGameMode();
	void OnRefreshItems(TArray<FRtItemInfo> Items);
protected:
	void InitGame(const FString& MapName, const FString& Options, FString& ErrorMessage) override;
	void PreInitializeComponents() override;
	void SpawnGameplayActors();
	void RefreshItems();

	FTimerHandle TimerHandle_RefreshItems;
	ARtRestSvc* RESTService;

	UPROPERTY(EditDefaultsOnly)
	TSubclassOf<ARtItemManager> ItemManagerClass = ARtItemManager::StaticClass();
	ARtItemManager* ItemManager;

};



