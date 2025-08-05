using System;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1000)]
public class SelectBulletManager : MonoBehaviour
{
    public SelectBulletIcon this[int index] => _icons[index];

    public void SetHighLight(int index, bool value)
    {
        _icons[index].SelectedHighLight.gameObject.SetActive(value);
    }

    [SerializeField]
    private SelectBulletIcon[] _icons;

    private void Start()
    {
        foreach (var icon in _icons)
        {
            icon.SelectedHighLight.gameObject.SetActive(false);
            icon.Guage.fillAmount = 1f;
        }
    }

    [Serializable]
    public struct SelectBulletIcon
    {
        public Image Icon => _icon;
        public Image Guage => _guage;
        public Image SelectedHighLight => _selectedHighLight;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Image _guage;

        [SerializeField]
        private Image _selectedHighLight;
    }
}
