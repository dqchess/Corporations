﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderGrowth : UpgradedParameterView
{
    public bool Yearly;
    public bool Monthly;

    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        int duration = 3;

        if (Monthly)
            duration = 1;
        else if (Yearly)
            duration = 12;

        var growth = CompanyUtils.GetValuationGrowth(SelectedCompany, duration);

        return Visuals.Sign(growth) + "%";
    }
}