using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_ProfilGenerator : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;
    [SerializeField] SSO_ListProfile _ssoListProfile;
    [SerializeField] SSO_MaxIntoleranceType _maxIntoleranceType;
    [SerializeField] RSE_OnProfilCreate _rseOnProfilCreate;


    private void Start()
    {
        CreateProfil();
    }

    void CreateProfil()
    {
        
        var profil = GetRandomItem(_ssoListProfile.Value);

        while(profil.DietType == DietType.None)
        {
            profil.DietType = GetRandomEnumValue<DietType>();
        }

        while(profil.DrinkPreference == DrinkPreference.None)
        {
            profil.DrinkPreference = GetRandomEnumValue<DrinkPreference>();
        }

        while (profil.BillSeparation == BillSeparation.None)
        {
            profil.BillSeparation = GetRandomEnumValue<BillSeparation>();
        }

        var randomNumberOfIntolerances = UnityEngine.Random.Range(1, _maxIntoleranceType.Value);
        profil.Intolerances = GetUniqueRandomEnumValues<IntoleranceType>(randomNumberOfIntolerances);
        _rsoCurrentProfile.Value = profil;
        _rseOnProfilCreate.RaiseEvent();

    }


    T GetRandomItem<T>(List<T> list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }

    T GetRandomEnumValue<T>() /*where T : Enum */
    {
        Array values = Enum.GetValues(typeof(T));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(randomIndex);
    }
   
    List<T> GetUniqueRandomEnumValues<T>(int count)
    {
        Array values = Enum.GetValues(typeof(T));

        List<T> uniqueValues = new List<T>();

        if (count > values.Length)
        {
            throw new ArgumentException($"Impossible de sélectionner {count} valeurs uniques. L'enum ne contient que {values.Length} valeurs.");
        }

        while (uniqueValues.Count < count)
        {
            T randomValue = (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));

            if (!uniqueValues.Contains(randomValue))
            {
                uniqueValues.Add(randomValue);
            }
        }

        return uniqueValues;
    }   
}
