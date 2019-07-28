﻿using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static BonusContainer GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            return new BonusContainer("Product Competitiveness")
                .RenderTitle()
                .Append("Just some value", 5)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus);
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitivenessBonus(company, gameContext).Sum();
        }


        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorSegmentLevels(GameEntity e, GameContext context, UserType userType)
        {
            var names = GetPlayersOnMarket(context, e)
                .Select(c => c.product.Concept + "lvl - " + ProlongNameToNDigits(c.company.Name, 10));

            return names;
        }


        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetPlayersOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, GameEntity e)
        {
            return GetPlayersOnMarket(context, e.product.Niche);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, NicheType niche)
        {
            return context.GetEntities(GameMatcher.Product).Where(p => p.product.Niche == niche);
        }

        public static GameEntity[] GetPlayersOnMarket(GameContext context, NicheType niche, bool something)
        {
            return Array.FindAll(
                context.GetEntities(GameMatcher.Product),
                p => p.product.Niche == niche
                );
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, niche).Count();
        }
    }
}
