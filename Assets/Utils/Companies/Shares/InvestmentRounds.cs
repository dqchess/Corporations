﻿namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void StartInvestmentRound(GameContext gameContext, int companyId)
        {
            StartInvestmentRound(GetCompanyById(gameContext, companyId), gameContext);
        }

        public static InvestmentRound GetInvestmentRoundName(GameEntity company)
        {
            switch (company.companyGoal.InvestorGoal)
            {
                case InvestorGoal.Prototype:
                    return InvestmentRound.Preseed;

                case InvestorGoal.FirstUsers:
                    return InvestmentRound.Seed;

                case InvestorGoal.BecomeMarketFit:
                    return InvestmentRound.A;

                case InvestorGoal.Release:
                    return InvestmentRound.B;

                case InvestorGoal.BecomeProfitable:
                    return InvestmentRound.C;

                case InvestorGoal.GrowCompanyCost:
                    return InvestmentRound.C;

                case InvestorGoal.IPO:
                    return InvestmentRound.D;

                default:
                    return InvestmentRound.E;
            }
        }

        public static void NotifyAboutInvestmentRound(GameEntity company, GameContext gameContext)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (IsInSphereOfInterest(playerCompany, company))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageInvestmentRoundStarted(company.company.Id));
        }

        public static void StartInvestmentRound(GameEntity company, GameContext gameContext)
        {
            if (company.hasAcceptsInvestments)
                return;

            //NotifyAboutInvestmentRound(company, gameContext);

            var round = GetInvestmentRoundName(company);

            company.ReplaceInvestmentRounds(round);
            company.AddAcceptsInvestments(Constants.INVESTMENT_ROUND_ACTIVE_FOR_DAYS);
        }
    }
}
