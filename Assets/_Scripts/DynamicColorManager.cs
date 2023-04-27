using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColorManager : MonoBehaviour
{

    public Color color;

    [SerializeField] private StartObject startObject;
    
    static public DynamicColorManager Instance = null;
    
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        if (startObject.character != null) color = startObject.character.colorTheme;
    }

}
