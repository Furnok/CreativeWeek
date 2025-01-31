using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Phone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image imageProfil;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAge;
    [SerializeField] private TextMeshProUGUI textType;
    [SerializeField] private TextMeshProUGUI textDescription;

    [Header("RSO")]
    [SerializeField] private RSO_CurrentProfile rsoCurrentProfile;

    private void OnEnable()
    {
        imageProfil.sprite = rsoCurrentProfile.Value.ProfilSprite;
        textName.text = $"{rsoCurrentProfile.Value.Name}, {rsoCurrentProfile.Value.Age} years";
        textAge.text = $"{rsoCurrentProfile.Value.ProfilType}, {rsoCurrentProfile.Value.DietType}, {rsoCurrentProfile.Value.DrinkPreference}, {rsoCurrentProfile.Value.BillSeparation}";

        textType.text = "";

        for (int i = 0; i < rsoCurrentProfile.Value.Intolerances.Count; i++)
        {
            if(i > 0)
            {
                textType.text += ", ";
            }

            textType.text += rsoCurrentProfile.Value.Intolerances[i];
        }

        textDescription.text = rsoCurrentProfile.Value.Description;
    }
}