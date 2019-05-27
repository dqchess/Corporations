﻿using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartTestCampaign(GameContext gameContext, GameEntity company)
        {
            CompanyUtils.AddCooldown(gameContext, company, CooldownType.TestCampaign, 45);

            var marketing = company.marketing;

            var clientGain = GetTestCampaignClientGain(gameContext, company);
            marketing.Segments[UserType.Core] += clientGain;

            company.ReplaceMarketing(marketing.BrandPower, marketing.Segments);


            var resources = GetTestCampaignCost(gameContext, company);

            Debug.Log("Spend Test campaign resources");
        }

        public static long GetTestCampaignClientGain(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            return costs.ClientBatch / 4;
        }

        public static TeamResource GetTestCampaignCost(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            var marketingCost = costs.MarketingCost;

            return new TeamResource(0, 0, marketingCost, 0, 0);
        }
    }
}
