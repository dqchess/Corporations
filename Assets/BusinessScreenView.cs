﻿using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BusinessScreenView : View
{
    public GameObject Companies;
    public GameObject CompnayPrefab;
    public Text CompanyNameLabel;

    // Start is called before the first frame update
    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void RenderSelectedCompanyName()
    {
        CompanyNameLabel.text = SelectedCompany.company.Name;
    }

    void Render()
    {
        RenderSelectedCompanyName();

        int index = 0;

        var companies = GameContext.GetEntities(GameMatcher.Company);

        foreach (var e in companies)
        {
            Transform c;

            if (index < Companies.transform.childCount - 2)
                c = Companies.transform.GetChild(index);
            else
            {
                c = Instantiate(CompnayPrefab, Companies.transform, false).transform;
                c.SetSiblingIndex(index);
            }

            c.gameObject.GetComponent<CompanyPreviewView>().SetEntity(e);
            index++;
        }
    }

    void Update()
    {
        Render();
    }
}
