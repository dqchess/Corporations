﻿using Assets.Core;
using UnityEngine;

public class DecreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newConditions = offer.acquisitionOffer.BuyerOffer;

        newConditions.Price = (long)(newConditions.Price / 1.1f);

        Companies.TweakAcquisitionConditions(Q, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);
    }
}
