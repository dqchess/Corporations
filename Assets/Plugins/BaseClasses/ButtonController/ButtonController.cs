﻿using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract partial class ButtonController : BaseClass
{
    Button Button;
    [Tooltip("Sets the color of link")]
    public bool IsLink = false;

    public abstract void Execute();
    public virtual void ButtonStart() { }

    void ExecuteAndUpdateScreen()
    {
        Execute();

        // TODO RENAVIGATE
        if (Contains<ToggleRoleButtons>())
            return;

        if (Contains<AutomaticallyShowAnnualReport>())
            return;

        UpdatePage();
    }

    // start
    public virtual void Initialize()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(ExecuteAndUpdateScreen);

        ButtonStart();

        if (IsLink)
            GetComponentInChildren<Text>().color = Visuals.GetColorFromString(VisualConstants.COLOR_LINK);
    }

    void OnEnable()
    {
        Initialize();
    }

    // destroy
    void OnDisable()
    {
        RemoveListener();
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(ExecuteAndUpdateScreen);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void UpdatePage()
    {
        ScreenUtils.UpdateScreenWithoutAnyChanges(GameContext);
    }
}

// others
public abstract partial class ButtonController
{
    public void UnlockTutorialFunctionality(TutorialFunctionality tutorialFunctionality)
    {
        TutorialUtils.Unlock(GameContext, tutorialFunctionality);
    }

    public void SetSelectedCompany(int companyId)
    {
        ScreenUtils.SetSelectedCompany(GameContext, companyId);
    }
}

// navigation
public abstract partial class ButtonController
{
    public void GoBack()
    {
        ScreenUtils.NavigateBack(GameContext);
    }

    // navigate
    public void Navigate(ScreenMode screenMode, string field, object data)
    {
        //if (!HasProductCompany && screenMode == ScreenMode.DevelopmentScreen)
        //    return;

        ScreenUtils.Navigate(GameContext, screenMode, field, data);
    }

    public void Navigate(ScreenMode screenMode)
    {
        //if (!HasProductCompany && screenMode == ScreenMode.DevelopmentScreen)
        //    return;

        ScreenUtils.Navigate(GameContext, screenMode);
    }

    public void NavigateToNiche(NicheType niche)
    {
        Navigate(ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, niche);
    }

    public void NavigateToIndustry(IndustryType industry)
    {
        Navigate(ScreenMode.IndustryScreen, Constants.MENU_SELECTED_INDUSTRY, industry);
    }

    public void NavigateToCompany(ScreenMode screenMode, int companyId)
    {
        Navigate(screenMode, Constants.MENU_SELECTED_COMPANY, companyId);
    }

    public void NavigateToHuman(int humanId)
    {
        Navigate(ScreenMode.CharacterScreen, Constants.MENU_SELECTED_HUMAN, humanId);
    }

    public void NavigateToProjectScreen(int companyId)
    {
        NavigateToCompany(ScreenMode.ProjectScreen, companyId);
    }
}