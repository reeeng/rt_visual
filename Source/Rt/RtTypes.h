#pragma once
#include "Vector.h"
#include "RtTypes.generated.h"

USTRUCT()
struct FRtItemInfo
{
	GENERATED_BODY();

	int ID;

	FVector Position;
	FVector Size;
};