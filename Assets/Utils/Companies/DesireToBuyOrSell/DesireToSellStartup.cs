﻿using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSellStartup(GameEntity startup, GameContext gameContext)
        {
            var shareholders = startup.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                desireToSell += GetDesireToSellStartupByInvestorType(startup, block.InvestorType, invId, gameContext) * block.amount;
                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }

        public static long GetDesireToSellStartupByInvestorType(GameEntity startup, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return OnGoalCompletion(startup, investorType);

                case InvestorType.StockExchange:
                    return GetStockExhangeTradeDesire(startup, shareholderId);

                case InvestorType.Founder:
                    return GetFounderExitDesire(startup, shareholderId, gameContext);

                case InvestorType.Strategic:
                    return GetStrategicInvestorExitDesire(startup, shareholderId, gameContext);

                default:
                    Debug.Log("GetDesireToSellStartupByInvestorType. Unknown investor type " + investorType.ToString());
                    return 0;
            }
        }

        public static long GetFounderExitDesire(GameEntity startup, int shareholderId, GameContext gameContext)
        {
            var founder = InvestmentUtils.GetInvestorById(gameContext, shareholderId);

            var ambitions = founder.humanSkills.Traits[TraitType.Ambitions];

            var ambition = HumanUtils.GetFounderAmbition(ambitions);

            if (ambition == Ambition.EarnMoney)
                return 1;

            return 0;
        }

        public static long GetStrategicInvestorExitDesire(GameEntity startup, int shareholderId, GameContext context)
        {
            var managingCompany = GetInvestorById(context, shareholderId);

            bool interestedIn = IsInSphereOfInterest(managingCompany, startup);

            return interestedIn ? 0 : 1;
        }

        public static long GetStockExhangeTradeDesire(GameEntity startup, int shareholderId)
        {
            return 1;
        }


        public static long OnGoalCompletion(GameEntity startup, InvestorType investorType)
        {
            bool goalCompleted = !InvestmentUtils.IsInvestorSuitableByGoal(investorType, startup.companyGoal.InvestorGoal);

            return goalCompleted ? 1 : 0;
        }
    }
}
