using UnityEngine;
using Zenject;

namespace CombarTracker
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] AudioManager _audioManagerPrefab;
        [SerializeField] Camera _camera;

        public override void InstallBindings()
        {
            AudioManager audioManager = Container.InstantiatePrefabForComponent<AudioManager>(_audioManagerPrefab);
            Container.BindInterfacesAndSelfTo<AudioManager>().FromInstance(audioManager).AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        }
    }
}

