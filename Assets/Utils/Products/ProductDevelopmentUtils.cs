﻿using Assets.Classes;

public enum DevelopmentFocus
{
    Concept,

    // loyalty
    Segments, 
    BugFixes
}

namespace Assets.Utils
{
    public static class ProductDevelopmentUtils
    {
        public static TeamResource GetDevelopmentCost(GameEntity e, GameContext context)
        {
            return new TeamResource(BaseDevCost(e, context), 0, 0, BaseIdeaCost(e, context), 0);
        }

        public static TeamResource GetSegmentImprovementCost(GameEntity e, GameContext gameContext)
        {
            var devCost = GetDevelopmentCost(e, gameContext);

            int multiplier = 3;

            return new TeamResource(
                devCost.programmingPoints / multiplier,
                devCost.programmingPoints / multiplier,
                devCost.managerPoints / multiplier,
                devCost.ideaPoints / multiplier,
                0);
        }

        internal static void ToggleDevelopment(GameContext gameContext, int companyId, DevelopmentFocus developmentFocus)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            if (CooldownUtils.HasCooldown(c, CooldownType.ProductFocus))
                return;

            c.ReplaceDevelopmentFocus(developmentFocus);

            CooldownUtils.AddCooldown(gameContext, c, CooldownType.ProductFocus, 90);
        }

        // niche based values
        public static int BaseIdeaCost(GameEntity e, GameContext gameContext)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, e.product.Niche).nicheCosts;

            int baseIdeaCost = costs.IdeaCost;

            return baseIdeaCost;
        }

        public static int BaseDevCost(GameEntity e, GameContext gameContext)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, e.product.Niche).nicheCosts;

            int baseDevCost = costs.TechCost;

            return baseDevCost;
        }
    }
}