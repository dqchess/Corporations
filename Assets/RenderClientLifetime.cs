﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClientLifetime : ParameterView
{
    public override string RenderValue()
    {
        var lifetime = Marketing.GetLifeTime(Q, SelectedCompany.company.Id);

        return lifetime.ToString("0.00") + " months";
    }
}
