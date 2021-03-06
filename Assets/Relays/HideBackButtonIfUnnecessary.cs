﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBackButtonIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        //var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id);

        //return daughters 

        var h = ScreenUtils.GetNavigationHistory(Q);
        var count = h.navigationHistory.Queries.Count;

        //Debug.Log("History Count: " + count);

        return !ScreenUtils.IsCanNavigateBack(Q);
    }
}
