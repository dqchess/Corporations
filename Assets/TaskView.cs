﻿using Assets.Utils;
using Assets.Utils.Formatting;
using Assets.Utils.Tutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : View
{
    public Text Text;

    public void SetEntity(TaskComponent task)
    {
        var text = GetTaskHeader(task.CompanyTask) + "\n\n";

        var remaining = task.EndTime - CurrentIntDate;



        if (remaining <= 0)
        {
            text += "DONE";

            var taskString = GetTaskString(task.CompanyTask);
            if (TutorialUtils.IsOpenedFunctionality(GameContext, taskString))
            {
                AddIfAbsent<Blinker>();

                var asd = AddIfAbsent<TutorialUnlocker>();
                asd.SetEvent(taskString);
            }
            else
                RemoveIfExists<Blinker>();
        }
        else
            text += "Finished in\n" + remaining + " days";

        Text.text = text;


        AddLinkToObservableObject(task.CompanyTask);
    }

    private void AddLinkToObservableObject(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.ExploreMarket:
                AddIfAbsent<LinkToNiche>().SetNiche((companyTask as CompanyTaskExploreMarket).NicheType);
                break;

            case CompanyTaskType.ExploreCompany:
                AddIfAbsent<LinkToProjectView>().CompanyId = ((companyTask as CompanyTaskExploreCompany).CompanyId);
                break;
        }
    }

    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\n" + CompanyUtils.GetCompanyById(GameContext, (companyTask as CompanyTaskAcquisition).CompanyId).company.Name;

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            case CompanyTaskType.ExploreCompany:
                return "Exploring company\n" + CompanyUtils.GetCompanyById(GameContext, (companyTask as CompanyTaskExploreCompany).CompanyId).company.Name;

            default: return "UNKNOWN TASK!!!!" + companyTask.CompanyTaskType;
        }
    }

    string GetTaskString(CompanyTask companyTask)
    {
        var text = companyTask.CompanyTaskType.ToString();

        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return text + (companyTask as CompanyTaskAcquisition).CompanyId;

            case CompanyTaskType.ExploreMarket:
                return text + (companyTask as CompanyTaskExploreMarket).NicheType;

            case CompanyTaskType.ExploreCompany:
                return text + (companyTask as CompanyTaskExploreCompany).CompanyId;

            default: return "UNKNOWN TASK!!!!" + text;
        }

    }
}


public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,
    AcquiringParlay,


}

public class CompanyTask
{
    public CompanyTaskType CompanyTaskType;
}

public class CompanyTaskAcquisition : CompanyTask
{
    public int CompanyId;

    public CompanyTaskAcquisition(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.AcquiringCompany;
    }
}

public class CompanyTaskExploreMarket : CompanyTask
{
    public NicheType NicheType;

    public CompanyTaskExploreMarket(NicheType nicheType)
    {
        CompanyTaskType = CompanyTaskType.ExploreMarket;
        NicheType = nicheType;
    }
}

public class CompanyTaskExploreCompany : CompanyTask
{
    public int CompanyId;

    public CompanyTaskExploreCompany(int companyId)
    {
        CompanyTaskType = CompanyTaskType.ExploreCompany;
        CompanyId = companyId;
    }
}
