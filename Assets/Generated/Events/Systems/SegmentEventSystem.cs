//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class SegmentEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ISegmentListener> _listenerBuffer;

    public SegmentEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ISegmentListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Segment)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasSegment && entity.hasSegmentListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.segment;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.segmentListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnSegment(e, component.Segments);
            }
        }
    }
}