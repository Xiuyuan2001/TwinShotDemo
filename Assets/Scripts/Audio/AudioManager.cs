using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource BGMSource;
    public AudioSource FXSource;

    #region Audio Clips

    public AudioClip MenuBgm;
    public AudioClip GameBgm;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip attack;
    public AudioClip enemyDead;
    public AudioClip pickUpCoin;
    public AudioClip victor;
    public AudioClip pickUpBuff;

    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayerBGM(MenuBgm);
    }

    public void PlayerBGM(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        FXSource.PlayOneShot(clip);
    }
}
