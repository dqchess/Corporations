﻿namespace Assets.Utils
{
    partial class Economy
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = Markets.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameContext context, int companyId) => GetProductCompanyBaseCost(context, Companies.GetCompany(context, companyId));
        public static long GetProductCompanyBaseCost(GameContext context, GameEntity company)
        {
            long audienceCost = GetClientBaseCost(context, company.company.Id);
            long profitCost = GetCompanyIncomeBasedCost(context, company.company.Id);

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            return 0;
            var c = Companies.GetCompany(context, companyId);

            return MarketingUtils.GetClients(c) * 100;
        }
    }
}