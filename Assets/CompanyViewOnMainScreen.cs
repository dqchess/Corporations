﻿using Assets.Core;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    public Text Name;
    public Hint CompanyHint;
    public Text Clients;
    public LinkToProjectView LinkToProjectView;

    public Text PositionOnMarket;

    public Image Image;
    public Image DarkImage;

    public Text Profitability;

    public RenderConceptProgress ConceptProgress;

    public HireWorker HireWorker;
    public TestCampaignButton TestCampaignButton;
    public StartRegularAdCampaign StartRegularAdCampaign;
    public StartBrandingCampaign StartBrandingCampaign;
    public ReleaseApp ReleaseApp;
    public UpgradeProductImprovements UpgradeChurn;
    public UpgradeProductImprovements UpgradeMonetisation;

    public Text Expertise;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;
        EnableDarkTheme = darkImage;

        Render(true);
    }

    void Render(bool wasCompanyUpdated)
    {
        if (company == null)
            return;

        var id = company.company.Id;

        var clients = Marketing.GetClients(company);
        var churn = Marketing.GetChurnRate(Q, company);
        var churnClients = Marketing.GetChurnClients(Q, id);

        var profit = Economy.GetProfit(Q, id);

        bool hasControl = Companies.GetControlInCompany(MyCompany, company, Q) > 0;

        var nameColor = hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL;
        var profitColor = profit >= 0 ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE;

        var positionOnMarket = Markets.GetPositionOnMarket(Q, company) + 1;

        var expertise = Products.GetFreeImprovements(company);




        SetEmblemColor();

        Clients.text = Format.Minify(clients);

        //Clients.GetComponent<Hint>().SetHint(
        //    $"This product has {Format.Minify(clients)} clients\n"
        //    + Visuals.Negative($"We are losing {Format.Minify(churnClients)} each week.\nChurn rate is {churn}%")
        //    + $".\n\nUpgrade app to reduce this value"
        //    );
        CompanyHint.SetHint(GetCompanyHint());


        Expertise.text = $"Development\n(Expertise: {expertise})";

        Name.text = company.company.Name;
        Name.color = Visuals.GetColorFromString(nameColor);

        Profitability.text = Format.Money(profit);
        Profitability.color = Visuals.GetColorFromString(profitColor);

        PositionOnMarket.text = $"#{positionOnMarket}";

        // buttons

        // set
        LinkToProjectView.CompanyId = id;
        HireWorker.companyId = id;
        ReleaseApp.SetCompanyId(id);

        TestCampaignButton.SetCompanyId(id);
        StartRegularAdCampaign.SetCompanyId(id);
        StartBrandingCampaign.SetCompanyId(id);
        UpgradeChurn.SetCompanyId(id);
        UpgradeMonetisation.SetCompanyId(id);


        var max = Economy.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetAmountOfWorkers(company, Q);

        var targetingCost = Marketing.GetTargetingCampaignCost(company, Q);
        var brandingCost = Marketing.GetBrandingCampaignCost(company, Q);



        // enable / disable them
        UpdateIfNecessary(HireWorker, workers < max);
        UpdateIfNecessary(ReleaseApp, Companies.IsReleaseableApp(company, Q));

        UpdateIfNecessary(TestCampaignButton, !company.isRelease);
        UpdateIfNecessary(StartRegularAdCampaign, company.isRelease);
        UpdateIfNecessary(StartBrandingCampaign, company.isRelease);


        // render
        HireWorker.GetComponentInChildren<Text>().text = $"Hire Worker ({workers}/{max})";

        StartRegularAdCampaign.GetComponent<Hint>().SetHint($"Cost: {Format.Money(targetingCost)}");
        StartBrandingCampaign.GetComponent<Hint>().SetHint($"Cost: {Format.Money(brandingCost)}");
    }

    void UpdateIfNecessary(MonoBehaviour mb, bool condition)
    {
        if (mb.gameObject.activeSelf != condition)
            mb.gameObject.SetActive(condition);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render(false);
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }
    
    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);
    }

    string GetCompanyHint()
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(Q, company);

        // quality description
        var conceptStatus = Products.GetConceptStatus(company, Q);

        var concept = "???";

        switch (conceptStatus)
        {
            case ConceptStatus.Leader: concept = "Sets Trends!"; break;
            case ConceptStatus.Outdated: concept = "Outdated"; break;
            case ConceptStatus.Relevant: concept = "Relevant"; break;
        }

        //
        var level = Products.GetProductLevel(company);

        var clients = Marketing.GetClients(company);

        var brand = (int)company.branding.BrandPower;

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine($"Clients: {Format.Minify(clients)} (#{position + 1})");
        hint.AppendLine($"Brand: {brand}");

        hint.AppendLine();

        hint.AppendLine($"Concept: {level}LVL ({concept})");

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine(GetProfitDescription());

        return hint.ToString();
    }
}