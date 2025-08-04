using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIのImageコンポーネントのFill Amountを制御するユーティリティクラスです。
/// </summary>
[RequireComponent(typeof(Image))]
public class ImageFillAmount : MonoBehaviour
{
    // --- privateフィールド ---
    private Image _image;

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Awake()
    {
        // Imageコンポーネントを取得します。
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// ImageのFill Amountを設定します。
    /// </summary>
    /// <param name="amount">設定するFill Amountの値（0.0fから1.0fの範囲にクランプされます）。</param>
    public void SetFillAmount(float amount)
    {
        // Imageコンポーネントがnullでなければ、Fill Amountを設定します。
        if (_image != null)
        {
            _image.fillAmount = Mathf.Clamp01(amount);
        }
    }
}