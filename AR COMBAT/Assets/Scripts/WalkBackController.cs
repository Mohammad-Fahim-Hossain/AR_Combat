using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkBackController : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public static bool MB = false;
    
    public void OnPointerDown(PointerEventData data) {
        FighterController.mvBack = true;
        MB = true;
        Debug.Log(MB);
    }

    public void OnPointerUp(PointerEventData data)
    {
        FighterController.mvBack = false;
        MB = false;
        Debug.Log(MB);
    }


}
