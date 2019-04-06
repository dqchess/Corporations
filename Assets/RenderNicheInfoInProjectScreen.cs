﻿using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View, IMenuListener
{
    public GameObject NicheLabel;
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        bool canRenderNiche = SelectedCompany.company.CompanyType == CompanyType.ProductCompany;

        ToggleNicheObjects(canRenderNiche);

        if (canRenderNiche)
            RenderNicheTab();
    }

    void ToggleNicheObjects(bool show)
    {
        NicheRoot.SetActive(show);
    }

    private void RenderNicheTab()
    {
        NicheType niche = SelectedCompany.product.Niche;

        NicheName.text = EnumFormattingUtils.GetFormattedNicheName(niche);

        LinkToNiche.SetNiche(niche);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}
