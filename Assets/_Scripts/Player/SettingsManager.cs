using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] private GameObject startOptions;

    public void Back() {
        SceneManager.LoadScene("Start");
    }

    public void ChangeMasterVolume() {

    }

    public void ChangeMusicVolume() {

    }

}
