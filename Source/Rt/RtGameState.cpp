// Fill out your copyright notice in the Description page of Project Settings.


#include "RtGameState.h"

float ARtGameState::GetPollRate()
{
	return PollRate;
}

FString ARtGameState::GetAPIBaseUrl()
{
	return APIBaseUrl;
}

void ARtGameState::SetItems(TArray<FRtItemInfo> NewItems)
{
	Items = NewItems;
}
