using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamicColorManager : MonoBehaviour
{

    private Color color;
    public Color Color {
        get { return color; }
        set { color = value; OnColorChanged?.Invoke(value); }
    }

    public delegate void ColorChangedHandler(Color color);
    public static event ColorChangedHandler OnColorChanged;

    [SerializeField] private StartObject startObject;
    
    static public DynamicColorManager Instance = null;
    
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        if (startObject.character != null) color = startObject.character.colorTheme;
    }

}
