using SymphonyFrameWork.System;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleUiManager : MonoBehaviour
{
    [SerializeField] private GameObject Press_Space;
    [SerializeField] private GameObject title_Panel;

    [SerializeField] private float flashing_Max;

    [SerializeField] private List<Flashing_Image> flashing_Image;

    private Func<bool> GetButtonPressed1;
    private Func<bool> GetButtonPressed2;

    [SerializeField] private GameObject Operation_Panel;
    [SerializeField] private List<GameObject> Operation_Page;
    

    private Action Operation_End_Action;

    [SerializeField] private int page_num;

    private void Start()
    {
        TitlePanelChange();

        if(Operation_Page.Count != 0) Operation_Page.Clear();
        for (int i = 0; i < Operation_Panel.transform.childCount; i++)
        {
            Debug.Log("追加");
            Operation_Page.Add(Operation_Panel.transform.GetChild(i).gameObject);
        }
        SelectedOperationPage();

    }
    private void Update()
    {
        FlashingFunc();
    }
    public void SetFunction(Func<bool> button1, Func<bool> button2, Action operation_end_action)
    {
        GetButtonPressed1 = button1;
        GetButtonPressed2 = button2;
        Operation_End_Action = operation_end_action;
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
    public void OperationPanelChange(InputAction.CallbackContext context) 
    {
        if (Operation_Panel.activeSelf)
        {
            Operation_Panel.SetActive(false);
            page_num = 0;
        }
        else
        {
            Operation_Panel.SetActive(true);
            SelectedOperationPage();
        }
    }
    private void SelectedOperationPage()
    {
        for(int i = 0; i < Operation_Page.Count; i++)
        {
            if (Operation_Panel.activeSelf)
            {
                Press_Space.SetActive(false);
            }
            else
            {
                Press_Space.SetActive(true);
            }


            if (i == page_num)
            {
                Operation_Page[i].SetActive(true);
            }
            else
            {
                Operation_Page[i].SetActive(false);
                Press_Space.SetActive(true);
            }
        }
    }
    public void PageChangerAction(InputAction.CallbackContext context)
    {
        if (!Operation_Panel.activeSelf) return;
        float vec = context.ReadValue<float>();


        if (vec > 0)
        {
            if (page_num + 1 >= Operation_Page.Count) return;
            page_num++;
            Debug.Log(page_num);
        }
        else if (vec < 0)
        {
            if (page_num == 0) return;
            page_num--;
        }

        SelectedOperationPage();
    }
    
    public bool GetOperationPanelActive()
    {
        if (page_num == Operation_Page.Count - 1)
        {
            return true;
        }
        return false;
    }
    [Serializable]
    class Flashing_Image
    {
        [SerializeField] private List<Image> main_Image;
        [SerializeField] private Text text;
        [SerializeField] private Image dicision_Image;

        public float count;

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
            if(dicision_Image) dicision_Image.color = Color.clear;
            if (is_Flashing)
            {
                ChangeColor(flashing_Color_Max);
            }
            else
            {
                ChangeColor(flashing_Color_Min);
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
                ChangeColor(Dicision_Color);
            }
            else
            {
                SetFlaching();
            }
        }
        public void ChangeColor(Color color)
        {
            if(text)text.color = color;
            foreach (var item in main_Image)
            {
                item.color = color;
            }
        }
    }


}
