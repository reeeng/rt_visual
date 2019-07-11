// Fill out your copyright notice in the Description page of Project Settings.


#include "RtItemManager.h"
#include "RtGameState.h"
#include "Engine/World.h"


// Sets default values
ARtItemManager::ARtItemManager()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void ARtItemManager::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ARtItemManager::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

void ARtItemManager::RefreshItems()
{
	ARtGameState* gameState = GetWorld()->GetGameState<ARtGameState>();

	if(gameState)
	{
		TArray<FRtItemInfo> Items = gameState->GetItems();

		for(FRtItemInfo Item : Items)
		{
			OnRefreshItemBP(Item);
		}
	}
}

