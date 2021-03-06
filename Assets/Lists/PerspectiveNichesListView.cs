﻿using Assets.Core;
using UnityEngine;

public class PerspectiveNichesListView : ListView
{
    public bool AdjacentNichesOnly;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NichePreview>().SetNiche(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var perspectiveNiches = Markets.GetPlayableNiches(Q);
            //AdjacentNichesOnly ?
            //    NicheUtils.GetPerspectiveAdjacentNiches(GameContext, MyCompany)
            //    :
            //    NicheUtils.GetPerspectiveNiches(GameContext);

        SetItems(perspectiveNiches);
    }
}
