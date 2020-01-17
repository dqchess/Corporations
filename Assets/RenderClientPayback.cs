﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClientPayback : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext);
        var income = Economy.GetUnitIncome(GameContext, product, 0);

        var period = ads / income;

        return period.ToString("0.00") + " months";
    }
}
