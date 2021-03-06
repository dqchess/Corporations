﻿using Assets.Core;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

class MoraleManagementSystem : OnMonthChange
{
    public MoraleManagementSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = contexts.game.GetEntities(GameMatcher.Product);

        for (var i = 0; i < products.Length; i++)
        {
            var change = products[i].isCrunching ? -4 : 2;

            var workers = Teams.GetAmountOfWorkers(products[i], gameContext) + 1;
            var required = Products.GetNecessaryAmountOfWorkers(products[i], gameContext);

            if (required > workers)
                change -= required / workers;


            products[i].team.Morale = Mathf.Clamp(products[i].team.Morale + change, 0, 100);
        }
    }
}
