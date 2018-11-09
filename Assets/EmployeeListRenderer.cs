﻿using Assets.Classes;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeListRenderer : ListRenderer
{
    public override int itemsPerLine
    {
        get { return 3; }
        set { }
    }

    public override Vector2 spacing
    {
        get { return new Vector2(300f, 175f); }
        set { }
    }

    public override void RenderObject(GameObject obj, object item, int index, Dictionary<string, object> parameters)
    {
        obj.GetComponent<EmployeeView>().UpdateView((Human)item, index, parameters);
    }
}