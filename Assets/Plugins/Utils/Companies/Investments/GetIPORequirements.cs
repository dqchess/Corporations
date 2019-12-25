﻿using UnityEngine;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static bool IsMeetsIPOCompanyCostRequirement(GameContext gameContext, int companyId)
        {
            return Economy.GetCompanyCost(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_COST;
        }

        public static bool IsMeetsIPOProfitRequirement(GameContext gameContext, int companyId)
        {
            return Economy.GetProfit(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_PROFIT;
        }

        public static bool IsMeetsIPOShareholderRequirement(GameContext gameContext, int companyId)
        {
            var c = GetCompany(gameContext, companyId);

            return c.shareholders.Shareholders.Count >= 3;
        }

        public static bool IsMeetsIPORequirements(GameContext gameContext, int companyId)
        {
            bool meetsCostRequirement = IsMeetsIPOCompanyCostRequirement(gameContext, companyId);
            bool meetsProfitRequirement = IsMeetsIPOProfitRequirement(gameContext, companyId);
            bool meetsShareholderRequirement = IsMeetsIPOShareholderRequirement(gameContext, companyId);

            return meetsCostRequirement && meetsProfitRequirement && meetsShareholderRequirement;
        }

        public static bool IsCanGoPublic(GameContext gameContext, int companyId)
        {
            bool isAlreadyPublic = GetCompany(gameContext, companyId).isPublicCompany;
            bool meetsIPORequirements = IsMeetsIPORequirements(gameContext, companyId);

            return !isAlreadyPublic && meetsIPORequirements;
        }
    }
}