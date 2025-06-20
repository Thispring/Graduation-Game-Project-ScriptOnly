using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip_InGame : MonoBehaviour
{
    public GameObject popupObject; // 팝업 오브젝트의 참조
    private bool isPopupOpen = false; // 팝업 상태 추적을 위한 변수
    public bool tipOn = false;
    public Option option;

    private void Start()
    {
        ClosePopup(); // 시작 시 팝업 닫힘
    }

    public void TogglePopupState()
    {
        if (isPopupOpen)
        {
            ClosePopup(); // 팝업이 열려있는 경우 닫음
        }
        else
        {
            OpenPopup(); // 팝업이 닫혀있는 경우 열음
        }
    }

    private void OpenPopup()
    {
        if (option.menuOn == false)
        {
            popupObject.SetActive(true);
            isPopupOpen = true;
            tipOn = true;
        }        
    }

    private void ClosePopup()
    {
        popupObject.SetActive(false);
        isPopupOpen = false;
        tipOn = false;
    }
}
