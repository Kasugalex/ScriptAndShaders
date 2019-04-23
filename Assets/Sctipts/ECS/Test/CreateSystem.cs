using Entitas;

public class CreateSystem : IInitializeSystem
{
    private Contexts contexts;

    public CreateSystem(Contexts contexts)
    {
        this.contexts = contexts;
    }

    public void Initialize()
    {
        var e = contexts.game.CreateEntity();
        e.AddHelloWorld("hello world");
    }
}
