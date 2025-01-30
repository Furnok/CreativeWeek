using System;
using System.Collections.Generic;
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
    [SerializeField] private List<int> itemPosses;
    [SerializeField] private List<GameObject> panelViews;

    [Header("RSE")]
    [SerializeField] private RSE_UpdateTimer rseUpdateTimer;
    [SerializeField] private RSE_UpdateInventory rseUpdateInventory;
    [SerializeField] private RSE_RemoveItem rseRemoveItem;
    [SerializeField] private RSE_ShowInScene rseShowInScene;
    [SerializeField] private RSE_HideInScene rseHideInScene;
    [SerializeField] private RSE_ClickItem rseClickItem;
    [SerializeField] private RSE_ArrowLeft rseArrowLeft;
    [SerializeField] private RSE_ArrowRight rseArrowRight;
    [SerializeField] private RSE_Placard rsePlacard;

    [Header("RSO")]
    [SerializeField] private RSO_TimerPreparation rsoTimerPreparation;
    [SerializeField] private RSO_CurrentListObject rsoCurrentListObject;

    [Header("SSO")]
    [SerializeField] private SSO_TimerPreparation ssoTimerPreparation;

    private int view;

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
        rseClickItem.action += ClickItem;
        rseArrowLeft.action += ArrowLeft;
        rseArrowRight.action += ArrowRight;
        rsePlacard.action += Placard;
    }

    private void OnDisable()
    {
        rseUpdateTimer.action -= UpdateTime;
        rseUpdateInventory.action -= UpdateInventory;
        rseShowInScene.action -= ShowInScene;
        rseHideInScene.action -= HideInScene;
        rseClickItem.action -= ClickItem;
        rseArrowLeft.action -= ArrowLeft;
        rseArrowRight.action -= ArrowRight;
        rsePlacard.action -= Placard;
    }

    private void UpdateTime()
    {
        textTimer.text = rsoTimerPreparation.Value.ToString("F2") + "s";
        sliderTimer.value = rsoTimerPreparation.Value;
    }

    private void UpdateInventory(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);

        itemPosses.Clear();

        for (int i = 0; i < buttonItems.Count; i++)
        {
            if (rsoCurrentListObject.Value.Count > i)
            {
                buttonItems[i].transform.GetChild(0).GetComponent<Image>().sprite = rsoCurrentListObject.Value[i].Sprite;
                buttonItems[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                itemPosses.Add(rsoCurrentListObject.Value[i].Index);
            }
            else
            {
                buttonItems[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                buttonItems[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void ClickItem(int index)
    {
        if(index < itemPosses.Count)
        {
            buttonObjects[itemPosses[index]].gameObject.SetActive(true);
            rseRemoveItem.RaiseEvent(itemPosses[index]);
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

    private void ArrowLeft()
    {
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < panelViews.Count; i++)
        {
            panelViews[i].gameObject.SetActive(false);
        }

        if(view == 0)
        {
            view = 1;
            panelViews[view].gameObject.SetActive(true);
        }
        else if(view == 2)
        {
            view = 1;
            panelViews[view].gameObject.SetActive(true);
        }
        else
        {
            view = 0;
            panelViews[view].gameObject.SetActive(true);
        }
    }

    private void ArrowRight()
    {
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < panelViews.Count; i++)
        {
            panelViews[i].gameObject.SetActive(false);
        }

        if (view == 0)
        {
            view = 1;
            panelViews[view].gameObject.SetActive(true);
        }
        else if (view == 2)
        {
            view = 1;
            panelViews[view].gameObject.SetActive(true);
        }
        else
        {
            view = 0;
            panelViews[view].gameObject.SetActive(true);
        }
    }

    private void Placard()
    {
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < panelViews.Count; i++)
        {
            panelViews[i].gameObject.SetActive(false);
        }

        view = 2;
        panelViews[view].gameObject.SetActive(true);
    }
}