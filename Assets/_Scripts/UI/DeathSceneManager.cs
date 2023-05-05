using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathSceneManager : MonoBehaviour
{

    [SerializeField] private EndObject endObject;
    [SerializeField] private StartObject startObject;
    [SerializeField] private float showInfoDelay = 1;

    [Header("Info")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelNumText;
    [SerializeField] private TMP_Text withText;
    [SerializeField] private TMP_Text characterText;
    [SerializeField] private GameObject playAgainButton;

    void Start() {
        Time.timeScale = 1;

        levelText.gameObject.SetActive(false);
        levelNumText.gameObject.SetActive(false);
        withText.gameObject.SetActive(false);
        characterText.gameObject.SetActive(false);
        playAgainButton.SetActive(false);

        levelNumText.text = "-" + endObject.level.ToString() + "-";
        characterText.text = startObject.character.characterName;

        StartCoroutine(ShowInfoRoutine());
    }

    private IEnumerator ShowInfoRoutine() {
        yield return new WaitForSeconds(showInfoDelay);
        levelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showInfoDelay);
        levelNumText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showInfoDelay);
        withText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showInfoDelay);
        characterText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showInfoDelay);
        playAgainButton.SetActive(true);
    }

    public void PlayAgain() {
        SceneLoader.Instance.LoadSelect();
    }

}
