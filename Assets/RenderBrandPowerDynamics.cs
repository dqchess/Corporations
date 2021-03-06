﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBrandPowerDynamics : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var brand = (int)SelectedCompany.branding.BrandPower;
        var change = BrandPower.Sum();

        return $"{brand} ({Visuals.PositiveOrNegativeMinified(change)} monthly)";
    }

    Bonus<long> BrandPower => Marketing.GetBrandChange(SelectedCompany, Q);
}