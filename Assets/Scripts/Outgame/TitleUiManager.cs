using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiManager : MonoBehaviour
{
    [SerializeField] private GameObject title_Panel;

    [SerializeField] private float flashing_Max;

    [SerializeField] private List<Flashing_Image> flashing_Image;

    private Func<bool> GetButtonPressed1;
    private Func<bool> GetButtonPressed2;

    [SerializeField] private GameObject Operation_Panel;
    private void Start()
    {
        TitlePanelChange();
    }
    private void Update()
    {
        FlashingFunc();
    }
    public void SetFunction(Func<bool> button1, Func<bool> button2)
    {
        GetButtonPressed1 = button1;
        GetButtonPressed2 = button2;
    }
    private void FlashingFunc()
    {
        float time = Time.deltaTime;
        int num = 0;
        foreach (var flashing_image in flashing_Image)
        {
            if(num == 0) flashing_image.DecisionFunc(GetButtonPressed1.Invoke());
            if(num == 1) flashing_image.DecisionFunc(GetButtonPressed2.Invoke());
            num++;

            flashing_image.SetCount(Time.deltaTime);

            if (flashing_image.GetCount() >= flashing_Max)
            {
                flashing_image.FlashingSystem();
                flashing_image.ResetCount();
            }

        }
    }
    public void TitlePanelChange()
    {
        title_Panel.SetActive(true);
        Operation_Panel.SetActive(false);
    }
    public void OperationPanelChange()
    {
        Operation_Panel.SetActive(true);
    }
    public bool GetOperationPanelActive()
    {
        return Operation_Panel.activeSelf;
    }
    [Serializable]
    class Flashing_Image
    {
        [SerializeField] private Image main_Image;
        [SerializeField] private Image dicision_Image;

        private float count;

        private bool is_Flashing = false;
        private bool is_Decision;

        [SerializeField] private Color flashing_Color_Min;
        [SerializeField] private Color flashing_Color_Max;
        [SerializeField] private Color Dicision_Color;

        [SerializeField] private Color dicision_Image_Color;

        public void SetCount(float num)
        {
            count += num;
        }
        public void ResetCount()
        {
            count = 0;
        }
        public float GetCount()
        {
            return count;
        }
        public bool GetIsDecision()
        {
            return is_Decision;
        }
        private void SetFlaching()
        {
            dicision_Image.color = Color.clear;
            if (is_Flashing)
            {
                main_Image.color = flashing_Color_Max;
            }
            else
            {
                main_Image.color = flashing_Color_Min;
            }
        }
        public void FlashingSystem()
        {
            is_Flashing = !is_Flashing;

            if (is_Decision) return;

            SetFlaching();
        }
        public void DecisionFunc(bool is_dicision)
        {
            is_Decision = is_dicision;
            if (is_Decision)
            {
                main_Image.color = Dicision_Color;
                dicision_Image.color = dicision_Image_Color;
            }
            else
            {
                SetFlaching();
            }
        }
    }


}
