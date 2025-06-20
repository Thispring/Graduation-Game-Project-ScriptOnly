using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    public MainScene mainScene;

    private Renderer spriteRenderer;
    public GameObject popupObject; // 팝업 오브젝트의 참조
    private bool isPopupOpen = false; // 팝업 상태 추적을 위한 변수

    private void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
        ClosePopup(); // 시작 시 팝업 닫힘
    }

    public void TogglePopupState()
    {
        if (isPopupOpen || mainScene.tipOpen == true)
        {
            ClosePopup(); // 팝업이 열려있는 경우 닫음
            mainScene.tipOpen = false;
        }
        else
        {
            OpenPopup(); // 팝업이 닫혀있는 경우 열음
        }
    }

    private void OpenPopup()
    {
        popupObject.SetActive(true);
        isPopupOpen = true;
        spriteRenderer.sortingOrder = 8;
    }

    private void ClosePopup()
    {
        popupObject.SetActive(false);
        isPopupOpen = false;
    }
}
