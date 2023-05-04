using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    public void Play() {
        SceneManager.LoadScene("Select");
    }

    public void Options() {
        SceneManager.LoadScene("Settings");
    }

    public void Quit() {
        Application.Quit();
    }

}
