using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField]
    private MainSoundManager mainSoundManager;
    public void GameStart()
    {
        //PlayerPrefs.DeleteAll();
        mainSoundManager.SaveCurrentVolume();
        SceneManager.LoadScene("PVE");
    }

    public void ReadExplain()
    {
        OpenPopup2();
        ClosePopup();
        tipOpen = true;
    }

    public GameObject popupObject; // 팝업 오브젝트의 참조
    public GameObject popupObject2;
    private bool isPopupOpen = false; // 팝업 상태 추적을 위한 변수
    public bool tipOpen = false;

    private void Start()
    {
        ClosePopup(); // 시작 시 팝업 닫힘
        LoadVolume();
    }
    private void LoadVolume()
    {
        if (mainSoundManager != null)
        {
            // MainSoundManager에서 저장된 볼륨 불러오기
            float savedVolume = PlayerPrefs.GetFloat("MainMusicVolume", 1f);
            mainSoundManager.soundSlider.value = savedVolume;
            mainSoundManager.maintargetAudioSource.volume = savedVolume;
        }
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
        popupObject.SetActive(true);
        isPopupOpen = true;
    }
    private void OpenPopup2()
    {
        popupObject2.SetActive(true);
    }

    private void ClosePopup()
    {
        popupObject.SetActive(false);
        isPopupOpen = false;
    }
}