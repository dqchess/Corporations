﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCapitalOrMarketShareLabel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var hasCompany = CompanyUtils.HasCompanyOnMarket(MyCompany, SelectedNiche, GameContext);

        if (hasCompany)
            return "Our market share";
        else
            return "Start Capital";
    }
}
