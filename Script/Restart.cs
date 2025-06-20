using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private EffectSoundManager effectSoundManager;
    public void Main()
    {
        PlayerPrefs.DeleteKey("life");
        PlayerPrefs.DeleteKey("enemylife");
        soundManager.BGMSaveCurrentVolume();
        effectSoundManager.EffectSaveCurrentVolume();
        SceneManager.LoadScene("Main");
    }
}
