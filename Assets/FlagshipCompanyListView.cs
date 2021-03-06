﻿using Assets.Core;
using System.Linq;
using UnityEngine;

public class FlagshipCompanyListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyViewOnMainScreen>().SetEntity(entity as GameEntity, false);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetDaughterCompanies(Q, MyCompany)
            .First(p => Companies.IsPlayerFlagship(Q, p));



        SetItems(new GameEntity[1] { flagship });
    }
}
