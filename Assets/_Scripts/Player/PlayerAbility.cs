using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Ability
{
    Dash,
    Stomp,
    Bombs
}

public class PlayerAbility : MonoBehaviour
{

    [SerializeField] private StartObject startObject;
    [SerializeField] private float cooldown = 3;
    [SerializeField] private ParticleSystem dustParticles;

    [Header("Abilities")]
    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private StompAbility stompAbility;
    [SerializeField] private BombsAbility bombsAbility;

    private float cooldownTimer = 0;
    private bool abilityIsReady = true;

    Color originalDustColor;
    ParticleSystem.MainModule main;

    void Start() {
        originalDustColor = dustParticles.main.startColor.color;
        main = dustParticles.main;
        ReadyAbility();
    }

    void Update() {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > cooldown && !abilityIsReady)
        {
            ReadyAbility();
        }
    }

    private void ActivateAbility() {
        if (abilityIsReady)
        {
            abilityIsReady = false;
            cooldownTimer = 0;
            main.startColor = new ParticleSystem.MinMaxGradient(originalDustColor);

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
                case Ability.Bombs: {
                    print("Bombs ability");
                    bombsAbility.Activate();
                    break;
                }
            }
        }
    }

    private void ReadyAbility() {
        abilityIsReady = true;
        print("Ability is ready");
        main.startColor = new ParticleSystem.MinMaxGradient(DynamicColorManager.Instance.Color);
    }

    void OnAbility() {
        ActivateAbility();
    }

}
