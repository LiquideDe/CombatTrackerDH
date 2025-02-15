using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace CombarTracker
{
    public class MainScenePresenter : IPresenter
    {
        public event Action ShowCreationPanel;

        private Character _character;
        private AudioManager _audioManager;
        private MainSceneView _view;
        private WeaponView _weaponView;

        private List<Character> _characters = new List<Character>();
        private Creators _creators;
        private LvlFactory _lvlFactory;
        private PresenterFactory _presenterFactory;
        private CanDestroyView _tempView;

        public MainScenePresenter(AudioManager audioManager, MainSceneView view, Creators creators, LvlFactory lvlFactory, WeaponView weaponView)
        {
            _audioManager = audioManager;
            _view = view;
            _creators = creators;
            _lvlFactory = lvlFactory;
            _weaponView = weaponView;
            Subscribe();
        }

        public void ShowView() => _view.gameObject.SetActive(true);

        private void Subscribe()
        {
            _view.AddCharacter += ShowCharacters;
            _view.ClearScene += ClearScene;
            _view.CoverBody += CoverBody;
            _view.CoverHead += CoverHead;
            _view.CoverLeftHand += CoverLeftHand;
            _view.CoverLeftLeg += CoverLeftLeg;
            _view.CoverRightHand += CoverRightHand;
            _view.CoverRightLeg += CoverRightLeg;
            _view.EndTurn += EndTurn;
            _view.EndBattle += EndBattle;
            _view.LoadScene += ShowScenes;
            _view.NextTurn += NextTurn;
            _view.SaveScene += SaveScene;
            _view.ShowHandbook += ShowHandbook;
            _view.ShowThisCharacter += ShowThisCharacter;
            _view.ShowThisFeature += ShowThisFeature;
            _view.StartBattle += StartBattle;
            _view.TakeDamage += TakeDamage;
            _view.ChangeCharacterParameters += ParseInputs;
            _view.RemoveThisCharacter += RemoveThisCharacter;
            _view.ToggleHorde += ToggleHorde;
            _view.ShowNPC += ShowNPCNature;

            _weaponView.ShowThisProperty += ShowThisFeature;
            _weaponView.AutoFire += GunIsFiredAuto;
            _weaponView.Reload += ReloadGun;
            _weaponView.SemiAutoFire += GunIsFiredSemiAuto;
            _weaponView.SingleFire += GunIsFiredSingle;

        }

        private void GunIsFiredSingle(string nameGun) => GunFired(SearchGunInCharacterEquipments(nameGun), 1);

        private void GunIsFiredSemiAuto(string nameGun)
        {
            Weapon gun = SearchGunInCharacterEquipments(nameGun);
            GunFired(gun, gun.SemiAuto);
        }
        private void GunIsFiredAuto(string nameGun)
        {
            Weapon gun = SearchGunInCharacterEquipments(nameGun);
            GunFired(gun, gun.Auto);
        }

        private Weapon SearchGunInCharacterEquipments(string nameGun)
        {
            foreach (Equipment equipment in _character.Equipments)
                if (string.Compare(nameGun, equipment.Name, true) == 0)
                {
                    Weapon gun = (Weapon)equipment;
                    return gun;
                }

            throw new Exception($"Не нашли пушку под названием {nameGun}");
        }

        private void GunFired(Weapon gun, int amount)
        {
            if (gun.Clip > 0)
            {
                if (gun.Clip >= amount)
                    gun.Clip -= amount;
                else
                    gun.Clip = 0;
            }
            _weaponView.Initialize(GetWeaponsFromCharacter(_character));
        }

        private void ReloadGun(string nameGun)
        {
            Weapon gun = SearchGunInCharacterEquipments(nameGun);
            if (gun.TotalAmmo > 0)
            {
                gun.Clip = gun.MaxClip;
                gun.TotalAmmo -= gun.MaxClip;
            }
            _weaponView.Initialize(GetWeaponsFromCharacter(_character));
        }

        private void ShowCharacters()
        {
            if (_tempView != null)
                _tempView.DestroyView();

            _audioManager.PlayClick();
            ListWithNewItemsAndNewButton listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewButton).GetComponent<ListWithNewItemsAndNewButton>();
            listWithNewItems.AddNewItem += GoToCreationCharacter;
            listWithNewItems.ChooseThis += AddThisCharacter;
            listWithNewItems.Close += CloseList;

            listWithNewItems.Initialize(_creators.Characters, "Выберите противника");
            _tempView = listWithNewItems;
        }

        private void GoToCreationCharacter()
        {
            _audioManager.PlayClick();
            _tempView.DestroyView();
            ShowCreationPanel?.Invoke();
            _view.gameObject.SetActive(false);
        }

        private void AddThisCharacter(string name)
        {
            _audioManager.PlayClick();
            _tempView.DestroyView();
            _characters.Add(new Character(_creators.GetCharacterByName(name)));
            _characters[^1].ShowingName += $" {_characters.Count}";
            _view.UpdateCharactersList(_characters);
        }

        private void CloseList()
        {
            _audioManager.PlayCancel();
            _tempView.DestroyView();
        }

        private void TakeDamage()
        {
            if (_character == null)
                return;

            _audioManager.PlayClick();
            DamagePanelView damageView = _lvlFactory.Get(TypeScene.DamagePanel).GetComponent<DamagePanelView>();
            DamagePanelPresenter damagePresenter = (DamagePanelPresenter)_presenterFactory.Get(TypeScene.DamagePanel);
            damagePresenter.ReturnTextToArmor += ShowDamage;
            damagePresenter.DamageToShelter += DamageToShelter;
            damagePresenter.Initialize(damageView, _character);
        }

        private void DamageToShelter()
        {
            _character.ShelterPoint--;
            _character.ShelterHead = _character.ShelterHead > 0 ? _character.ShelterPoint : 0;
            _character.ShelterRightHand = _character.ShelterRightHand > 0 ? _character.ShelterPoint : 0;
            _character.ShelterLeftHand = _character.ShelterLeftHand > 0 ? _character.ShelterPoint : 0;
            _character.ShelterBody = _character.ShelterBody > 0 ? _character.ShelterPoint : 0;
            _character.ShelterRightLeg = _character.ShelterRightLeg > 0 ? _character.ShelterPoint : 0;
            _character.ShelterLeftLeg = _character.ShelterLeftLeg > 0 ? _character.ShelterPoint : 0;

            _view.UpdateArmors(_character);
        }

        private void ShowDamage(string text)
        {
            ShowText(text);
            _view.ShowCharacter(_character);
        }

        private void ShowText(string text)
        {
            if (_tempView != null)
                _tempView.DestroyView();

            PaneltTextInfoView paneltTextInfo = _lvlFactory.Get(TypeScene.TextInfo).GetComponent<PaneltTextInfoView>();
            paneltTextInfo.Close += CloseList;
            paneltTextInfo.Initialize(text);
            _tempView = paneltTextInfo;
        }

        private void StartBattle()
        {
            _audioManager.PlayClick();
            _view.ShowEndBattleButton();
            _characters = _characters.OrderByDescending(o => o.Initiative).ToList();
            _view.UpdateCharactersList(_characters);
        }

        private void EndBattle()
        {
            _audioManager.PlayClick();
            _view.ShowStartBattleButton();
        }

        private void ShowThisFeature(string name)
        {
            _audioManager.PlayClick();
            ShowText(_creators.GetDescription(name));
        }

        private void ShowThisCharacter(string name)
        {
            _audioManager.PlayClick();
            foreach (Character character in _characters)
                if (string.Compare(name, character.ShowingName, true) == 0)
                {
                    _character = character;
                    _view.ShowCharacter(character);
                    _weaponView.Initialize(GetWeaponsFromCharacter(character));
                    break;
                }
        }

        private List<Weapon> GetWeaponsFromCharacter(Character character)
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (Equipment equipment in character.Equipments)
                if (equipment is Weapon)
                    weapons.Add((Weapon)equipment);

            return weapons;
        }

        private void ShowHandbook()
        {
            if (_tempView != null)
                _tempView.DestroyView();

            _audioManager.PlayClick();
            ListWithNewItems listWith = _lvlFactory.Get(TypeScene.ListUniversal).GetComponent<ListWithNewItems>();
            listWith.Close += CloseList;
            listWith.ChooseThis += ShowThisFeature;
            listWith.Initialize(_creators.AllFeatures, "Выбирайте");
            _tempView = listWith;
        }



        private void NextTurn()
        {
            foreach (Character character in _characters)
                character.IsTurned = false;

            _view.UpdateCharactersList(_characters);
        }

        private void EndTurn()
        {
            _character.IsTurned = true;
            _view.UpdateCharactersList(_characters);
        }

        private void SaveScene()
        {
            if (_view.InputNameScene.Length == 0)
            {
                _audioManager.PlayWarning();
            }
            else
            {
                _audioManager.PlayClick();
                string charactersNames = "";
                string showingCharactersName = "";
                for (int i = 0; i < _characters.Count; i++)
                {
                    charactersNames += _characters[i].Name;
                    charactersNames += "/";

                    showingCharactersName += _characters[i].ShowingName;
                    showingCharactersName += "/";
                }
                charactersNames = charactersNames.TrimEnd('/');
                showingCharactersName = showingCharactersName.TrimEnd('/');

                SaveLoadScene saveScene = new SaveLoadScene();
                saveScene.charactersNames = charactersNames;
                saveScene.showingCharactersNames = showingCharactersName;
                saveScene.nameScene = _view.InputNameScene;

                var path = Path.Combine($"{Application.dataPath}/StreamingAssets/BattleScenes/", saveScene.nameScene + ".JSON");
                List<string> data = new List<string> { JsonUtility.ToJson(saveScene) };
                File.WriteAllLines(path, data);
                _creators.AddBattleScene(saveScene);
            }
        }

        private void ShowScenes()
        {
            if (_tempView != null)
                _tempView.DestroyView();

            _audioManager.PlayClick();
            ListWithNewItems listWithNewItems = _lvlFactory.Get(TypeScene.ListUniversal).GetComponent<ListWithNewItems>();
            listWithNewItems.ChooseThis += LoadScene;
            listWithNewItems.Close += CloseList;

            listWithNewItems.Initialize(_creators.Scenes, "Выберите сцену");
            _tempView = listWithNewItems;
        }

        private void LoadScene(string name)
        {
            _audioManager.PlayClick();
            _tempView.DestroyView();
            BattleScene battleScene = _creators.GetBattleSceneByName(name);
            _view.InputNameScene = battleScene.Name;

            foreach (Character character in battleScene.Characters)
                _characters.Add(character);

            _view.UpdateCharactersList(_characters);

        }

        private void CoverRightLeg(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterRightLeg > 0)
                _character.ShelterRightLeg = 0;
            else
                _character.ShelterRightLeg = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private void CoverRightHand(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterRightHand > 0)
                _character.ShelterRightHand = 0;
            else
                _character.ShelterRightHand = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private void CoverLeftLeg(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterLeftLeg > 0)
                _character.ShelterLeftLeg = 0;
            else
                _character.ShelterLeftLeg = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private void CoverLeftHand(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterLeftHand > 0)
                _character.ShelterLeftHand = 0;
            else
                _character.ShelterLeftHand = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private void CoverHead(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterHead > 0)
                _character.ShelterHead = 0;
            else
                _character.ShelterHead = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private void CoverBody(string points)
        {
            _audioManager.PlayClick();
            if (_character.ShelterBody > 0)
                _character.ShelterBody = 0;
            else
                _character.ShelterBody = ParseArmorPoint(points);

            _view.UpdateArmors(_character);
        }

        private int ParseArmorPoint(string points)
        {
            int.TryParse(points, out int armorPoints);
            return armorPoints;
        }

        private void ClearScene()
        {
            _audioManager.PlayClick();
            _view.ClearCharacterInputs();
            _weaponView.ClearWeapons();
            _character = null;
            _characters.Clear();
            _view.UpdateCharactersList(_characters);
        }

        private void RemoveThisCharacter(string name)
        {
            foreach (Character character in _characters)
                if (string.Compare(name, character.ShowingName, true) == 0)
                {
                    if (character == _character)
                    {
                        _character = null;
                        _view.ClearCharacterInputs();
                        _weaponView.ClearWeapons();
                    }
                    _characters.Remove(character);
                    break;
                }

            _view.UpdateCharactersList(_characters);

        }
        private void ParseInputs()
        {
            if (_character == null)
                return;

            _character.ShowingName = _view.NameCharacter;

            int.TryParse(_view.Wounds, out int wounds);
            _character.Wounds = wounds;

            int.TryParse(_view.HeadArmor, out int headArmor);
            int.TryParse(_view.HeadAblArmor, out int headAblArmor);
            int.TryParse(_view.HeadTotal, out int headTotal);
            _character.ArmorHead = headArmor;
            _character.ArmorAblHead = headAblArmor;
            _character.ArmorTotalHead = headTotal;


            int.TryParse(_view.RightHandArmor, out int rightHandArmor);
            int.TryParse(_view.RightHandAblArmor, out int rightHandAblArmor);
            int.TryParse(_view.RightHandTotal, out int rightHandTotal);
            _character.ArmorRightHand = rightHandArmor;
            _character.ArmorAblRightHand = rightHandAblArmor;
            _character.ArmorTotalRightHand = rightHandTotal;

            int.TryParse(_view.LeftHandArmor, out int leftHandArmor);
            int.TryParse(_view.LeftHandAblArmor, out int leftHandAblArmor);
            int.TryParse(_view.LeftHandTotal, out int leftHandTotal);
            _character.ArmorLeftHand = leftHandArmor;
            _character.ArmorAblLeftHand = leftHandAblArmor;
            _character.ArmorTotalLeftHand = leftHandTotal;

            int.TryParse(_view.BodyArmor, out int bodyArmor);
            int.TryParse(_view.BodyAblArmor, out int bodyAblArmor);
            int.TryParse(_view.BodyTotal, out int bodyTotal);
            _character.ArmorBody = bodyArmor;
            _character.ArmorAblBody = bodyAblArmor;
            _character.ArmorTotalBody = bodyTotal;

            int.TryParse(_view.RightLegArmor, out int rightLegArmor);
            int.TryParse(_view.RightLegAblArmor, out int rightLegAblArmor);
            int.TryParse(_view.RightLegTotal, out int rightLegTotal);
            _character.ArmorRightLeg = rightLegArmor;
            _character.ArmorAblRightLeg = rightLegAblArmor;
            _character.ArmorTotalRightLeg = rightLegTotal;

            int.TryParse(_view.LeftLegArmor, out int leftLegArmor);
            int.TryParse(_view.LeftLegAblArmor, out int leftLegAblArmor);
            int.TryParse(_view.LeftLegTotal, out int leftLegTotal);
            _character.ArmorLeftLeg = leftLegArmor;
            _character.ArmorAblLeftLeg = leftLegAblArmor;
            _character.ArmorTotalLeftLeg = leftLegTotal;


            int.TryParse(_view.Half, out int half);
            int.TryParse(_view.Full, out int full);
            int.TryParse(_view.Natisk, out int natisk);
            int.TryParse(_view.Run, out int run);
            _character.Half = half;
            _character.Full = full;
            _character.Natisk = natisk;
            _character.Run = run;

            int.TryParse(_view.WeaponSkill, out int weaponSkill);
            int.TryParse(_view.WeaponSkillSuper, out int weaponSkillSuper);
            _character.WeaponSkill = weaponSkill;
            _character.WeaponSkillSuper = weaponSkillSuper;

            int.TryParse(_view.BallisticSkill, out int ballisticSkill);
            int.TryParse(_view.BallisticSkillSuper, out int ballisticSkillSuper);
            _character.BallisticSkill = ballisticSkill;
            _character.BallisticSkillSuper = ballisticSkillSuper;

            int.TryParse(_view.Strength, out int strength);
            int.TryParse(_view.StrengthSuper, out int strengthSuper);
            _character.Strength = strength;
            _character.StrengthSuper = strengthSuper;

            int.TryParse(_view.Toughness, out int toughness);
            int.TryParse(_view.ToughnessSuper, out int toughnessSuper);
            _character.Toughness = toughness;
            _character.ToughnessSuper = toughnessSuper;

            int.TryParse(_view.Agility, out int agility);
            int.TryParse(_view.AgilitySuper, out int agilitySuper);
            _character.Agility = agility;
            _character.AgilitySuper = agilitySuper;

            int.TryParse(_view.Intelligence, out int intelligence);
            int.TryParse(_view.IntelligenceSuper, out int intelligenceSuper);
            _character.Intelligence = intelligence;
            _character.IntelligenceSuper = intelligenceSuper;

            int.TryParse(_view.Perception, out int perception);
            int.TryParse(_view.PerceptionSuper, out int perceptionSuper);
            _character.Perception = perception;
            _character.PerceptionSuper = perceptionSuper;

            int.TryParse(_view.Willpower, out int willpower);
            int.TryParse(_view.WillpowerSuper, out int willpowerSuper);
            _character.Willpower = willpower;
            _character.WillpowerSuper = willpowerSuper;

            int.TryParse(_view.Fellowship, out int fellowship);
            int.TryParse(_view.FellowshipSuper, out int fellowshipSuper);
            _character.Fellowship = fellowship;
            _character.FellowshipSuper = fellowshipSuper;

            int.TryParse(_view.Influence, out int influence);
            int.TryParse(_view.InfluenceSuper, out int influenceSuper);
            _character.Influence = influence;
            _character.InfluenceSuper = influenceSuper;

            int.TryParse(_view.Initiative, out int initiative);
            _character.Initiative = initiative;

            int.TryParse(_view.Shelter, out int shelter);
            _character.ShelterPoint = shelter;

            _view.UpdateCharactersList(_characters);
        }

        private void ToggleHorde()
        {
            _audioManager.PlayClick();
            _character.IsHorde = !_character.IsHorde;
        }

        private void ShowNPCNature()
        {
            _audioManager.PlayClick();
            _lvlFactory.Get(TypeScene.NPCNature);
        }
    }
}

