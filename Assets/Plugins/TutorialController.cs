﻿using Assets.Utils;
using UnityEngine;

public class TutorialController : View
{
    [Tooltip("This components will show up when this tutorial functionality will be unlocked")]
    public TutorialFunctionality TutorialFunctionality;

    public GameObject[] HideableObjects;

    public override void ViewRender()
    {
        base.ViewRender();

        var show = TutorialUtils.IsOpenedFunctionality(GameContext, TutorialFunctionality);

        foreach (var obj in HideableObjects)
            obj.SetActive(show);
    }
}