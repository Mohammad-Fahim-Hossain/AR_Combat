using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkFwrdController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool MF = false;
    public void OnPointerDown(PointerEventData data)
    {
        MF = true;
        FighterController.mvFwrd = true;
        Debug.Log(MF);
    }

    public void OnPointerUp(PointerEventData data)
    {
        MF = false;
        FighterController.mvFwrd = false;
        Debug.Log(MF);
    }


}
