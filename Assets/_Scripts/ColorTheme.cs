using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTheme : MonoBehaviour
{

    public Color enemyHurt;

    #region Singleton
    
    static public ColorTheme Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

}
