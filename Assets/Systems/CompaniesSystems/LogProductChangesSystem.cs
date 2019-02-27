﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class LogProductChangesSystem : ReactiveSystem<GameEntity>
{
    private Contexts contexts;

    public LogProductChangesSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            Debug.Log($"{entity.product.Name}.ProductLevel updated to {entity.product.ProductLevel}");
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasProduct && entity.product.Id == 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Product);
    }
}

