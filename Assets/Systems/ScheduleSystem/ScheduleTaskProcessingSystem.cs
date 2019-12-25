﻿using System.Collections.Generic;
using Entitas;

public class ScheduleTaskProcessingSystem : ReactiveSystem<GameEntity>
{
    private Contexts contexts;

    public ScheduleTaskProcessingSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    void ProcessTasks(DateComponent date)
    {
        //GameEntity[] tasks = contexts.game.GetEntities(GameMatcher.Task);

        //foreach (var t in tasks)
        //{
        //    if (date.Date >= t.task.EndTime)
        //    {
        //        t.ReplaceTask(false, t.task.CompanyTask, t.task.StartTime, t.task.Duration, t.task.EndTime);
        //        // TODO: play sounds for some taskTypes maybe??
        //    }
        //}
    }

    protected override void Execute(List<GameEntity> entities)
    {
        ProcessTasks(entities[0].date);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}