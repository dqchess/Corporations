﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectView : View, IMenuListener
{
    public Text CompanyName;
    public Text CompanyTypeLabel;
    public LinkToCompanyPreview LinkToCompanyPreview;
    public GameObject ShareholderPreviewPrefab;
    public GameObject ShareholderContainer;

    public LinkToNiche LinkToNiche;
    public GameObject NicheLabel;
    public Text NicheName;
    public GameObject NicheExpectations;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void SetCompanyId()
    {
        LinkToCompanyPreview.CompanyId = SelectedCompany.company.Id;
    }

    //void Update()
    //{
    //    Render();
    //}

    void Render()
    {
        CompanyName.text = SelectedCompany.company.Name;

        CompanyTypeLabel.text = SelectedCompany.company.CompanyType.ToString();

        RenderPerspectives();

        SetCompanyId();

        RenderShareholders(GetShareholders(), ShareholderContainer);
    }

    void RenderPerspectives()
    {
        if (SelectedCompany.company.CompanyType == CompanyType.ProductCompany)
            RenderNicheTab();
        else
            RenderCorporateGroup();
    }

    void ToggleNicheObjects(bool show)
    {
        NicheLabel.SetActive(show);
        NicheExpectations.SetActive(show);
        NicheName.gameObject.SetActive(show);
    }

    void ToggleCorporateGroupObjects(bool show)
    {

    }

    private void RenderCorporateGroup()
    {
        ToggleNicheObjects(false);
        ToggleCorporateGroupObjects(true);
    }

    private void RenderNicheTab()
    {
        ToggleCorporateGroupObjects(false);
        ToggleNicheObjects(true);

        NicheType niche = SelectedCompany.product.Niche;

        LinkToNiche.SetNiche(niche);

        NicheName.text = niche.ToString();
        NicheName.gameObject.GetComponent<LinkToNiche>().SetNiche(niche);
    }

    Dictionary<int, int> GetShareholders()
    {
        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        if (SelectedCompany.hasShareholders)
            shareholders = SelectedCompany.shareholders.Shareholders;

        return shareholders;
    }

    int GetTotalShares(Dictionary<int, int> shareholders)
    {
        int totalShares = 0;

        foreach (var e in shareholders)
            totalShares += e.Value;

        return totalShares;
    }

    void RemoveInstances(int amount, GameObject Container)
    {
        // Remove useless gameobjects
        for (var i = 0; i < amount; i++)
            Destroy(Container.transform.GetChild(Container.transform.childCount - 1).gameObject);
    }

    void SpawnInstances(int amount, GameObject Container)
    {
        for (var i = 0; i < amount; i++)
            Instantiate(ShareholderPreviewPrefab, Container.transform, false);
    }

    void ProvideEnoughInstances(Dictionary<int, int> list, GameObject Container)
    {
        int childCount = Container.transform.childCount;

        if (list.Count < childCount)
            RemoveInstances(childCount - list.Count, Container);
        else
            SpawnInstances(list.Count - childCount, Container);
    }

    void RenderShareholders(Dictionary<int, int> shareholders, GameObject Container)
    {
        int index = 0;

        int totalShares = GetTotalShares(shareholders);

        ProvideEnoughInstances(shareholders, Container);

        foreach (var e in shareholders)
        {
            Container.transform.GetChild(index)
                .GetComponent<ShareholderPreviewView>()
                .SetEntity(e.Key, e.Value, totalShares);

            index++;
        }
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Debug.Log("OnMenu Check in ProjectView.cs " + screenMode.ToString());
        if (screenMode == ScreenMode.ProjectScreen)
        {
            Debug.Log("render projectScreen");

            Render();
        }
    }
}
