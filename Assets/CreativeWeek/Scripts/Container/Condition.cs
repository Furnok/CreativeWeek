using System.Collections.Generic;
using System;

[Serializable]
public struct Condition
{
    public DrinkPreference DrinkPreference;
    public DietType DietType;
    public BillSeparation BillSeparation;
    public List<IntoleranceType> IntoleranceType;
}
