#pragma once
#include "Vector.h"
#include "RtTypes.generated.h"

USTRUCT(BlueprintType)
struct FRtItemInfo
{
	GENERATED_BODY();

	UPROPERTY(BlueprintReadWrite)
	int ID;
	UPROPERTY(BlueprintReadWrite)
	FVector Position;
	UPROPERTY(BlueprintReadWrite)
	FVector Size;
};