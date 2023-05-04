using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{

    [SerializeField] private StartObject startObject;
    [SerializeField] private Material accentColorMaterial;

    [Header("Characters")]
    [SerializeField] private CharacterObject[] characters;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;
    [SerializeField] private Transform model;

    [Header("Weapons")]
    [SerializeField] private GunObject[] weapons;
    [SerializeField] private WeaponCell[] weaponCells;

    private int page = 0;

    #region Singleton
    
    static public SelectManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        if (startObject.character != null)
        {
            page = System.Array.IndexOf(characters, startObject.character);
        } else
        {
            page = 0;
        }

        SetCharacter(characters[page]);

        // weaponCells[0].selected = !weaponCells[0].selected;
        // weaponCells[0].border.color = weaponCells[0].hoverColor;

        // SetWeapon(weaponCells[0]);
    }

    public void NextPage(int direction) {
        page += direction;
        if (page > characters.Length - 1)
        {
            page = 0;
        } else if (page < 0)
        {
            page = characters.Length - 1;
        }

        SetCharacter(characters[page]);
    }

    public void SetCharacter(CharacterObject characterObject) {
        characterName.text = characterObject.characterName;
        characterDescription.text = "\"" + characterObject.characterDescription + "\"";

        Destroy(model.gameObject);
        model = Instantiate(characterObject.model, model.position, model.rotation).transform;

        startObject.character = characters[page];

        DynamicColorManager.Instance.Color = characterObject.colorTheme;
        accentColorMaterial.color = DynamicColorManager.Instance.Color;
    }

    public void SetWeapon(WeaponCell weaponCell) {
        startObject.gun = weaponCell.gun;

        foreach (WeaponCell cell in weaponCells)
        {
            cell.selected = false;
            cell.border.color = cell.originalColor;
        }

        weaponCell.selected = !weaponCell.selected;
        weaponCell.border.color = weaponCell.hoverColor;
    }

    public void Go() {
        SceneManager.LoadScene("Game");
    }

    void OnUIMovement(InputValue value) {
        NextPage((int)value.Get<float>());
    }

    public void Back() {
        SceneManager.LoadScene("Start");
    }

    void OnUISelect() {
        Go();
    }

}
