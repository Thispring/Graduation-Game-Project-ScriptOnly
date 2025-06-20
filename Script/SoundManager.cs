using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider soundSlider; // Inspector에서 슬라이더를 할당하기 위한 public 변수
    public AudioSource ingametargetAudioSource; // Inspector에서 대상 AudioSource를 할당하기 위한 public 변수

    private void Start()
    {
        // 슬라이더의 값을 PlayerPrefs에서 읽어와 대상 AudioSource의 볼륨으로 설정
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // 기본값은 1 (최대 볼륨)
        soundSlider.value = savedVolume;
        SetVolume(savedVolume);
        ingametargetAudioSource.volume = savedVolume;

        // 슬라이더의 "On Value Changed" 이벤트에 이 스크립트의 SetVolume 메서드 연결
        soundSlider.onValueChanged.AddListener(SetVolume);
    }

    // 슬라이더를 조절할 때 호출되는 메서드
    private void SetVolume(float volume)
    {
        ingametargetAudioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void BGMSaveCurrentVolume()
    {
        // 씬이 변경되기 전에 현재 볼륨을 저장
        PlayerPrefs.SetFloat("MusicVolume", ingametargetAudioSource.volume);
        //PlayerPrefs.Save();
        Debug.Log("MusicVolume: " + PlayerPrefs.GetFloat("MusicVolume"));
    }
    
}
