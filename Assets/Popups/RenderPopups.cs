﻿using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using System.Collections.Generic;

public partial class PopupView : View
{
    void RenderCloseCompanyPopup()
    {
        RenderUniversalPopup(
            "Do you want to close this company?",
            "",
            typeof(ClosePopup),
            typeof(CloseCompanyPopupButton)
            );
    }


    void RenderInterestToCompany(PopupMessageInterestToCompany popup)
    {
        var target = Companies.GetCompany(GameContext, popup.companyId);
        var buyer = Investments.GetInvestorById(GameContext, popup.buyerInvestorId);

        RenderUniversalPopup(
            $"{buyer.company.Name} wants to buy {target.company.Name}!",
            "If we want to prevent this, we need to send a counter offer!"
            );
    }

    void RenderTargetAcquisition(PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence popup)
    {
        var target = Companies.GetCompany(GameContext, popup.companyId);
        var buyer = Investments.GetInvestorById(GameContext, popup.InterceptorCompanyId);

        RenderUniversalPopup(
            "ACQUISITION!",
            $"Company {buyer.company.Name} BOUGHT {target.company.Name} for {Format.Money(popup.Bid)}!\n\n" +
            $"This move will increase market share of {buyer.company.Name}"
            );
    }

    void RenderMarketChangePopup(PopupMessageMarketPhaseChange popup)
    {
        var name = EnumUtils.GetFormattedNicheName(popup.NicheType);
        var state = Markets.GetMarketState(GameContext, popup.NicheType);
        var possibilities = "";

        switch (state)
        {
            case MarketState.Innovation:
                possibilities = "It's time to be first! Your innovation chances in this niche increase by 25%";
                break;

            case MarketState.Trending:
                possibilities = "It seems, that this market has the potential! Companies on this market will get way more clients, but maintenance cost will also increase";
                break;

            case MarketState.MassGrowth:
                possibilities = "It's time to earn money now! Company maintenances increase even more";
                break;

            case MarketState.Decay:
                possibilities = "Market passed it's prime and will decay slowly. Companies will no longer receive new users";
                break;
        }

        RenderUniversalPopup(
            "Market state changed!",
            $"Market of {name} is {state} now!\n\n{possibilities}",
            typeof(ClosePopup)
            );
    }

    void RenderBankruptCompany(PopupMessageCompanyBankrupt popup)
    {
        RenderUniversalPopup(
            "Our competitor is bankrupt!",
            "Company " + GetCompanyName(popup.companyId) + " is bankrupt now!" +
            "\nSome of their clients will start using our product instead"
            );
    }

    void RenderNewCompany(PopupMessageCompanySpawn popup)
    {
        RenderUniversalPopup(
            "New Startup",
            "Company " + GetCompanyName(popup.companyId) + " started it's business. Their approach seems REVOLUTIONARY. They will compete with our products now" +
            "\n\nKeep an eye on them. Perhaps, we can buy them later"
            );
    }

    void RenderPreReleasePopup(PopupMessageDoYouWantToRelease popup)
    {
        RenderUniversalPopup(
            "Do you really want to release this product?",
            "Maintenance cost will increase\nThis product will get 20 brand power",
            typeof(ReleaseAppPopupButton),
            typeof(ClosePopup)
            );
    }

    void RenderCreateCompanyPopup(PopupMessageCreateApp popup)
    {
        RenderUniversalPopup(
            $"You have created a new company called {GetCompanyName(popup.companyId)}!",
            "You will need some time to match market requirements and after that, you will be able to release your product and make money from it",
            typeof(ClosePopupOK)
            );
    }

    void RenderHasTooManyPartners(PopupMessageTooManyPartners popup)
    {
        bool we = popup.companyId == MyCompany.company.Id;
        RenderUniversalPopup(
            (we ? "We" : "They") + " have too many partnerships already!",
            (we ? "We" : "They") + " need to break one partnership to become partners",
            typeof(ClosePopupOK)
            );
    }

    void RenderInspirationPopup(PopupMessageMarketInspiration popup)
    {
        RenderUniversalPopup(
            "INSPIRATION",
            "On your spare time you got new revolutionary ideas! You can create first " + EnumUtils.GetFormattedNicheName(popup.NicheType).ToUpper(),
            typeof(InspirationPopupButton)
            );
    }

    void RenderDoYouReallyWantToCreateAPrototype(PopupMessageDoYouWantToCreateApp popup)
    {
        // check if has enough resources
        var startCapital = Markets.GetStartCapital(popup.NicheType, GameContext);

        bool hasResources = Companies.IsEnoughResources(MyCompany, startCapital);

        var title = "Do you really want to create a new " + EnumUtils.GetFormattedNicheName(popup.NicheType);
        var description = $"We need at least {Format.Money(startCapital)} to create a product, which meets market requirements";

        var resourceText = hasResources ? "" : Visuals.Negative("\nYou don't have enough money for that :(");


        RenderUniversalPopup(
            title,
            description + resourceText,
            typeof(CreateAppPopupButton),
            typeof(ClosePopup)
            );
    }

    void RenderBankruptcyThreat(PopupMessageBankruptcyThreat popup)
    {
        RenderUniversalPopup(
            "BANKRUPTCY IS COMING!",
            "YOU HAVE ONE DAY TO SAVE YOUR COMPANY!\n\n" + Visuals.Negative("Raise investments and stop capturing markets!"),
            typeof(ClosePopupOK)
            );
    }

    void RenderGameOverMessage(PopupMessageGameOver popup)
    {
        RenderUniversalPopup(
            "YOU ARE BANKRUPT!",
            Visuals.Negative("<b><i>GAME OVER</i></b>") + "\nYou will be luckier next time. The game will be closed. Restart it!",
            typeof(ClosePopupExitGame)
            );
    }

    string GetCompanyName(int companyId) => Companies.GetCompanyName(GameContext, companyId);
}