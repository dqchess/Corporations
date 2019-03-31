//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly NavigationHistoryComponent navigationHistoryComponent = new NavigationHistoryComponent();

    public bool isNavigationHistory {
        get { return HasComponent(GameComponentsLookup.NavigationHistory); }
        set {
            if (value != isNavigationHistory) {
                var index = GameComponentsLookup.NavigationHistory;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : navigationHistoryComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherNavigationHistory;

    public static Entitas.IMatcher<GameEntity> NavigationHistory {
        get {
            if (_matcherNavigationHistory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NavigationHistory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNavigationHistory = matcher;
            }

            return _matcherNavigationHistory;
        }
    }
}
