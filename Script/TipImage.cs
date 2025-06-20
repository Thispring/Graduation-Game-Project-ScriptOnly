using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipImage : MonoBehaviour
{
    public Image targetImage;  // 이미지 UI 요소
    public Sprite[] imageArray;  // 이미지 스프라이트 배열
    private int currentIndex = 0;  // 현재 표시 중인 이미지의 인덱스

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % imageArray.Length;
        targetImage.sprite = imageArray[currentIndex];
    }

    public void PreviousImage()
    {
        currentIndex = (currentIndex - 1 + imageArray.Length) % imageArray.Length;
        targetImage.sprite = imageArray[currentIndex];
    }

    public MainScene mainScene;

    private Renderer spriteRenderer;
    public GameObject popupObject; // 팝업 오브젝트의 참조
    private bool isPopupOpen = false; // 팝업 상태 추적을 위한 변수

    private void Start()
    {
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
    }

    private void ClosePopup()
    {
        popupObject.SetActive(false);
        isPopupOpen = false;
    }
}
