using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Material _material;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().material = _material;
        _material.SetTexture("_ImageTexture", GetComponent<Image>().sprite.texture);

        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + 0.2f, GetComponent<RectTransform>().localScale.y + 0.2f, GetComponent<RectTransform>().localScale.z + 0.2f);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().material = null;

        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - 0.2f, GetComponent<RectTransform>().localScale.y - 0.2f, GetComponent<RectTransform>().localScale.z - 0.2f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GetComponent<Image>().material = null;

        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - 0.2f, GetComponent<RectTransform>().localScale.y - 0.2f, GetComponent<RectTransform>().localScale.z - 0.2f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
