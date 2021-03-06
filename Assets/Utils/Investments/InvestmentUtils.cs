﻿using Entitas;
using System;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        public static GameEntity[] GetInvestmentsOf(GameContext gameContext, int shareholderId)
        {
            var shareholder = GetInvestorById(gameContext, shareholderId);

            return Array.FindAll(gameContext.GetEntities(GameMatcher.Shareholders), e => IsInvestsInCompany(shareholder, e));
        }

        public static GameEntity GenerateAngel(GameContext gameContext)
        {
            var human = Humans.GenerateHuman(gameContext);

            var investorId = GenerateInvestorId(gameContext);

            BecomeInvestor(gameContext, human, 1000000);

            TurnToAngel(gameContext, investorId);

            return human;
        }

        public static void TurnToAngel(GameContext gameContext, int investorId)
        {
            var investor = GetInvestorById(gameContext, investorId);

            investor.ReplaceShareholder(investor.shareholder.Id, investor.shareholder.Name, InvestorType.Angel);
        }

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            var investor = GetInvestorById(context, investorId);

            Companies.AddResources(investor, sum);
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == investorId);
        }

        public static GameEntity GetCompanyByInvestorId(GameContext context, int investorId)
        {
            return GetInvestorById(context, investorId);
        }
        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return GetCompanyByInvestorId(context, investorId).company.Id;
        }

        public static string GetInvestorGoalDescription(BlockOfShares shares)
        {
            return shares.InvestorType.ToString();
            //return GetFormattedInvestorGoal(shares.InvestorType);
        }

        public static long GetInvestorCapitalCost(GameContext gameContext, GameEntity human)
        {
            var holdings = Companies.GetPersonalHoldings(gameContext, human.shareholder.Id, false);

            return Economy.GetHoldingCost(gameContext, holdings);
        }
    }
}
