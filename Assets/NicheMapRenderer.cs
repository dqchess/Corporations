﻿using Assets.Core;
using Assets.Visuals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NicheMapRenderer : View
{
    Dictionary<NicheType, GameObject> niches = new Dictionary<NicheType, GameObject>();
    Dictionary<int, GameObject> companies = new Dictionary<int, GameObject>();

    public GameObject NichePrefab;
    public GameObject CompanyPrefab;

    NicheType currNiche;

    [HideInInspector]
    internal float CompanyRadius = 165f;

    public override void ViewRender()
    {
        Render();
    }

    void Render()
    {
        var baseForIndustry = new Vector3(350, -350); // Rendering.GetPointPositionOnCircle(0, 1, 0, 1);

        if (currNiche != SelectedNiche)
        {
            foreach (var n in niches)
                Destroy(n.Value);

            foreach (var c in companies)
                Destroy(c.Value);

            niches.Clear();
            companies.Clear();

            currNiche = SelectedNiche;
        }

        RenderMarket(SelectedNiche, 0, 1, baseForIndustry);

        // clean dead companies
        foreach (var c in companies)
            c.Value.SetActive(Companies.Get(Q, c.Key).isAlive);
    }

    void RenderMarket(NicheType niche, int index, int marketCount, Vector3 industryPosition)
    {
        GameObject m = GetMarketObject(niche);

        UpdateMarketPosition(m, index, marketCount, industryPosition, niche);

        m.GetComponent<MarketShareView>().SetEntity(niche);

        var isPlayable = Markets.IsPlayableNiche(Q, niche);
        m.SetActive(isPlayable);

        RenderCompanies(niche, m);
    }

    GameEntity[] GetProductsOnMarket (NicheType niche)
    {
        var marketWasResearched = Markets.IsExploredMarket(Q, niche);

        if (!marketWasResearched)
            return new GameEntity[0];

        return Markets.GetProductsOnMarket(Q, niche)
            //.OrderByDescending(p => ProductUtils.GetProductLevel(p) * 1000 + CompanyUtils.GetCompanyExpertise(p))
            .OrderByDescending(Marketing.GetClients)
            .ToArray();

    }

    void RenderCompanies(NicheType niche, GameObject m)
    {
        var competitors = GetProductsOnMarket(niche);

        var marketPosition = m.transform.localPosition;

        var count = competitors.Length;
        for (var i = 0; i < count; i++)
            RenderCompany(competitors[i], i, count, marketPosition);
    }

    void RenderCompany(GameEntity company, int index, int amount, Vector3 marketPosition)
    {
        GameObject c = GetCompanyObject(company.company.Id);

        UpdateCompanyPosition(c, index, amount, marketPosition, company);

        c.GetComponent<CompanyViewOnMap>().SetEntity(company, true);
    }

    GameObject GetMarketObject(NicheType niche)
    {
        if (!niches.ContainsKey(niche))
            niches[niche] = Instantiate(NichePrefab, transform);

        return niches[niche];
    }

    GameObject GetCompanyObject(int companyId)
    {
        if (!companies.ContainsKey(companyId))
        {
            companies[companyId] = Instantiate(CompanyPrefab, transform);
            //companies[companyId].AddComponent<EnlargeOnAppearance>();
        }

        return companies[companyId];
    }



    float GetMarketScale(NicheType niche)
    {
        var marketSize = Markets.GetMarketSize(Q, niche);

        if (marketSize < 0)
            marketSize = 1000;

        return Mathf.Clamp(Mathf.Log10(marketSize) / 4f, 0.8f, 2.5f);
    }


    // set transforms
    void UpdateMarketPosition(GameObject m, int index, int marketCount, Vector3 basePosition, NicheType niche)
    {
        var scale = GetMarketScale(niche);

        m.transform.localScale = new Vector3(scale, scale, 1);
        m.transform.localPosition = basePosition;
            //Rendering.GetPointPositionOnCircle(index, marketCount, NicheRadius, 1) 
    }

    void UpdateCompanyPosition(GameObject c, int index, int count, Vector3 basePosition, GameEntity startup)
    {
        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(startup, Q) / 100f;
        //var share = startup.branding.BrandPower / 100f;

        var min = 0.55f;
        var max = 1.55f;
        var k = max - min;

        var scale = min + k * share; // GetMarketScale(niche);

        var marketScale = 1f; // GetMarketScale(startup.product.Niche) - 0.9f;

        c.transform.localScale = new Vector3(scale, scale, 1);
        c.transform.localPosition = Rendering.GetPointPositionOnCircle(index, count, CompanyRadius + marketScale * 25f, 1, 1.05f * Mathf.PI) + basePosition;
    }
}
