//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TaskComponent task { get { return (TaskComponent)GetComponent(GameComponentsLookup.Task); } }
    public bool hasTask { get { return HasComponent(GameComponentsLookup.Task); } }

    public void AddTask(bool newIsCompleted, CompanyTask newCompanyTask, int newStartTime, int newDuration, int newEndTime) {
        var index = GameComponentsLookup.Task;
        var component = (TaskComponent)CreateComponent(index, typeof(TaskComponent));
        component.isCompleted = newIsCompleted;
        component.CompanyTask = newCompanyTask;
        component.StartTime = newStartTime;
        component.Duration = newDuration;
        component.EndTime = newEndTime;
        AddComponent(index, component);
    }

    public void ReplaceTask(bool newIsCompleted, CompanyTask newCompanyTask, int newStartTime, int newDuration, int newEndTime) {
        var index = GameComponentsLookup.Task;
        var component = (TaskComponent)CreateComponent(index, typeof(TaskComponent));
        component.isCompleted = newIsCompleted;
        component.CompanyTask = newCompanyTask;
        component.StartTime = newStartTime;
        component.Duration = newDuration;
        component.EndTime = newEndTime;
        ReplaceComponent(index, component);
    }

    public void RemoveTask() {
        RemoveComponent(GameComponentsLookup.Task);
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

    static Entitas.IMatcher<GameEntity> _matcherTask;

    public static Entitas.IMatcher<GameEntity> Task {
        get {
            if (_matcherTask == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Task);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTask = matcher;
            }

            return _matcherTask;
        }
    }
}
