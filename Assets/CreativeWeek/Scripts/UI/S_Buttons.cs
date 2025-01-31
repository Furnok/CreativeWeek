using UnityEngine;
using UnityEngine.UI;

public class S_Buttons : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}