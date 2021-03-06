﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideReleaseAppButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var p = SelectedCompany;
        return !Companies.IsReleaseableApp(p, Q);
    }
}
