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
    [SerializeField] private UnityEvent onClick;

    private bool hovering = false;

    Color textOriginalColor;
    Color buttonOriginalColor;

    void Start() {
        if (text != null) textOriginalColor = text.color;
        if (button != null) buttonOriginalColor = button.color;

        DynamicColorManager.OnColorChanged += HandleColorChanged;
    }

    private void HandleColorChanged(Color newColor) {
        if (button != null && hovering) button.color = DynamicColorManager.Instance.Color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (text != null) text.color = textHoverColor;
        if (button != null) button.color = DynamicColorManager.Instance.Color;

        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (text != null) text.color = textOriginalColor;
        if (button != null) button.color = buttonOriginalColor;

        hovering = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

}
