﻿using System;

namespace Assets.Utils.Formatting
{
    public static class EnumUtils
    {
        public static T Next<T>(this T src) where T : struct
        {
            // https://stackoverflow.com/questions/642542/how-to-get-next-or-previous-enum-value-in-c-sharp
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static string GetFormattedUserType(UserType userType)
        {
            switch (userType)
            {
                case UserType.Core: return "Core users";
                case UserType.Regular: return "Regular users";
                case UserType.Mass: return "Mass users";

                default: return userType.ToString();
            }
        }

        public static string GetFormattedNicheName(NicheType niche)
        {
            switch (niche)
            {
                case NicheType.Messenger: return "Messengers";
                case NicheType.SocialNetwork: return "Social Networks";
                case NicheType.Blogs: return "Blogs";
                case NicheType.Forums: return "Forums";
                case NicheType.Dating: return "Dating apps";


                case NicheType.CloudComputing: return "Cloud Computing";
                case NicheType.OSDesktop: return "Desktop OS";
                case NicheType.SearchEngine: return "Search Engines";

                default: return "Unknown niche: " + niche.ToString();
            }
        }

        public static string GetFormattedIndustryName(IndustryType industry)
        {
            switch (industry)
            {
                //case IndustryType.OS: return "Operation Systems";
                case IndustryType.Fundamental: return "Fundamentals";
                case IndustryType.Communications: return "Communications";

                default: return "Unknown Industry: " + industry.ToString();
            }
        }

        public static string GetFormattedCompanyType(CompanyType companyType)
        {
            switch (companyType)
            {
                case CompanyType.ProductCompany: return "Product Company";
                case CompanyType.Holding: return "Holding Company";
                case CompanyType.Corporation: return "Corporation";
                case CompanyType.FinancialGroup: return "Investment Company";
                case CompanyType.Group: return "Group of companies";

                default: return "WUT " + companyType.ToString();
            }
        }
    }
}
