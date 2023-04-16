using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Button : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

}
