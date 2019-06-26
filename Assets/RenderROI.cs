﻿using Assets.Utils;

public class RenderROI : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Sign(CompanyEconomyUtils.GetBalanceROI(SelectedCompany, GameContext)) + "%";
    }
}
