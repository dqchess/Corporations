﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSendAcquisitionOfferButtonIfSentAlready : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var offer = Companies.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        return offer.acquisitionOffer.Turn == AcquisitionTurn.Seller || Companies.IsCompanyRelatedToPlayer(GameContext, SelectedCompany);
    }
}
