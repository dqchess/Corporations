﻿using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            ManageProductDevelopment(e);

        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            Operate(e);
    }
}