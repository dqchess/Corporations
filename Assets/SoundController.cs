﻿using Assets;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Sound Sound;
    Button Button;

    SoundManager SoundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = new SoundManager();

        Button = GetComponent<Button>();

        Button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        SoundManager.Play(Sound);
    }

    void Destroy()
    {
        Button.onClick.RemoveListener(PlaySound);
    }
}
