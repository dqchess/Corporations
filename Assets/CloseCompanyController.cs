﻿using Assets.Utils;
using UnityEngine.UI;

public class CloseCompanyController : PopupButtonController<PopupMessageCloseCompany>
{
    public override void Execute()
    {
        CompanyUtils.CloseCompany(GameContext, Popup.companyId);

        Navigate(ScreenMode.GroupManagementScreen);
    }

    public override string GetButtonName()
    {
        return "YES";
    }
}

// TODO move to baseClass folder
public abstract class SimplePopupButtonController : ButtonController
{
    public abstract string GetButtonName();

    void OnEnable()
    {
        Initialize();

        SetButtonName(GetButtonName());
    }

    public virtual void SetButtonName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }
}

// TODO duplicate
public abstract class PopupButtonController<T> : ButtonController where T : PopupMessage
{
    public abstract string GetButtonName();

    void OnEnable()
    {
        Initialize();

        SetButtonName(GetButtonName());
    }

    internal T Popup => NotificationUtils.GetPopupMessage(GameContext) as T;

    public virtual void SetButtonName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }
}