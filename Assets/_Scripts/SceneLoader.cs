using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private Transform circle;
    [SerializeField] private float transitionSpeed = 100;
    [SerializeField] private float delay = 1.5f;

    private float targetScale = 0;

    #region Singleton
    
    static public SceneLoader Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Update() {
        circle.localScale = Vector2.MoveTowards(circle.localScale, targetScale * Vector2.one, transitionSpeed * Time.unscaledDeltaTime);
    }

    private void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    public IEnumerator LoadSceneRoutine(string sceneName) {
        targetScale = 1;
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGame() {
        LoadScene("Game");
    }

    public void LoadStart() {
        LoadScene("Start");
    }

    public void LoadSelect() {
        LoadScene("Select");
    }

    public void LoadDie() {
        LoadScene("Die");
    }

}
