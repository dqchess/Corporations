﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HideCaptureMarketButtonIfHaveNoCompaniesOnThisMarket : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasReleasedCompaniesOnMarket = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, Q).Count(c => c.isRelease) > 0;

        return !hasReleasedCompaniesOnMarket;
    }
}
