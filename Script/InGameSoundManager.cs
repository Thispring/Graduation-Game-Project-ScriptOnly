using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSoundManager : MonoBehaviour
{
    public Slider BGMSoundSlider; // Inspector에서 슬라이더를 할당하기 위한 public 변수
    public AudioSource BGMTargetAudioSource; // Inspector에서 대상 AudioSource를 할당하기 위한 public 변수
    public Slider effectSoundSlider; // Inspector에서 슬라이더를 할당하기 위한 public 변수
    public AudioSource effectTargetAudioSource; // Inspector에서 대상 AudioSource를 할당하기 위한 public 변수

    private void Start()
    {
        // BGM 사운드
        float mainSavedVolume = PlayerPrefs.GetFloat("BGMVolume", BGMTargetAudioSource.volume);
        BGMSoundSlider.value = mainSavedVolume;
        BGMTargetAudioSource.volume = mainSavedVolume;
        BGMSoundSlider.onValueChanged.AddListener(BGMSetVolume);

        // 이펙트 사운드
        float effectSavedVolume = PlayerPrefs.GetFloat("EffectVolume", effectTargetAudioSource.volume);
        effectSoundSlider.value = effectSavedVolume;
        effectTargetAudioSource.volume = effectSavedVolume;
        effectSoundSlider.onValueChanged.AddListener(EffectSetVolume);

        // 여기서 초기값을 설정
        BGMSoundSlider.value = mainSavedVolume; 
        effectSoundSlider.value = effectSavedVolume;
    }
    
    private void BGMSetVolume(float volume)
    {
        BGMTargetAudioSource.volume = volume;

        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    private void EffectSetVolume(float volume)
    {
        effectTargetAudioSource.volume = volume;

        PlayerPrefs.SetFloat("EffectVolume", volume);
    }

    public void BGMSaveCurrentVolume()
    {
        // 씬이 변경되기 전에 현재 볼륨을 저장
        PlayerPrefs.SetFloat("BGMVolume", BGMTargetAudioSource.volume);
        PlayerPrefs.Save();
        Debug.Log("BGMVolume: " + PlayerPrefs.GetFloat("BGMVolume"));
    }
    public void EffectSaveCurrentVolume()
    {
        // 씬이 변경되기 전에 현재 볼륨을 저장
        PlayerPrefs.SetFloat("EffectVolume", effectTargetAudioSource.volume);
        PlayerPrefs.Save();
        Debug.Log("EffectVolume: " + PlayerPrefs.GetFloat("EffectVolume"));
    }
}
