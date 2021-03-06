﻿using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity[] GetDaughterProductCompanies(GameContext context, int companyId) => GetDaughterProductCompanies(context, Get(context, companyId));
        public static GameEntity[] GetDaughterProductCompanies(GameContext context, GameEntity company)
        {
            return GetDaughterCompanies(context, company)
                .Where(p => p.hasProduct)
                .ToArray();
        }

        public static bool IsHasReleasedProducts(GameContext gameContext, GameEntity company)
        {
            return GetDaughterProductCompanies(gameContext, company)
                .Count(p => p.isRelease) > 0;
        }

        public static GameEntity[] GetDaughterCompaniesOnMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            return GetDaughterProductCompanies(gameContext, group)
                .Where(p => p.product.Niche == nicheType)
                .ToArray();
        }

        public static bool IsHasDaughters(GameContext gameContext, GameEntity company)
        {
            return GetDaughterCompaniesAmount(company, gameContext) > 0;
        }

        public static int GetDaughterCompaniesAmount(GameEntity company, GameContext gameContext)
        {
            return GetDaughterCompanies(gameContext, company).Count();
        }

        public static GameEntity[] GetDaughterCompanies(GameContext context, int companyId) => GetDaughterCompanies(context, Get(context, companyId));
        public static GameEntity[] GetDaughterCompanies(GameContext context, GameEntity c)
        {
            if (!c.hasShareholder)
                return new GameEntity[0];

            int investorId = c.shareholder.Id;

            return GetInvestments(context, investorId);
        }

        public static bool IsDaughterOfCompany(GameEntity parent, GameEntity daughter)
        {
            return IsInvestsInCompany(daughter, parent.shareholder.Id);
        }

        public static GameEntity GetParentCompany(GameContext context, GameEntity company)
        {
            if (company.isIndependentCompany)
                return null;


            var shares = 0;
            var shareholders = company.shareholders.Shareholders;
            GameEntity parent = null;

            foreach (var shareholder in shareholders)
            {
                var id = shareholder.Key;
                var block = shareholder.Value.amount;

                var investor = Investments.GetInvestorById(context, id);
                if (investor.hasCompany)
                {
                    // is managing company
                    if (block > shares)
                    {
                        parent = investor;
                    }
                }
            }

            return parent;
        }

        private static GameEntity[] GetInvestments(GameContext context, int investorId)
        {
            return Array.FindAll(
                context.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders)),
                c => IsInvestsInCompany(c, investorId)
                );
        }

        public static List<CompanyHolding> GetCompanyHoldings(GameContext context, int companyId, bool recursively)
        {
            List<CompanyHolding> companyHoldings = new List<CompanyHolding>();

            var c = Get(context, companyId);

            var investments = GetDaughterCompanies(context, companyId);

            foreach (var investment in investments)
            {
                var holding = new CompanyHolding
                {
                    companyId = investment.company.Id,
                    control = GetShareSize(context, investment.company.Id, c.shareholder.Id),
                    holdings = recursively ? GetCompanyHoldings(context, investment.company.Id, recursively) : new List<CompanyHolding>()
                };

                companyHoldings.Add(holding);
            }

            return companyHoldings;
        }

        public static List<CompanyHolding> GetPersonalHoldings(GameContext context, int shareholderId, bool recursively)
        {
            List<CompanyHolding> companyHoldings = new List<CompanyHolding>();

            var investments = Investments.GetInvestmentsOf(context, shareholderId);

            foreach (var investment in investments)
            {
                var holding = new CompanyHolding
                {
                    companyId = investment.company.Id,
                    control = GetShareSize(context, investment.company.Id, shareholderId),
                    holdings = recursively ? GetCompanyHoldings(context, investment.company.Id, recursively) : new List<CompanyHolding>()
                };

                companyHoldings.Add(holding);
            }

            return companyHoldings;
        }

        // changes
        public static bool IsCompanyGrowing(GameEntity product, GameContext gameContext)
        {
            return GetProductCompanyResults(product, gameContext).MarketShareChange > 0;
        }

        public static ProductCompanyResult GetProductCompanyResults(GameEntity product, GameContext gameContext)
        {
            var competitors = Markets.GetProductsOnMarket(gameContext, product);

            long previousMarketSize = 0;
            long currentMarketSize = 0;

            long prevCompanyClients = 0;
            long currCompanyClients = 0;

            foreach (var c in competitors)
            {
                var last = c.metricsHistory.Metrics.Count - 1;
                var prev = c.metricsHistory.Metrics.Count - 2;

                // company was formed this month
                if (prev < 0)
                    continue;

                var audience = c.metricsHistory.Metrics[prev].AudienceSize;
                var clients = c.metricsHistory.Metrics[last].AudienceSize;

                previousMarketSize += audience;
                currentMarketSize += clients;

                if (c.company.Id == product.company.Id)
                {
                    prevCompanyClients = audience;
                    currCompanyClients = clients;
                }
            }

            var prevShare = prevCompanyClients * 100d / (previousMarketSize + 1);
            var Share = currCompanyClients * 100d / (currentMarketSize + 1);

            return new ProductCompanyResult
            {
                clientChange = currCompanyClients - prevCompanyClients,
                MarketShareChange = (float)(Share - prevShare),
                ConceptStatus = Products.GetConceptStatus(product, gameContext),
                CompanyId = product.company.Id
            };
        }


    }

    public class CompanyHolding
    {
        // shares percent
        public int control;

        // controlled company id
        public int companyId;

        public List<CompanyHolding> holdings;
    }
}
