using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class HelloWorldSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public HelloWorldSystem(Contexts contexts) : base(contexts.game)
    {

    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.helloWorld.log);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHelloWorld;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.HelloWorld);
    }
}
