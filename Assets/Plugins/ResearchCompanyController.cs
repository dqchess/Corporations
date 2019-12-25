﻿using Assets.Utils;

public class ResearchCompanyController : ButtonController
{
    public override void Execute()
    {
        CooldownUtils.AddTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id), 8);
    }
}