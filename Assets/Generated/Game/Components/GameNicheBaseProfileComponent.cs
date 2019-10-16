//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public NicheBaseProfileComponent nicheBaseProfile { get { return (NicheBaseProfileComponent)GetComponent(GameComponentsLookup.NicheBaseProfile); } }
    public bool hasNicheBaseProfile { get { return HasComponent(GameComponentsLookup.NicheBaseProfile); } }

    public void AddNicheBaseProfile(MarketProfile newProfile) {
        var index = GameComponentsLookup.NicheBaseProfile;
        var component = (NicheBaseProfileComponent)CreateComponent(index, typeof(NicheBaseProfileComponent));
        component.Profile = newProfile;
        AddComponent(index, component);
    }

    public void ReplaceNicheBaseProfile(MarketProfile newProfile) {
        var index = GameComponentsLookup.NicheBaseProfile;
        var component = (NicheBaseProfileComponent)CreateComponent(index, typeof(NicheBaseProfileComponent));
        component.Profile = newProfile;
        ReplaceComponent(index, component);
    }

    public void RemoveNicheBaseProfile() {
        RemoveComponent(GameComponentsLookup.NicheBaseProfile);
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

    static Entitas.IMatcher<GameEntity> _matcherNicheBaseProfile;

    public static Entitas.IMatcher<GameEntity> NicheBaseProfile {
        get {
            if (_matcherNicheBaseProfile == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NicheBaseProfile);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNicheBaseProfile = matcher;
            }

            return _matcherNicheBaseProfile;
        }
    }
}