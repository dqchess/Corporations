﻿using Assets.Core;

public class RenderCompanyValuation : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Money(Economy.GetCompanyCost(Q, SelectedCompany.company.Id));
    }
}
