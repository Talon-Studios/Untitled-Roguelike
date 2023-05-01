using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    Dash,
    Stomp
}

public class CharacterManager : MonoBehaviour
{

    [SerializeField] private StartObject startObject;

    [Header("Abilities")]
    [SerializeField] private DashAbility dashAbility;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        AddAbilityScript();
    }

    private void AddAbilityScript() {
        switch (startObject.character.ability)
        {
            case Ability.Dash: {
                print("Dash Ability");
                dashAbility.enabled = true;
                break;
            }
        }
    }

}
