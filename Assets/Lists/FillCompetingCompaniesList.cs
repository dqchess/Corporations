﻿using Assets.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

public class FillCompetingCompaniesList : View
    , IMenuListener
    , IAnyCompanyListener
{
    GameEntity[] GetProductsOnNiche()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);

        var list = NicheUtils.GetPlayersOnMarket(GameContext, niche).ToArray();

        Array.Sort(list, SortCompanies);

        //string names = String.Join(",", list.Select(e => e.product.Name).ToArray());

        //Debug.Log("Rendering companies: " + names);

        return list;
    }

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void OnEnable()
    {
        GetUniversalListener.AddAnyCompanyListener(this);

        Render();
    }

    int SortCompanies(GameEntity p1, GameEntity p2)
    {
        if (p1.isControlledByPlayer)
            return -1;

        if (p2.isControlledByPlayer)
            return 1;

        if (p1.product.ProductLevel == 0)
            return -1;

        if (p2.product.ProductLevel == 0)
            return 1;

        return 0;
    }

    void Render()
    {
        GetComponent<CompetingCompaniesListView>().SetItems(GetProductsOnNiche());
    }

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        if (screenMode == ScreenMode.NicheScreen)
            Render();
    }
}