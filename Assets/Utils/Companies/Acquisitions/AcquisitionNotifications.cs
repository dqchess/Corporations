﻿using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static void NotifyAboutInterest(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var company = Get(gameContext, companyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageInterestToCompany(companyId, buyerInvestorId));
        }

        public static void NotifyAboutAcquisition(GameContext gameContext, int buyerShareholderId, int targetCompanyId, long bid)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, buyerShareholderId, bid));

            var company = Get(gameContext, targetCompanyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(targetCompanyId, buyerShareholderId, bid));


            Debug.LogFormat("ACQUISITION: {0} bought {1} for {2}!",
                GetInvestorName(gameContext, buyerShareholderId),
                Get(gameContext, targetCompanyId).company.Name,
                Format.Money(bid));
        }

        public static void NotifyAboutCorporateAcquisition(GameContext gameContext, int buyerShareholderId, int targetCompanyId)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, buyerShareholderId, 0));

            var company = Get(gameContext, targetCompanyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(targetCompanyId, buyerShareholderId, 0));


            Debug.LogFormat("CORPORATE ACQUISITION: {0} integrated {1}!",
                GetInvestorName(gameContext, buyerShareholderId),
                Get(gameContext, targetCompanyId).company.Name);
        }
    }
}
