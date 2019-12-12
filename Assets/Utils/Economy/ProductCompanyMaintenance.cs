﻿using Assets.Utils.Formatting;
using UnityEngine;

namespace Assets.Utils
{
    partial class Economy
    {
        // resulting costs
        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var devFinancing = GetDevelopmentCost(e, gameContext);
            var marketingFinancing = GetMarketingCost(e, gameContext);

            return devFinancing + marketingFinancing;
        }

        public static long GetMarketingCost(GameEntity e, GameContext gameContext) => GetMarketingCost(e, gameContext, GetMarketingFinancingCostMultiplier(e));
        public static long GetMarketingCost(GameEntity e, GameContext gameContext, float financing)
        {
            var gainedClients = MarketingUtils.GetAudienceGrowth(e, gameContext);
            var acquisitionCost = Markets.GetClientAcquisitionCost(e.product.Niche, gameContext);

            var result = gainedClients * acquisitionCost * financing;

            return (long)result;
        }


        public static int GetAmountOfWorkers(GameEntity e, GameContext gameContext)
        {
            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            return (int)Mathf.Pow(complexity, concept / 6f);
        }

        public static long GetDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var baseCost = Markets.GetBaseDevelopmentCost(e.product.Niche, gameContext);

            var workers = GetAmountOfWorkers(e, gameContext);

            return baseCost * workers;
        }
    }
}
