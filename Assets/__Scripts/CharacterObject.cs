using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Rogue,
    Ranger,
    Fighter,
    Mage,
    Artificer
}

[CreateAssetMenu(menuName = "Character", fileName = "New Character")]
public class CharacterObject : ScriptableObject
{

    public string characterName;
    [TextArea] public string characterDescription;
    public GameObject model;
    public Color colorTheme = Color.magenta;
    public GunObject gun;
    public Ability ability = Ability.Dash;
    public int unlockLevel = 10;
    public UpgradeObject[] upgrades;

}
