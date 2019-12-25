﻿using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public partial class UpdateNicheStateSystem : OnMonthChange, IInitializeSystem
{
    public UpdateNicheStateSystem(Contexts contexts) : base(contexts)
    {
    }

    void IInitializeSystem.Initialize()
    {
        CheckNiches();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        CheckNiches();
    }

    void CheckNiches()
    {
        var niches = Markets.GetNiches(gameContext);

        foreach (var n in niches)
            CheckNiche(n);
    }

    void CheckNiche(GameEntity niche)
    {
        var phase = Markets.GetMarketState(niche);

        if (phase == MarketState.Death)
            return;

        ActivateIfNecessary(niche);

        if (Markets.GetCompetitorsAmount(niche.niche.NicheType, gameContext) == 0)
            return;

        //var value = Random.Range(0, 1f);

        if (IsNeedsPromotion(niche)) //  && value > 0.9f
            PromoteNiche(niche);
        else
            DecrementDuration(niche);
    }


    void ActivateIfNecessary(GameEntity niche)
    {
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        var state = Markets.GetMarketState(niche);
        var nicheStartDate = niche.nicheLifecycle.OpenDate;

        if (date > nicheStartDate && state == MarketState.Idle)
        {
            //Debug.Log($"Awake niche from idle {state} {date}: niche start date={nicheStartDate}");
            PromoteNiche(niche);
        }
    }

    void PromoteNiche(GameEntity niche)
    {
        Markets.PromoteNicheState(niche);

        var player = Companies.GetPlayerCompany(gameContext);

        if (player == null)
            return;

        if (!Companies.IsInSphereOfInterest(player, niche.niche.NicheType))
            return;

        var popup = new PopupMessageMarketPhaseChange(niche.niche.NicheType);

        NotificationUtils.AddPopup(gameContext, popup);
    }


    bool IsNeedsPromotion(GameEntity niche)
    {
        var duration = niche.nicheState.Duration;

        var phase = Markets.GetMarketState(niche);

        if (phase == MarketState.Death || phase == MarketState.Idle)
            return false;

        return duration <= 0;
    }

    void DecrementDuration(GameEntity niche)
    {
        var state = niche.nicheState;

        niche.ReplaceNicheState(state.Phase, Mathf.Max(state.Duration - 1, 0));
    }
}