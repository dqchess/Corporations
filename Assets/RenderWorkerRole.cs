﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderWorkerRole : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var Link = GetComponent<LinkToProjectView>();

        var text = "";
        var human = SelectedHuman;

        var role = Humans.GetRole(human);

        var formattedRole = Humans.GetFormattedRole(role);
        var rating = Humans.GetRating(Q, human);

        text = $"{formattedRole} ({rating}LVL)";

        if (!human.hasWorker || human.worker.companyId == -1)
        {
            text += " (Unemployed)";
            Link.enabled = false;
        }
        else
        {
            var companyId = human.worker.companyId;

            var c = Companies.Get(Q, companyId);

            text += " " + Visuals.Link($"in {c.company.Name}");

            Link.enabled = true;
            Link.CompanyId = companyId;
        }

        GetComponent<Text>().text = text;
    }
}
