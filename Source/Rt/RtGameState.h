// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameStateBase.h"
#include "RtTypes.h"
#include "RtGameState.generated.h"

/**
 * 
 */
UCLASS()
class RT_API ARtGameState : public AGameStateBase
{
	GENERATED_BODY()

private:
	TArray<FRtItemInfo> Items;

	UPROPERTY(EditDefaultsOnly, Category = "API")
	float PollRate = 5.0f;

public:
	float GetPollRate();
	void SetItems(TArray<FRtItemInfo> NewItems);
};
