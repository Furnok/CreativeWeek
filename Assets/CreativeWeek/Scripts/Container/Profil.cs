using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProfilState
{
    Neutral,
    Happy,
    Angry
}

public enum ProfilType
{
    Street,
    Rich,
    Babos
}

public enum DrinkPreference
{
    None,
    Alcohol,
    Water,
    Soda
}

public enum DietType
{
    None,
    Carnivore,
    Vegetarian,
    Vegan
}

public enum IntoleranceType
{
    Lactose,
    Gluten,
    Fructose,
    NutsFruits
}

public enum BillSeparation
{
    None,
    Full,
    FiftyFifty,
    DontPay
}

[System.Serializable]
public struct ProfilStateDictionary
{
    public ProfilState State;
    public Sprite Sprite;
}

[System.Serializable]
public class Profil
{
    public int Age;
    public string Name;
    public ProfilType ProfilType;
    public ProfilState CurrentProfilState;
    public List<ProfilStateDictionary> ListProfilStates;
    public string Description;
    //public List<ItemUseEffect> ItemsWantedAndUnwanted;
    [HideInInspector] public DietType DietType;
    [HideInInspector] public DrinkPreference DrinkPreference;
    [HideInInspector] public List<IntoleranceType> Intolerances = new List<IntoleranceType>();
    [HideInInspector] public BillSeparation BillSeparation;

}
