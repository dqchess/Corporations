﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextBlink : MonoBehaviour
{
    int size;
    float duration;

    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();
        size = Text.fontSize;
        duration = 1;
    }

    void Render()
    {
        Text.fontSize = (int)(size * (1 + 0.85f * duration));
        duration -= Time.deltaTime;

        if (duration < 0)
            Destroy(this);
    }

    void Update()
    {
        Render();
    }
}