using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenuUIHandler : MonoBehaviour {

    [SerializeField] private GameObject initialScreen;

    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Slider loadingSlider;

    private void Start() {
        initialScreen.SetActive(true);
        loadingScreen.SetActive(false);
        loadingSlider.value = 0;
    }

    public void StartGame() {
        initialScreen.SetActive(false);
        loadingScreen.SetActive(true);
        this.StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync() {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(1);

        if (loadAsync == null) yield break;

        while (!loadAsync.isDone) {
            loadingSlider.value = loadAsync.progress;
            yield return null;
        }
        
        initialScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    
}