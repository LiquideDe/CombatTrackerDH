using Zenject;
using System;

namespace CombarTracker
{
    public class PresenterFactory
    {
        private DiContainer _diContainer;

        public PresenterFactory(DiContainer diContainer) => _diContainer = diContainer;

        public IPresenter Get(TypeScene type)
        {
            switch (type)
            {
                case TypeScene.MainScene:
                    return _diContainer.Instantiate<MainScenePresenter>();

                case TypeScene.CreatorCharacter:
                    return _diContainer.Instantiate<CreationCharacterPresenter>();

                case TypeScene.DamagePanel:
                    return _diContainer.Instantiate<DamagePanelPresenter>();

                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }
}

