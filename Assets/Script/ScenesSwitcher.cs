using UnityEngine.SceneManagement;

public class ScenesSwitcher : Singleton<ScenesSwitcher>
{
    private const string MainLevelSceneName = "Game Level";
    private bool _isAlreadyLoading = false;

    public void LoadMainLevelScene() => LoadScene(MainLevelSceneName);

    protected override void SpecialAwakeAction()
    {
    }

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