using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1, 1, 0, 1f);
        GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
        GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GetComponent<Image>().color = Color.white;
        GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
