using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class StartSceneManager : MonoBehaviour
{

    [SerializeField] private GameObject startOptions;
    [SerializeField] private GameObject settingsOptions;

    [Header("Settings")]
    [SerializeField] private TMP_Text masterVolumeText;
    [SerializeField] private TMP_Text musicVolumeText;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioMixer musicMixer;

    private float masterVolume = 100;
    private float musicVolume = 100;

    void Start() {
        StartOptions();

        SetVolume(ref masterVolume, ref masterVolumeText, "MasterVolume", "Master Volume", PlayerPrefs.GetFloat("Master Volume", 100));
        SetVolume(ref musicVolume, ref musicVolumeText, "MusicVolume", "Music Volume", PlayerPrefs.GetFloat("Music Volume", 100));
    }

    public void Play() {
        SceneManager.LoadScene("Select");
    }

    public void Settings() {
        startOptions.SetActive(false);
        settingsOptions.SetActive(true);
    }

    public void StartOptions() {
        startOptions.SetActive(true);
        settingsOptions.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }

    private void SetVolume(ref float volume, ref TMP_Text text, string parameter, string settingName, float percent) {
        volume = percent;
        if (volume > 101) volume = 0;
        if (volume < 0.0001f) volume = 0.0001f;
        masterMixer.SetFloat(parameter, Mathf.Log10(volume / 100f) * 20f);
        text.text = "- " + settingName + ": " + volume.ToString("0") + "% -";

        PlayerPrefs.SetFloat(settingName, volume);
        PlayerPrefs.Save();
    }

    public void ChangeMasterVolume() {
        SetVolume(ref masterVolume, ref masterVolumeText, "MasterVolume", "Master Volume", masterVolume + 10);
    }

    public void ChangeMusicVolume() {
        SetVolume(ref musicVolume, ref musicVolumeText, "MusicVolume", "Music Volume", musicVolume + 10);
    }

}
