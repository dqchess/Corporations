﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    partial class Companies
    {
        public static int GetTotalShares(GameContext context, int companyId) => GetTotalShares(Get(context, companyId).shareholders.Shareholders);
        public static int GetTotalShares(Dictionary<int, BlockOfShares> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value.amount;

            return totalShares;
        }

        public static bool IsSharesCanBeSold(GameEntity company)
        {
            return company.isPublicCompany || company.hasAcceptsInvestments;
        }

        public static bool IsAreSharesSellable(GameContext context, int companyId)
        {
            return IsSharesCanBeSold(Get(context, companyId));
        }

        public static Dictionary<int, BlockOfShares> GetShareholders(GameEntity company)
        {
            var shareholders = new Dictionary<int, BlockOfShares>();

            if (company.hasShareholders)
                shareholders = company.shareholders.Shareholders;

            return shareholders;
        }

        public static int GetAmountOfShares(GameContext context, int companyId, int investorId)
        {
            var c = Get(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            return IsInvestsInCompany(c, investorId) ? shareholders[investorId].amount : 0;
        }

        public static bool IsInvestsInCompany(GameContext gameContext, int companyId, int investorId)
        {
            return IsInvestsInCompany(Get(gameContext, companyId), investorId);
        }

        public static bool IsInvestsInCompany(GameEntity company, int investorId)
        {
            return Investments.IsInvestsInCompany(investorId, company);
            //return company.shareholders.Shareholders.ContainsKey(investorId);
        }

        public static int GetShareSize(GameContext context, int companyId, int investorId)
        {
            var c = Get(context, companyId);

            int shares = GetAmountOfShares(context, companyId, investorId);
            int total = GetTotalShares(c.shareholders.Shareholders);

            if (total == 0)
                return 0;

            return shares * 100 / total;
        }

        public static long GetSharesCost(GameContext context, int companyId, int investorId)
        {
            var c = Get(context, companyId);

            int shares = GetAmountOfShares(context, companyId, investorId);
            int total = GetTotalShares(c.shareholders.Shareholders);

            return Economy.GetCompanyCost(context, c.company.Id) * shares / total;
        }

        public static string GetInvestorName(GameEntity investor)
        {
            return investor.shareholder.Name;
        }

        public static string GetInvestorName(GameContext context, int investorId)
        {
            return GetInvestorName(GetInvestorById(context, investorId));
        }

        public static int GetBiggestShareholder(GameContext gameContext, int companyId)
        {
            var c = Get(gameContext, companyId);

            var list = c.shareholders.Shareholders.OrderBy(key => key.Value);

            return list.First().Key;
        }

        public static string GetBiggestShareholderName(GameContext gameContext, int companyId)
        {
            return GetInvestorName(gameContext, GetBiggestShareholder(gameContext, companyId));
        }

        public static string GetShareholderStatus(int sharesPercent)
        {
            if (sharesPercent < 1)
                return "Non voting";

            if (sharesPercent < 10)
                return "Voting";

            if (sharesPercent < 25)
                return "Majority";

            if (sharesPercent < 50)
                return "Blocking";

            if (sharesPercent < 100)
                return "Controling";

            return "Owner";
        }
    }
}
