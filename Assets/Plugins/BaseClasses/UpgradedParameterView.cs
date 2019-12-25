﻿using Assets.Utils;
using UnityEngine.UI;

// TODO extend ParameterView
public abstract class UpgradedParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void PickComponents()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        PickComponents();

        Text.text = RenderValue();

        string hint = RenderHint();

        //if (hint.Length > 0)
        //Hint.SetHint(hint);

        if (Hint != null)
            Hint.SetHint(hint);
    }

    public void Colorize(string color)
    {
        Text.color = Visuals.GetColorFromString(color);
    }

    public void Colorize(int value, int min, int max)
    {
        Text.color = Visuals.GetGradientColor(min, max, value);
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}