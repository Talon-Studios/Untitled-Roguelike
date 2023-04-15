using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSelectManager : MonoBehaviour
{

    [SerializeField] private GunObject[] weapons;

    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDescription;
    [SerializeField] private Transform model;

    private int page = 0;

    #region Singleton
    
    static public WeaponSelectManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        SetWeaponInfo(weapons[page]);
    }

    public void NextPage(int direction) {
        page += direction;
        if (page > weapons.Length - 1)
        {
            page = 0;
        } else if (page < 0)
        {
            page = weapons.Length - 1;
        }

        SetWeaponInfo(weapons[page]);
    }

    public void SetWeaponInfo(GunObject gunObject) {
        weaponName.text = gunObject.weaponName;
        weaponDescription.text = "\"" + gunObject.weaponDescription + "\"";

        Destroy(model.gameObject);
        model = Instantiate(gunObject.model, model.position, model.rotation).transform;
    }

    public void SetGameWeapon(GunObject gunObject) {
        
    }

}
