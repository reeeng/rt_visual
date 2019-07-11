#pragma once
#include "Vector.h"
#include "RtTypes.generated.h"

USTRUCT(BlueprintType)
struct FRtItemInfo
{
	GENERATED_BODY();

	UPROPERTY()
	int ID;
	UPROPERTY()
	FVector Position;
	UPROPERTY()
	FVector Size;
};