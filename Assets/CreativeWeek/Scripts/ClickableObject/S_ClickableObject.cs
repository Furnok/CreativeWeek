using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
    }

    private void OnDisable()
    {
        GetComponent<Image>().color = Color.white;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
