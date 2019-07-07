﻿using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static bool IsMeetsIPOCompanyCostRequirement(GameContext gameContext, int companyId)
        {
            return CompanyEconomyUtils.GetCompanyCost(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_COST;
        }

        public static bool IsMeetsIPOProfitRequirement(GameContext gameContext, int companyId)
        {
            return CompanyEconomyUtils.GetBalanceChange(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_PROFIT;
        }

        public static bool IsMeetsIPOShareholderRequirement(GameContext gameContext, int companyId)
        {
            var c = GetCompanyById(gameContext, companyId);

            return c.shareholders.Shareholders.Count >= 3;
        }

        public static bool IsMeetsIPORequirements(GameContext gameContext, int companyId)
        {
            bool meetsCostRequirement = IsMeetsIPOCompanyCostRequirement(gameContext, companyId);
            bool meetsProfitRequirement = IsMeetsIPOProfitRequirement(gameContext, companyId);
            bool meetsShareholderRequirement = IsMeetsIPOShareholderRequirement(gameContext, companyId);

            return meetsCostRequirement && meetsProfitRequirement && meetsShareholderRequirement;
        }
    }
}