using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character", fileName = "New Character")]
public class CharacterObject : ScriptableObject
{

    public string characterName;
    [TextArea] public string characterDescription;
    public GameObject model;
    public Color colorTheme = Color.magenta;
    public GunObject gun;
    public Ability ability = Ability.Dash;
    public UpgradeObject[] upgrades;

}
