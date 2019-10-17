﻿using System.Collections.Generic;
using Assets.Utils;
using System.Linq;
using UnityEngine;

public partial class AIManageGroupSystems
{
    public void ExpandSphereOfInfluence(GameEntity group)
    {
        CheckNiches(group);
    }

    void CheckNiches(GameEntity group)
    {
        if (IsCompanyReadyToExpand(group))
            AddRandomNiche(group);
    }



    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity product)
    {
        return EconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product);
    }

    bool IsCompanyReadyToExpand(GameEntity group)
    {
        foreach (var n in group.companyFocus.Niches)
        {
            Debug.Log("Checking niche " + n.ToString());
            foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
            {
                Debug.Log("Checking holding " + holding.company.Name);

                if (!holding.hasProduct)
                    continue;

                if (IsCompanyNeedsMoreMoneyOnMarket(holding))
                    return false;
            }
        }

        return true;
    }

    void AddRandomNiche(GameEntity group)
    {
        //Debug.Log("Search Suitable Niche: " + group.company.Name);

        var industry = group.companyFocus.Industries[0];

        var playableNiches = NicheUtils.GetPlayableNichesInIndustry(industry, gameContext);
        var profit = EconomyUtils.GetProfit(group, gameContext);

        var averageProfit = profit / (CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id).Count() + 1);

        var suitableNiches = playableNiches
            .Where(n => CompanyUtils.IsEnoughResources(group, 10 * NicheUtils.GetStartCapital(n))) // can start business and hold for a while
            .Where(n => !CompanyUtils.IsInSphereOfInterest(group, n.niche.NicheType)) // exclude niches, that we cover already
            .Where(n => NicheUtils.GetLowestIncomeOnMarket(gameContext, n) > averageProfit / 2) // is profitable niche
            //.Where(n => NicheUtils.GetBiggestIncomeOnMarket(gameContext, n) > averageProfit) // can compete with current products
            .ToArray();

        var count = suitableNiches.Count();

        if (count == 0)
            return;

        var rand = Random.Range(0, count);

        //var niche = RandomEnum<NicheType>.GenerateValue();
        var niche = suitableNiches[rand].niche.NicheType;

        CompanyUtils.AddFocusNiche(niche, group, gameContext);
    }
}
