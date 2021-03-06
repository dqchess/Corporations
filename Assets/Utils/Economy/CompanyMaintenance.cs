﻿using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        internal static long GetCompanyMaintenance(GameContext gameContext, int companyId) => GetCompanyMaintenance(gameContext, Companies.Get(gameContext, companyId));
        internal static long GetCompanyMaintenance(GameContext gameContext, GameEntity c)
        {
            if (Companies.IsProductCompany(c))
                return GetProductCompanyMaintenance(c, gameContext);
            else
                return GetGroupMaintenance(gameContext, c.company.Id);
        }

        private static long GetGroupMaintenance(GameContext gameContext, int companyId)
        {
            var holdings = Companies.GetCompanyHoldings(gameContext, companyId, true);

            return holdings
                .Sum(h => h.control * GetCompanyMaintenance(gameContext, h.companyId) / 100);
        }
    }
}
