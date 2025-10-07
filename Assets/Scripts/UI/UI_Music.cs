using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Music : MonoBehaviour
{
    private static bool isMuted;
    private Image currentImage;

    [SerializeField] private Sprite fullImage;
    [SerializeField] private Sprite blankImage;

    private void Start()
    {
        currentImage = GetComponent<Image>();

        SetSoundAndImage();
    }

    private void Update()
    {
        SetSoundAndImage();
    }

    public void OnClicked()
    {
        isMuted = !isMuted;

        SetSoundAndImage();
    }

    private void SetSoundAndImage()
    {
        if (isMuted)
        {
            AudioManager.instance.BGMSource.volume = 0;
            currentImage.sprite = blankImage;
        }
        else
        {
            AudioManager.instance.BGMSource.volume = 1;
            currentImage.sprite = fullImage;
        }
    }
}
