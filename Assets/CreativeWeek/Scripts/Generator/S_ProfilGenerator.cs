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


    private void Start()
    {
        
    }

    void CreateProfil()
    {
        var profil = GetRandomItem(_ssoListProfile.Value);
        profil.DietType = GetRandomEnumValue<DietType>();
        profil.DrinkPreference = GetRandomEnumValue<DrinkPreference>();
        profil.BillSeparation = GetRandomEnumValue<BillSeparation>();
        var randomNumberOfIntolerances = UnityEngine.Random.Range(0, _maxIntoleranceType.Value);
        profil.Intolerances = GetUniqueRandomEnumValues<IntoleranceType>(randomNumberOfIntolerances);
        _rsoCurrentProfile.Value = profil;
    }


    T GetRandomItem<T>(List<T> list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }

    T GetRandomEnumValue<T>()
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
