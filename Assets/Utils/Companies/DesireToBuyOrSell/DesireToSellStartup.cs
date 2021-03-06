﻿using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
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
                    return Balance.COMPANY_DESIRE_TO_SELL_NO;
            }
        }

        public static Ambition GetFounderAmbition(GameEntity company, GameContext gameContext)
        {
            var CEOId = company.cEO.HumanId;

            return Humans.GetFounderAmbition(gameContext, CEOId);
        }

        public static long GetFounderExitDesire(GameEntity startup, int shareholderId, GameContext gameContext)
        {
            var founder = Investments.GetInvestorById(gameContext, shareholderId);

            var ambitions = founder.humanSkills.Traits[TraitType.Ambitions];

            var ambition = Humans.GetFounderAmbition(ambitions);

            if (ambition == Ambition.EarnMoney)
                return Balance.COMPANY_DESIRE_TO_SELL_YES;

            return Balance.COMPANY_DESIRE_TO_SELL_NO;
        }

        public static long GetStrategicInvestorExitDesire(GameEntity startup, int shareholderId, GameContext context)
        {
            var managingCompany = GetInvestorById(context, shareholderId);

            bool interestedIn = IsInSphereOfInterest(managingCompany, startup);

            return interestedIn ? Balance.COMPANY_DESIRE_TO_SELL_NO : Balance.COMPANY_DESIRE_TO_SELL_YES;
        }

        public static long GetStockExhangeTradeDesire(GameEntity startup, int shareholderId)
        {
            return Balance.COMPANY_DESIRE_TO_SELL_YES;
        }


        public static long OnGoalCompletion(GameEntity startup, InvestorType investorType)
        {
            bool goalCompleted = !Investments.IsInvestorSuitableByGoal(investorType, startup.companyGoal.InvestorGoal);

            return goalCompleted ? Balance.COMPANY_DESIRE_TO_SELL_YES : Balance.COMPANY_DESIRE_TO_SELL_NO;
        }
    }
}
