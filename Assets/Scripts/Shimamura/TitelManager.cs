using UnityEngine;
using UnityEngine.SceneManagement;

public class TitelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TitleSenen()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OptionSenen()
    {
        SceneManager.LoadScene("OptionScene");
    }
    public void StratGame()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後にアプリケーションを終了
        Application.Quit();
#endif
    }
}
