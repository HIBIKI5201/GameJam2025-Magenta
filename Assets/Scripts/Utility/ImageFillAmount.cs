using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFillAmount : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetFillAmount(float amount)
    {
        if (_image != null)
        {
            _image.fillAmount = Mathf.Clamp01(amount);
        }
    }
}
