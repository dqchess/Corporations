﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductOverview : View
{
    public Text DevelopmentSpeed;
    public Text Expertise;
    public SetAmountOfStars TeamSpeed;

    public Text Popularity;
    public Text AppQuality;

    public Text PositioningLabel;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!SelectedCompany.hasProduct)
            return;

        var canShowData = Companies.IsExploredCompany(Q, SelectedCompany);

        RenderCommonInfo();
    }

    void RenderCommonInfo()
    {
        var position = Markets.GetPositionOnMarket(Q, SelectedCompany) + 1;
        Popularity.text = $"#{position}";


        //var quality = Products.GetProductLevel(SelectedCompany);
        //var demand =  Products.GetMarketDemand(SelectedCompany, GameContext);
        //var status =  Products.GetConceptStatus(SelectedCompany, GameContext);

        //AppQuality.text = $"Concept: {quality} / {demand} ({status})";


        //var posTextual = Markets.GetCompanyPositioning(SelectedCompany, GameContext);
        //PositioningLabel.text = "Positioning: " + posTextual;
    }
}
