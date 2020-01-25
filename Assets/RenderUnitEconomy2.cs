﻿using Assets.Core;
using UnityEngine.UI;

public class RenderUnitEconomy2 : View
{
    public Text EconomyDescription;
    public Text UnitEconomy;
    public Text Payback;
    public Text Lifetime;
    public Text MonetisationType;


    public override void ViewRender()
    {
        base.ViewRender();

        var product = SelectedCompany;

        var income = Economy.GetUnitIncome(GameContext, product, 0);

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext);

        var payback = ads / income;
        var lifetime = MarketingUtils.GetLifeTime(GameContext, product.company.Id);

        var paybackDescription = "=\nMarketing cost: " + ads.ToString("0.00");

        paybackDescription += "\n/\nMonthly income per user: " + income.ToString("0.00");

        Payback.text = payback.ToString("0.00") + " months";
        Payback.GetComponent<Hint>().SetHint(paybackDescription);

        var ROI = (int)(lifetime * 100 / payback);

        if (ROI > 100)
        {
            EconomyDescription.text = "ROI: " + ROI + "%";
            UnitEconomy.text = "Unit economy is " + Visuals.Positive("GOOD");
        }
        else
        {
            EconomyDescription.text = "lifetime < payback \nImprove your product!";
            UnitEconomy.text = "Unit economy is " + Visuals.Negative("BAD");
        }


        Lifetime.text = lifetime.ToString("0.00") + " months";

        var niche = Markets.GetNiche(GameContext, product);
        MonetisationType.text = Products.GetFormattedMonetisationType(niche);
    }
}