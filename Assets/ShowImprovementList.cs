﻿using Assets.Core;

public class ShowImprovementList : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var growth = Marketing.GetAudienceGrowth(SelectedCompany, Q);

        var churn = Marketing.GetChurnClients(Q, SelectedCompany.company.Id);

        //var text = "Audience grows by " + Format.Minify(clients) + " clients each month due to current brand power and concept level";
        var text = $"This product will {Visuals.Positive("receive")} approximately {Format.Minify(growth)} clients next month." +
            $"\n\nDue to churn they will {Visuals.Negative("lose")} {Format.Minify(churn)} clients." +
            $"\n\nThis values are based on brand power, product relevance"
            ;

        return text;
    }
}
