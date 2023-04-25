using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    private const string MainLevelSceneName = "Game Level";
    private bool _isAlreadyLoading = false;

    public void LoadMainLevelScene() => LoadScene(MainLevelSceneName);

    private void LoadScene(string sceneName)
    {
        if (_isAlreadyLoading)
        {
            return;
        }

        _isAlreadyLoading = true;
        SceneManager.LoadSceneAsync(sceneName);
    }
}