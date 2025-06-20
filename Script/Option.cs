using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject popupObject; // 팝업 오브젝트의 참조
    private bool isPopupOpen = false; // 팝업 상태 추적을 위한 변수
    public Manager manager;
    public SoundManager soundManager;
    public EffectSoundManager effectSoundManager;
    //public MainSoundManager mainSoundManager;
    public bool menuOn = false;
    public Tip_InGame tip_InGame;

    private void Start()
    {
        InGameLoadVolume();
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
        if (tip_InGame.tipOn == false)
        {
            popupObject.SetActive(true);
            isPopupOpen = true;
            menuOn = true;
        }
        
    }

    private void ClosePopup()
    {
        popupObject.SetActive(false);
        isPopupOpen = false;
        menuOn = false;
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteKey("life");
        PlayerPrefs.DeleteKey("enemylife");
        soundManager.BGMSaveCurrentVolume();
        effectSoundManager.EffectSaveCurrentVolume();
        Application.Quit();
    }

    public void Main()
    {
        PlayerPrefs.DeleteKey("life");
        PlayerPrefs.DeleteKey("enemylife");
        //PlayerPrefs.DeleteAll();
        //mainSoundManager.SaveCurrentVolume();
        soundManager.BGMSaveCurrentVolume();
        effectSoundManager.EffectSaveCurrentVolume();
        SceneManager.LoadScene("Main");
    }

    private void InGameLoadVolume()
    {
        if (soundManager != null && effectSoundManager != null)
        {
            Debug.Log("로드성공");

            float savedBGMVolume = PlayerPrefs.GetFloat("MusicVolume", soundManager.ingametargetAudioSource.volume);
            soundManager.soundSlider.value = Mathf.Clamp01(savedBGMVolume); // Slider 값은 0에서 1 사이로 정규화
            soundManager.ingametargetAudioSource.volume = soundManager.soundSlider.value;

            float savedEffectVolume = PlayerPrefs.GetFloat("EffectVolume", effectSoundManager.effecttargetAudioSource.volume);
            effectSoundManager.soundSlider.value = Mathf.Clamp01(savedEffectVolume); // Slider 값은 0에서 1 사이로 정규화
            effectSoundManager.effecttargetAudioSource.volume = effectSoundManager.soundSlider.value;
        }
    }
}