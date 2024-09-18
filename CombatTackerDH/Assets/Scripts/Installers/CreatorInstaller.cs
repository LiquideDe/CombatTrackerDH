using Zenject;

public class CreatorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInstance();
    }

    private void BindInstance()
    {
        Container.Bind<Creators>().AsSingle();
    }
}
