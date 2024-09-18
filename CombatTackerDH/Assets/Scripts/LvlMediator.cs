using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LvlMediator
{
    private LvlFactory _lvlFactory;
    private PresenterFactory _presenterFactory;
    private MainScenePresenter _mainScene;

    public LvlMediator(LvlFactory lvlFactory, PresenterFactory presenterFactory)
    {
        _lvlFactory = lvlFactory;
        _presenterFactory = presenterFactory;
    }

    public void Initialize()
    {
        _mainScene = (MainScenePresenter)_presenterFactory.Get(TypeScene.MainScene);
        MainSceneView view = _lvlFactory.Get(TypeScene.MainScene).GetComponent<MainSceneView>();
        WeaponView weaponView = view.GetComponentInChildren<WeaponView>();
        _mainScene.ShowCreationPanel += ShowCreationPanel;
        _mainScene.Initialize(view, weaponView);
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
