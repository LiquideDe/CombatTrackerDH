using UnityEngine;

namespace CombarTracker
{
    public class LvlMediator
    {
        private LvlFactory _lvlFactory;
        private PresenterFactory _presenterFactory;
        private MainScenePresenter _mainScene;
        private AudioManager _audioManager;
        private Creators _creators;
        private GameObject _loadingScreen;

        public LvlMediator(LvlFactory lvlFactory, AudioManager audioManager, Creators creators)
        {
            _lvlFactory = lvlFactory;
            _audioManager = audioManager;
            _creators = creators;
        }

        public void ShowLoading()
        {
            _loadingScreen = _lvlFactory.Get(TypeScene.Loading);
            _creators.AllDone += CreateMainScene;
            _creators.LoadAll();
        }

        private void CreateMainScene()
        {
            
            MainSceneView view = _lvlFactory.Get(TypeScene.MainScene).GetComponent<MainSceneView>();
            WeaponView weaponView = view.GetComponentInChildren<WeaponView>();
            _mainScene = new MainScenePresenter(_audioManager, view, _creators, _lvlFactory, weaponView);
            _mainScene.ShowCreationPanel += ShowCreationPanel;
            weaponView.DestroyLoading(_loadingScreen);
        }

        private void ShowCreationPanel()
        {
            CreationCharacterPresenter presenter = (CreationCharacterPresenter)_presenterFactory.Get(TypeScene.CreatorCharacter);
            CreationCharacterView view = _lvlFactory.Get(TypeScene.CreatorCharacter).GetComponent<CreationCharacterView>();
            presenter.Close += ShowMainScene;
            presenter.Initialize(view);
        }

        private void ShowMainScene()
        {
            _mainScene.ShowView();
        }
    }
}

