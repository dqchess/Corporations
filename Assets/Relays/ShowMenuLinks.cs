﻿using Assets.Core;

public class ShowMenuLinks : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughterCompanies(Q, MyCompany.company.Id).Length == 0;
    }
}
