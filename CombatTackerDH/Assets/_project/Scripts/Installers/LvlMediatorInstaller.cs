using UnityEngine;
using Zenject;

namespace CombarTracker
{
    public class LvlMediatorInstaller : MonoInstaller
    {
        [SerializeField] private PrefabHolder _prefabHolder;
        public override void InstallBindings()
        {
            Container.Bind<PrefabHolder>().FromInstance(_prefabHolder).AsSingle();
            Container.Bind<LvlFactory>().AsSingle();
            Container.Bind<PresenterFactory>().AsSingle();
            Container.Bind<LvlMediator>().AsSingle();
        }
    }
}

