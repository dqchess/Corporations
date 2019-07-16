//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AcquisitionOfferComponent acquisitionOffer { get { return (AcquisitionOfferComponent)GetComponent(GameComponentsLookup.AcquisitionOffer); } }
    public bool hasAcquisitionOffer { get { return HasComponent(GameComponentsLookup.AcquisitionOffer); } }

    public void AddAcquisitionOffer(long newOffer, int newCompanyId, int newBuyerId) {
        var index = GameComponentsLookup.AcquisitionOffer;
        var component = (AcquisitionOfferComponent)CreateComponent(index, typeof(AcquisitionOfferComponent));
        component.Offer = newOffer;
        component.CompanyId = newCompanyId;
        component.BuyerId = newBuyerId;
        AddComponent(index, component);
    }

    public void ReplaceAcquisitionOffer(long newOffer, int newCompanyId, int newBuyerId) {
        var index = GameComponentsLookup.AcquisitionOffer;
        var component = (AcquisitionOfferComponent)CreateComponent(index, typeof(AcquisitionOfferComponent));
        component.Offer = newOffer;
        component.CompanyId = newCompanyId;
        component.BuyerId = newBuyerId;
        ReplaceComponent(index, component);
    }

    public void RemoveAcquisitionOffer() {
        RemoveComponent(GameComponentsLookup.AcquisitionOffer);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAcquisitionOffer;

    public static Entitas.IMatcher<GameEntity> AcquisitionOffer {
        get {
            if (_matcherAcquisitionOffer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AcquisitionOffer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAcquisitionOffer = matcher;
            }

            return _matcherAcquisitionOffer;
        }
    }
}