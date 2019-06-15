﻿using Assets.Utils;
using UnityEngine.UI;

public class CheckConceptUpgradeInteractability : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var cost = ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity, GameContext);

        GetComponent<Button>().interactable = CompanyUtils.IsEnoughResources(MyProductEntity, cost);
    }
}