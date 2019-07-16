﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newOffer = offer.acquisitionOffer.Offer * 1.1f;
        if (newOffer > Balance)
            newOffer = Balance;

        CompanyUtils.UpdateAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, (long)newOffer);
    }
}