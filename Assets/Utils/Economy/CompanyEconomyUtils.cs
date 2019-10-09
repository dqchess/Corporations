﻿namespace Assets.Utils
{
    public static partial class EconomyUtils
    {
        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(e, context);
        }

        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }

        internal static long GetProfit(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static long GetProfit(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetProfit(c, context);
        }


        internal static bool IsProfitable(GameContext gameContext, int companyId)
        {
            return GetProfit(gameContext, companyId) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetProfit(c, context);

            return change * 100 / maintenance;
        }


        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            var isMaxDevelopment = true;
            var isProfitable = IsProfitable(gameContext, product.company.Id);

            return isMaxDevelopment && isProfitable;
        }
    }
}
