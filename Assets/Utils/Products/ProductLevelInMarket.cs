﻿using Assets.Classes;

public enum ConceptStatus
{
    Leader,
    Relevant,
    Outdated
}

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetMarketDemand(GameEntity niche)
        {
            return niche.segment.Segments[UserType.Core];
        }

        public static int GetMarketDemand(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            return GetMarketDemand(niche);
        }

        // always positive or equal to zero
        public static int GetDifferenceBetweenMarketDemandAndAppConcept(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var demand = GetMarketDemand(niche);
            var level = GetProductLevel(product);

            return demand - level;
        }


        public static bool IsInMarket(GameEntity product, GameContext gameContext)
        {
            return GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext) == 0;
        }

        public static bool IsOutOfMarket(GameEntity product, GameContext gameContext)
        {
            return GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext) > 0;
        }

        public static bool IsWillInnovate(GameEntity product, GameContext gameContext)
        {
            return IsInMarket(product, gameContext);
        }


        internal static ConceptStatus GetConceptStatus(GameEntity product, GameContext gameContext)
        {
            var isRelevant = IsInMarket(product, gameContext);
            var isOutdated = IsOutOfMarket(product, gameContext);

            if (product.isTechnologyLeader)
                return ConceptStatus.Leader;

            if (isOutdated)
                return ConceptStatus.Outdated;

            return ConceptStatus.Relevant;
        }
    }
}
