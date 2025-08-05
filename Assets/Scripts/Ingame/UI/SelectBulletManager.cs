using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class SelectBulletManager : MonoBehaviour
{
    public SelectBulletIcon this[int index] => _icons[index];


    [SerializeField]
    private SelectBulletIcon[] _icons;

    private void Start()
    {
        foreach (var icon in _icons)
        {
            icon.SelectedHighLight.gameObject.SetActive(false);
            icon.Guage.fillAmount = 0f;
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
