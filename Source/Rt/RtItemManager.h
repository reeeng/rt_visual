// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "RtTypes.h"
#include "RtItemManager.generated.h"

UCLASS()
class RT_API ARtItemManager : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	ARtItemManager();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

	void RefreshItems();

	UFUNCTION(BlueprintImplementableEvent, Category = "Items")
	void OnRefreshItemBP(FRtItemInfo Item);

};
