﻿using Assets.Classes;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void SpendResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Spend(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}
