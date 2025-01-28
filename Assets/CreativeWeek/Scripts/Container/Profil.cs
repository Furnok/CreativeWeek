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
    Alcohol,
    Water,
    Soda
}

public enum DietType
{
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
    //public ProfilState CurrentProfilState;
    public List<ProfilStateDictionary> ListProfilStates;
    public string Description;
    public List<ItemUseEffect> ItemsWantedAndUnwanted;
    public DietType DietType;
    public DrinkPreference DrinkPreference;
    public List<IntoleranceType> Intolerances;

}
