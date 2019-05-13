﻿using Assets.Utils;
using UnityEngine.UI;

public class NicheScreenView : View
    , IAnyCompanyListener
{
    public Text NicheName;
    public Text IndustryName;
    public MarketPotentialView MarketPotentialView;
    public Text AmountOfCompetitors;

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        RenderAmountOfCompanies();
    }

    void OnEnable()
    {
        Render();

        GetUniversalListener.AddAnyCompanyListener(this);
    }

    void RenderAmountOfCompanies()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);

        AmountOfCompetitors.text = "(" + NicheUtils.GetCompetitorsAmount(NicheType, GameContext) + ")";
    }

    void Render()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = "Niche: " + NicheType.ToString();
        IndustryName.text = "Is part of " + IndustryType.ToString() + " industry";

        MarketPotentialView.SetEntity(NicheType);
        RenderAmountOfCompanies();
    }
}
