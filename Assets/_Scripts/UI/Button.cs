using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private Image button;

    [SerializeField] private Color textHoverColor = Color.white;
    [SerializeField] private Color buttonHoverColor = Color.white;

    [SerializeField] private UnityEvent onClick;

    Color textOriginalColor;
    Color buttonOriginalColor;

    void Start() {
        if (text != null) textOriginalColor = text.color;
        if (button != null) buttonOriginalColor = button.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (text != null) text.color = textHoverColor;
        if (button != null) button.color = buttonHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (text != null) text.color = textOriginalColor;
        if (button != null) button.color = buttonOriginalColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

}
