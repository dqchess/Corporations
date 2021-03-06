﻿namespace Assets.Core
{
    public static partial class Investments
    {
        public static InvestorGoal GetNextGoal(InvestorGoal current)
        {
            switch (current)
            {
                case InvestorGoal.Prototype: return InvestorGoal.FirstUsers;
                case InvestorGoal.FirstUsers: return InvestorGoal.BecomeMarketFit;
                case InvestorGoal.BecomeMarketFit: return InvestorGoal.Release;
                case InvestorGoal.Release: return InvestorGoal.BecomeProfitable;
                case InvestorGoal.BecomeProfitable: return InvestorGoal.Operationing;

                default: return current;
            }
        }

        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                case InvestorGoal.Prototype: measurableGoal = 1; break;
                case InvestorGoal.FirstUsers: measurableGoal = 50; break;

                case InvestorGoal.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoal.Release: measurableGoal = 1; break;

                case InvestorGoal.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoal.IPO: measurableGoal = 1; break;

                case InvestorGoal.GrowCompanyCost:
                    measurableGoal = Economy.GetCompanyCost(gameContext, company.company.Id) * (100 + Balance.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    break;
            }

            company.ReplaceCompanyGoal(investorGoal, measurableGoal);
        }

        private static bool IsCanTakeIPOGoal (GameEntity company, GameContext gameContext, InvestorGoal nextGoal)
        {
            return nextGoal == InvestorGoal.GrowCompanyCost && Economy.GetCompanyCost(gameContext, company.company.Id) > Balance.IPO_REQUIREMENTS_COMPANY_COST / 2;
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, bool forceComplete = false)
        {
            var nextGoal = GetNextGoal(company.companyGoal.InvestorGoal);

            if (IsCanTakeIPOGoal(company, gameContext, nextGoal))
                nextGoal = InvestorGoal.IPO;

            if (forceComplete || IsGoalCompleted(company, gameContext))
                SetCompanyGoal(gameContext, company, nextGoal);
        }
    }
}
