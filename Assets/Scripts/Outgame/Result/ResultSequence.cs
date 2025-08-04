using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSequence : MonoBehaviour
{
    [SerializeField]
    private string _titleSceneName = "Title"; 

    private async void Start()
    {
        

        //一旦タイトルに戻る
        await Awaitable.WaitForSecondsAsync(1f);

        SceneManager.LoadScene(_titleSceneName);
    }
}
