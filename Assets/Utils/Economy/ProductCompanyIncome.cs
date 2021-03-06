﻿using System;
using UnityEngine;

namespace Assets.Core
{
    static partial class Economy
    {
        internal static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            var segmentId = e.productPositioning.Positioning;
            float income = GetIncomeBySegment(context, e, segmentId);

            long result = 0;

            try
            {
                result = Convert.ToInt64(income);
            }
            catch
            {
                Debug.LogWarning("GetProductCompanyIncome " + Enums.GetFormattedNicheName(e.product.Niche) + " error " + e.company.Name);
            }

            return result * Balance.PERIOD / 30;
        }

        public static float GetImprovementMonetisationValue(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.Adverts:
                    return 1f;

                case Monetisation.Enterprise:
                    return 1f;

                case Monetisation.Paid:
                    return 4f;

                case Monetisation.Service:
                    return 5f;

                default:
                    return 1f;
            }
        }

        public static float GetBaseMonetisationValue(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.Adverts:
                    return 0.3f;

                case Monetisation.Enterprise:
                    return 0.7f;

                case Monetisation.Paid:
                    return 0.7f;

                case Monetisation.Service:
                    return 0.3f;

                default:
                    return 0.15f;
            }
        }

        public static float GetMonetisationModifier(GameContext gameContext, GameEntity c)
        {
            var niche = Markets.GetNiche(gameContext, c.product.Niche);

            var pricingType = niche.nicheBaseProfile.Profile.MonetisationType;

            var baseValue = GetBaseMonetisationValue(pricingType);
            var multiplier = GetImprovementMonetisationValue(pricingType);


            var improvements = c.features.features[ProductFeature.Monetisation];

            return baseValue + improvements * multiplier / 100;
        }

        internal static float GetIncomeBySegment(GameContext gameContext, GameEntity c, int segmentId)
        {
            if (c.isDumping)
                return 0;

            float unitIncome = GetUnitIncome(gameContext, c, segmentId);

            long clients = Marketing.GetClients(c);

            return clients * unitIncome;
        }

        public static float GetUnitIncome(GameContext gameContext, GameEntity c, int segmentId)
        {
            float price = GetBaseSegmentIncome(gameContext, c, segmentId);

            float monetisationModifier = GetMonetisationModifier(gameContext, c);

            return price * monetisationModifier;
        }

        internal static float GetBaseSegmentIncome(GameContext gameContext, GameEntity c, int segmentId)
        {
            return Markets.GetSegmentProductPrice(gameContext, c.product.Niche, segmentId);
        }
    }
}
