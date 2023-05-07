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
    [SerializeField] private GameObject lockModel;
    [SerializeField] private TMP_Text levelsText;

    [Header("Characters")]
    [SerializeField] private CharacterObject[] characters;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;
    [SerializeField] private Transform model;

    [Header("Weapons")]
    [SerializeField] private GunObject[] weapons;
    [SerializeField] private WeaponCell[] weaponCells;

    private int page = 0;
    private int level = 0;
    private bool selectedCharacter = false;

    #region Singleton
    
    static public SelectManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        page = 0;

        SetLevel();
        SetCharacter(characters[page]);
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
        Destroy(model.gameObject);
        GameObject characterModel;
        if (level >= characterObject.unlockLevel)
        {
            characterName.text = characterObject.characterName;
            characterDescription.text = "\"" + characterObject.characterDescription + "\"";
            characterModel = characterObject.model;
        } else
        {
            characterName.text = "Locked";
            characterDescription.text = "Get to level " + characterObject.unlockLevel + " to unlock";
            characterModel = lockModel;
        }
        model = Instantiate(characterModel, model.position, model.rotation).transform;

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

    private void SetLevel() {
        level = PlayerPrefs.GetInt("Levels", 0);
        levelsText.text = level.ToString();
    }

    public void Go() {
        if (level >= characters[page].unlockLevel)
        {
            selectedCharacter = true;
            AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.select);
            SceneLoader.Instance.LoadGame();
        } else
        {
            AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.locked);
            FollowCam.Instance.ScreenShake(0.1f, 0.3f);
        }
    }

    public void Back() {
        SceneLoader.Instance.LoadStart();
    }

    void OnUIMovement(InputValue value) {
        if (!selectedCharacter)
        {
            if (value.Get<float>() != 0) AudioManager.Instance.PlayRandomPitch(AudioManager.Instance.hover);
            NextPage((int)value.Get<float>());
        }
    }

    void OnUISelect() {
        Go();
    }

}
