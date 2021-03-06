﻿using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ProductCompanyTableView : View
{
    public SetAmountOfStars SetAmountOfStars;
    [SerializeField] Text AudienceGrowth;

    GameEntity entity;
    bool QuarterlyOrYearly;

    public void SetEntity(GameEntity company, bool quarterOrYearly)
    {
        entity = company;
        QuarterlyOrYearly = quarterOrYearly;

        Render();
    }

    public void Render()
    {
        if (entity == null)
            return;

        RenderAudienceGrowth();

        var niche = Markets.GetNiche(Q, entity.product.Niche);
        var rating = Markets.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);
    }

    void RenderAudienceGrowth()
    {
        var monthly = CompanyStatisticsUtils.GetAudienceGrowth(entity, 3);
        var yearly = CompanyStatisticsUtils.GetAudienceGrowth(entity, 12);

        var quarGrowth = monthly == 0 ? "???" : Format.Sign(monthly) + "%";
        var yrGrowth = yearly == 0 ? "???" : Format.Sign(yearly) + "%";

        AudienceGrowth.text = QuarterlyOrYearly ? quarGrowth : yrGrowth;


        //AudienceGrowth.text = $"{monGrowth} / {yrGrowth}";
    }
}
