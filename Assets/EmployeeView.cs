﻿using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EmployeeView : MonoBehaviour {
    string GetSignedValue(int value)
    {
        if (value > 0)
            return "+" + value;

        return "" + value;
    }

    void RenderSkills(Human human)
    {
        GameObject Avatar = gameObject.transform.Find("Name").gameObject;
        Hint SkillsetHint = Avatar.GetComponent<Hint>();

        string hintText = String.Format(
            "          {3}         \n\n" +
            "<b>Management</b>  - {0} LVL \n" +
            "<b>Programming</b> - {1} LVL \n" +
            "<b>Marketing</b>   - {2} LVL \n",
            human.Skills.Management.Level,
            human.Skills.Programming.Level,
            human.Skills.Marketing.Level,
            human.GetLiteralSpecialisation()
        );

        SkillsetHint.SetHintObject(hintText);
    }

    void RenderEffeciency(Human human)
    {
        GameObject Effeciency = gameObject.transform.Find("Effeciency").gameObject;
        Effeciency.GetComponent<Text>().text = String.Format("+{0} points monthly", human.BaseProduction);
    }

    void RenderAvatar(Human human)
    {
        var avatar = gameObject.transform.Find("Avatar").GetComponentInChildren<WorkerAvatarView>();
        //avatar.RenderAvatar(human.Level, human.Specialisation);
    }

    void RenderName(Human human)
    {
        GameObject NameObject = gameObject.transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName + " \n " + human.Level + "lvl";
    }

    void RenderHireButton(int workerId, int projectId)
    {
        Button button = gameObject.transform.Find("Hire").gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate { BaseController.HireWorker(workerId, projectId); });
    }

    public void Render(Human human, int index, int projectId)
    {
        RenderName(human);
        RenderAvatar(human);
        RenderSkills(human);
        RenderEffeciency(human);

        RenderHireButton(index, projectId);
    }

    public void UpdateView(Human human, int index, Dictionary<string, object> parameters)
    {
        int projectId = (int)parameters["projectId"];

        Render(human, index, projectId);
    }
}
