using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_UIPrepa : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private Slider sliderTimer;
    [SerializeField] private List<Button> buttonItems;
    [SerializeField] private List<Button> buttonObjects;

    [Header("RSE")]
    [SerializeField] private RSE_UpdateTimer rseUpdateTimer;
    [SerializeField] private RSE_UpdateInventory rseUpdateInventory;
    [SerializeField] private RSE_RemoveItem rseRemoveItem;
    [SerializeField] private RSE_ShowInScene rseShowInScene;
    [SerializeField] private RSE_HideInScene rseHideInScene;

    [Header("RSO")]
    [SerializeField] private RSO_TimerPreparation rsoTimerPreparation;
    [SerializeField] private RSO_CurrentListObject rsoCurrentListObject;

    [Header("SSO")]
    [SerializeField] private SSO_TimerPreparation ssoTimerPreparation;

    private void Start()
    {
        textTimer.text = ssoTimerPreparation.Value.ToString("F2") + "s";
        sliderTimer.maxValue = ssoTimerPreparation.Value;
        sliderTimer.value = ssoTimerPreparation.Value;
    }

    private void OnEnable()
    {
        rseUpdateTimer.action += UpdateTime;
        rseUpdateInventory.action += UpdateInventory;
        rseShowInScene.action += ShowInScene;
        rseHideInScene.action += HideInScene;
    }

    private void OnDisable()
    {
        rseUpdateTimer.action -= UpdateTime;
        rseUpdateInventory.action -= UpdateInventory;
        rseShowInScene.action -= ShowInScene;
        rseHideInScene.action -= HideInScene;
    }

    private void UpdateTime()
    {
        textTimer.text = rsoTimerPreparation.Value.ToString("F2") + "s";
        sliderTimer.value = rsoTimerPreparation.Value;
    }

    private void UpdateInventory(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < buttonItems.Count; i++)
        {
            buttonItems[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
            buttonItems[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < rsoCurrentListObject.Value.Count; i++)
        {
            buttonItems[i].transform.GetChild(0).GetComponent<Image>().sprite = rsoCurrentListObject.Value[i].Sprite;
            buttonItems[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            buttonItems[i].onClick.RemoveAllListeners();
            buttonItems[i].onClick.AddListener(() => rseRemoveItem.RaiseEvent(rsoCurrentListObject.Value[i].Index));
        }
    }

    private void ShowInScene(int index)
    {
        buttonObjects[index].gameObject.SetActive(true);
    }

    private void HideInScene(int index)
    {
        buttonObjects[index].gameObject.SetActive(false);
    }
}