﻿using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }

        public static BonusContainer GetInnovationChanceDescription(GameEntity company, GameContext gameContext)
        {
            var morale = company.team.Morale;

            var moraleChance = morale / 5; // 0...20
            var expertiseChance = Mathf.Clamp(company.expertise.ExpertiseLevel, 0, 25);

            var crunch = company.isCrunching ? 10 : 0;


            var sphereOfInterestBonus = 0;

            if (!company.isIndependentCompany)
            {
                var parent = CompanyUtils.GetParentCompany(gameContext, company);

                if (parent != null)
                {
                    if (CompanyUtils.IsInSphereOfInterest(parent, company.product.Niche))
                        sphereOfInterestBonus = 5;
                }
            }

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var phase = NicheUtils.GetMarketState(niche);
            var marketStage = CompanyUtils.GetMarketStageInnovationModifier(niche);


            var leaderBonus = GetLeaderInnovationBonus(company);

            return new BonusContainer("Innovation chance")
                .Append("Base", 5)
                //.Append("Morale", moraleChance)
                .Append("CEO bonus", leaderBonus)
                .Append("Market stage " + CompanyUtils.GetMarketStateDescription(phase), marketStage)
                .AppendAndHideIfZero("Is fully focused on market", company.isIndependentCompany ? 5 : 0)
                .AppendAndHideIfZero("Parent company focuses on this company market", sphereOfInterestBonus)
                .AppendAndHideIfZero("Crunch", crunch);
            //.Append("Expertise", expertiseChance);
        }
        public static int GetInnovationChance(GameEntity company, GameContext gameContext)
        {
            var chance = GetInnovationChanceDescription(company, gameContext);

            return (int)chance.Sum();
        }

        public static int GetLeaderInnovationBonus(GameEntity company)
        {
            //var CEOId = 
            int companyId = company.company.Id;
            int CEOId = CompanyUtils.GetCEOId(company);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * CompanyUtils.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }

        internal static long GetMarketStageInnovationModifier(GameEntity company, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);

            return CompanyUtils.GetMarketStageInnovationModifier(niche);
        }
    }
}
