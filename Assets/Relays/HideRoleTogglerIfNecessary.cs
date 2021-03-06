﻿using Assets.Core;

public class HideRoleTogglerIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var myCompany = MyCompany.company.Id;

        return true;

        bool worksInMyCompany = Humans.IsWorksInCompany(SelectedHuman, myCompany);

        return !worksInMyCompany;
    }
}
