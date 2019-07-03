// Fill out your copyright notice in the Description page of Project Settings.


#include "RtGameState.h"

float ARtGameState::GetPollRate()
{
	return PollRate;
}

void ARtGameState::SetItems(TArray<FRtItemInfo> NewItems)
{
	Items = NewItems;
}
