﻿using Assets.Classes;
using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public enum ProductCompanyGoals
{
    Survive,
    FixClientLoyalty,
    CompleteCompanyGoal,
    Develop,
    TakeTechLeadership
}

public partial class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {}

    GameEntity[] GetAIProducts()
    {
        return gameContext.GetEntities(
            GameMatcher
            .AllOf(GameMatcher.Product)
            .NoneOf(GameMatcher.ControlledByPlayer)
        );
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in GetAIProducts())
        {
            var goal = ChooseGoal(e);

            ExecuteGoal(goal, e);
        }
    }

    ProductCompanyGoals ChooseGoal(GameEntity product)
    {
        var goals = new Dictionary<ProductCompanyGoals, long>
        {
            // threats
            [ProductCompanyGoals.Survive] = GetBankruptcyUrgency(product),
            //[ProductCompanyGoals.FixClientLoyalty] = GetLoyaltyUrgency(product),

            // company goal
            [ProductCompanyGoals.CompleteCompanyGoal] = GetCompanyGoalUrgency(product),
        };
        
        return PickMostImportantValue(goals);
    }

    static T PickMostImportantValue<T> (Dictionary<T, long> values)
    {
        long value = 0;
        T goal = default;

        foreach (var pair in values)
        {
            if (pair.Value > value)
            {
                value = pair.Value;
                goal = pair.Key;
            }
        }

        return goal;
    }

    void ExecuteGoal(ProductCompanyGoals goal, GameEntity product)
    {
        switch (goal)
        {
            case ProductCompanyGoals.Survive: Survive(product); break;
            case ProductCompanyGoals.FixClientLoyalty: FixLoyalty(product); break;

            default: CompleteCompanyGoal(product); break;
        }
    }




    TestComponent GetLogs()
    {
        return gameContext.GetEntities(GameMatcher.Test)[0].test;
    }

    bool GetLog(LogTypes logTypes)
    {
        return GetLogs().logs[logTypes];
    }

    void Print(string action, GameEntity company)
    {
        var player = GetPlayerProductCompany();

        bool isMyCompany = company.isControlledByPlayer;
        bool isMyCompetitor = player != null && company.product.Niche == player.product.Niche;

        bool canRenderMyCompany = GetLog(LogTypes.MyProductCompany) && isMyCompany;
        bool canRenderMyCompetitors = GetLog(LogTypes.MyProductCompanyCompetitors) && isMyCompetitor;

        string companyName = company.company.Name;
        if (isMyCompany)
            companyName = Visuals.Colorize(company.company.Name, VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO);
        
        if (canRenderMyCompany || canRenderMyCompetitors)
            Debug.Log($"Goal: {ChooseGoal(company)}. {action} : {companyName}");
    }



    long GetSegmentLoyalty(GameEntity company, UserType userType)
    {
        return MarketingUtils.GetClientLoyalty(gameContext, company.company.Id, userType);
    }

    TeamResource GetResourceChange(GameEntity company)
    {
        return CompanyEconomyUtils.GetResourceChange(company, gameContext);
    }

    TeamResource GetConceptCost(GameEntity company)
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);
    }

    TeamResource GetSegmentCost(GameEntity company, UserType userType)
    {
        return ProductUtils.GetSegmentUpgradeCost(company, gameContext, userType);
    }

    GameEntity GetPlayerProductCompany()
    {
        return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.ControlledByPlayer, GameMatcher.Product))[0];
    }
}