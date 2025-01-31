using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_InventoryManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] RSE_PickUpItem RSE_PickUpItem;
    [SerializeField] RSE_RemoveItem RSE_RemoveItem;
    [SerializeField] RSE_UpdateInventory RSE_UpdateInventory;
    [SerializeField] RSE_ShowInScene rseShowInScene;
    [SerializeField] RSE_HideInScene rseHideInScene;
    [Header("RSO")]
    [SerializeField] RSO_CurrentListObject RSO_CurrentListObject;
    [Header("SSO")]
    [SerializeField] SSO_ListObject SSO_ListObject;
    [SerializeField] SSO_MaxObject SSO_MaxObject;
    private bool isNotInCurrentList = true;
    private bool haveAlreadyTenue = false;

    private void OnEnable()
    {
        RSE_PickUpItem.action += AddItem;
        RSE_RemoveItem.action += RemoveItem;
    }
    private void OnDisable()
    {
        RSE_PickUpItem.action -= AddItem;
        RSE_RemoveItem.action -= RemoveItem;
    }

    private void Start()
    {
        RSO_CurrentListObject.Value.Clear();
    }

    private void AddItem(int index)
    {
        if (RSO_CurrentListObject.Value.Count < SSO_MaxObject.Value)
        {
            isNotInCurrentList = true;
            haveAlreadyTenue = false;
            foreach (var item in RSO_CurrentListObject.Value)
            {
                if(item.Index == index)
                {
                    isNotInCurrentList = false;
                }

                if (item.Index == index)
                {
                    haveAlreadyTenue = true;
                }
            }
            if (isNotInCurrentList && !haveAlreadyTenue)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                RSO_CurrentListObject.Value.Add(SSO_ListObject.Value.FirstOrDefault(x => x.Index == index));
                RSE_UpdateInventory.RaiseEvent(index);
                rseHideInScene.RaiseEvent(index);
            }
        }
    }

    private void RemoveItem(int index)
    {
        if (RSO_CurrentListObject.Value.Count > 0)
        {
            bool isInCurrentList = false;

            foreach (var item in RSO_CurrentListObject.Value)
            {
                if (item.Index == index)
                {
                    isInCurrentList = true;
                }
            }

            if (isInCurrentList)
            {
                RSO_CurrentListObject.Value.Remove(SSO_ListObject.Value.FirstOrDefault(x => x.Index == index));
                RSE_UpdateInventory.RaiseEvent(index);
                rseShowInScene.RaiseEvent(index);
            }
        }
    }
}
