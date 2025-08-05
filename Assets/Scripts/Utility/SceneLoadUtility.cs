using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadUtility
{
    public static async void LoadScene(string sceneName)
    {
        Scene current = SceneManager.GetActiveScene();
        await SceneManager.UnloadSceneAsync(current.name);
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
