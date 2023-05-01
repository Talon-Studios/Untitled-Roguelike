using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Ability
{
    Dash,
    Stomp
}

public class PlayerAbility : MonoBehaviour
{

    [SerializeField] private StartObject startObject;

    [Header("Abilities")]
    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private StompAbility stompAbility;

    private void ActivateAbility() {
        switch (startObject.character.ability)
        {
            case Ability.Dash: {
                print("Dash Ability");
                dashAbility.Activate();
                break;
            }
            case Ability.Stomp: {
                print("Stomp Ability");
                stompAbility.Activate();
                break;
            }
        }
    }

    void OnAbility() {
        ActivateAbility();
    }

}
