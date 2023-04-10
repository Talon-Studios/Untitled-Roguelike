using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData) {
        MousePointer.Instance.SetMouseState(MouseState.Interacting);
    }

    public void OnPointerExit(PointerEventData eventData) {
        MousePointer.Instance.SetMouseState(MouseState.Aiming);
    }

}
