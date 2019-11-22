﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyResultView : View
{
    public LinkToNiche LinkToNiche;
    public Text CompanyName;
    public Text ClientGrowth;
    public Text MarketShareChange;
    public Text ConceptStatusText;
    public Text Profit;

    public ToggleMarketingFinancing ToggleMarketingFinancing;

    ProductCompanyResult result1;

    public void SetEntity(ProductCompanyResult result)
    {
        result1 = result;

        var product = CompanyUtils.GetCompanyById(GameContext, result.CompanyId);

        CompanyName.text = product.company.Name;

        DrawProductGrowth(product, result);
        DrawProductStatus();

        var profit = EconomyUtils.GetProfit(GameContext, result.CompanyId);
        Profit.text = "Profit\n" + Visuals.Colorize(Format.Money(profit), profit > 0);

        LinkToNiche.SetNiche(product.product.Niche);
        ToggleMarketingFinancing.SetCompanyId(result.CompanyId);
    }

    void DrawProductGrowth(GameEntity product, ProductCompanyResult result)
    {
        var growth = MarketingUtils.GetAudienceGrowthMultiplier(product, GameContext);
        var growthMultiplier = MarketingUtils.GetGrowthMultiplier(product, GameContext);
        ClientGrowth.text = "Client growth\n" + Visuals.PositiveOrNegativeMinified(growth) + "%";
        ClientGrowth.gameObject.GetComponent<Hint>().SetHint(growthMultiplier.ToString());

        var share = (long)result.MarketShareChange;
        MarketShareChange.text = "Market share\n" + Visuals.PositiveOrNegativeMinified(share) + "%";
    }

    void DrawProductStatus()
    {
        var conceptStatus = result1.ConceptStatus;
        var color = GetStatusColor(conceptStatus);

        CooldownUtils.TryGetCooldown(GameContext, new CooldownImproveConcept(result1.CompanyId), out Cooldown c1);

        var days = 0;
        if (c1 != null)
            days = c1.EndDate - CurrentIntDate;

        ConceptStatusText.text = Visuals.Colorize(conceptStatus.ToString(), color) + $"\nUpgrades in {days}d";
    }

    public override void ViewRender()
    {
        base.ViewRender();

        DrawProductStatus();
    }

    string GetStatusColor (ConceptStatus conceptStatus)
    {
        if (conceptStatus == ConceptStatus.Leader)
            return VisualConstants.COLOR_POSITIVE;

        if (conceptStatus == ConceptStatus.Outdated)
            return VisualConstants.COLOR_NEGATIVE;

        return VisualConstants.COLOR_NEUTRAL;
    }
}
