using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace CombarTracker
{
    public class MainSceneView : MonoBehaviour
    {
        [SerializeField] private Transform _contentWithCharacters;
        [SerializeField] private ItemWithSignals _characterInListPrefab;

        [SerializeField] private Transform _contentWithFeatures;
        [SerializeField] private ItemInList _featurePrefab;

        [SerializeField] private TMP_InputField _nameCharacter, _wounds;

        [SerializeField] private TMP_InputField _headArmor, _headAblArmor, _headTotal;
        [SerializeField] private TMP_InputField _rightHandArmor, _rightHandAblArmor, _rightHandTotal;
        [SerializeField] private TMP_InputField _leftHandArmor, _leftHandAblArmor, _leftHandTotal;
        [SerializeField] private TMP_InputField _bodyArmor, _bodyAblArmor, _bodyTotal;
        [SerializeField] private TMP_InputField _rightLegArmor, _rightLegAblArmor, _rightLegTotal;
        [SerializeField] private TMP_InputField _leftLegArmor, _leftLegAblArmor, _leftLegTotal;

        [SerializeField] private TMP_InputField _weaponSkill, _weaponSkillSuper;
        [SerializeField] private TMP_InputField _ballisticSkill, _ballisticSkillSuper;
        [SerializeField] private TMP_InputField _strength, _strengthSuper;
        [SerializeField] private TMP_InputField _toughness, _toughnessSuper;
        [SerializeField] private TMP_InputField _agility, _agilitySuper;
        [SerializeField] private TMP_InputField _intelligence, _intelligenceSuper;
        [SerializeField] private TMP_InputField _perception, _perceptionSuper;
        [SerializeField] private TMP_InputField _willpower, _willpowerSuper;
        [SerializeField] private TMP_InputField _fellowship, _fellowshipSuper;
        [SerializeField] private TMP_InputField _influence, _influenceSuper;
        [SerializeField] private TMP_InputField _initiative;

        [SerializeField] private TMP_InputField _half, _full, _natisk, _run;
        [SerializeField] private TMP_InputField _inputShelter, _inputNameScene;

        [SerializeField]
        private Button _buttonAddCharacter, _buttonStartBattle, _buttonEndBattle, _buttonNextTurn,
            _buttonHandbook, _buttonTakeDamage, _buttonEndTurn;
        [SerializeField] private Button _buttonSaveScene, _buttonLoadScene, _buttonClearScene;

        [SerializeField] private Button _buttonShelterHead, _buttonShelterRightHand, _buttonShelterLeftHand, _buttonShelterBody, _buttonShelterRightLeg, _buttonShelterLeftLeg;
        [SerializeField] private Sprite _activeShelter, _deactiveShelter;
        [SerializeField]
        private Image _imageButtonShelterHead, _imageButtonShelterRightHand, _imageButtonShelterLeftHand, _imageButtonShelterBody,
            _imageButtonShelterRightLeg, _imageButtonShelterLeftLeg;

        [SerializeField] private Toggle _toggleHorde;
        [SerializeField] private Button _buttonShowNPCNature;

        public event Action AddCharacter, StartBattle, NextTurn, ShowHandbook, EndTurn, TakeDamage, SaveScene, LoadScene, ClearScene,
            ChangeCharacterParameters, ToggleHorde, EndBattle, ShowNPC;
        public event Action<string> ShowThisCharacter, ShowThisFeature, RemoveThisCharacter;
        public event Action<string> CoverHead, CoverRightHand, CoverLeftHand, CoverBody, CoverRightLeg, CoverLeftLeg;
        private List<ItemInList> _features = new List<ItemInList>();
        private List<ItemWithSignals> _characters = new List<ItemWithSignals>();

        public string NameCharacter => _nameCharacter.text;
        public string Wounds => _wounds.text;
        public string HeadArmor => _headArmor.text;
        public string HeadAblArmor => _headAblArmor.text;
        public string HeadTotal => _headTotal.text;
        public string RightHandArmor => _rightHandArmor.text;
        public string RightHandAblArmor => _rightHandAblArmor.text;
        public string RightHandTotal => _rightHandTotal.text;
        public string LeftHandArmor => _leftHandArmor.text;
        public string LeftHandAblArmor => _leftHandAblArmor.text;
        public string LeftHandTotal => _leftHandTotal.text;
        public string BodyArmor => _bodyArmor.text;
        public string BodyAblArmor => _bodyAblArmor.text;
        public string BodyTotal => _bodyTotal.text;
        public string RightLegArmor => _rightLegArmor.text;
        public string RightLegAblArmor => _rightLegAblArmor.text;
        public string RightLegTotal => _rightLegTotal.text;
        public string LeftLegArmor => _leftLegArmor.text;
        public string LeftLegAblArmor => _leftLegAblArmor.text;
        public string LeftLegTotal => _leftLegTotal.text;
        public string WeaponSkill => _weaponSkill.text;
        public string WeaponSkillSuper => _weaponSkillSuper.text;
        public string BallisticSkill => _ballisticSkill.text;
        public string BallisticSkillSuper => _ballisticSkillSuper.text;
        public string Strength => _strength.text;
        public string StrengthSuper => _strengthSuper.text;
        public string Toughness => _toughness.text;
        public string ToughnessSuper => _toughnessSuper.text;
        public string Agility => _agility.text;
        public string AgilitySuper => _agilitySuper.text;
        public string Intelligence => _intelligence.text;
        public string IntelligenceSuper => _intelligenceSuper.text;
        public string Perception => _perception.text;
        public string PerceptionSuper => _perceptionSuper.text;
        public string Willpower => _willpower.text;
        public string WillpowerSuper => _willpowerSuper.text;
        public string Fellowship => _fellowship.text;
        public string FellowshipSuper => _fellowshipSuper.text;
        public string Influence => _influence.text;
        public string InfluenceSuper => _influenceSuper.text;
        public string Half => _half.text;
        public string Full => _full.text;
        public string Natisk => _natisk.text;
        public string Run => _run.text;
        public string Shelter => _inputShelter.text;
        public string InputNameScene { get => _inputNameScene.text; set => _inputNameScene.text = value; }
        public string Initiative => _initiative.text;

        private void OnEnable()
        {
            _buttonAddCharacter.onClick.AddListener(AddCharacterPressed);
            _buttonStartBattle.onClick.AddListener(StartBattlePressed);
            _buttonEndBattle.onClick.AddListener(EndBattlePressed);
            _buttonNextTurn.onClick.AddListener(NextTurnPressed);
            _buttonHandbook.onClick.AddListener(ShowHandbookPressed);
            _buttonEndTurn.onClick.AddListener(EndTurnPressed);
            _buttonTakeDamage.onClick.AddListener(TakeDamagePressed);

            _nameCharacter.onDeselect.AddListener(ChangeInInput);
            _wounds.onDeselect.AddListener(ChangeInInput);
            _headArmor.onDeselect.AddListener(ChangeInInput);
            _headAblArmor.onDeselect.AddListener(ChangeInInput);
            _headTotal.onDeselect.AddListener(ChangeInInput);
            _rightHandArmor.onDeselect.AddListener(ChangeInInput);
            _rightHandAblArmor.onDeselect.AddListener(ChangeInInput);
            _rightHandTotal.onDeselect.AddListener(ChangeInInput);
            _leftHandArmor.onDeselect.AddListener(ChangeInInput);
            _leftHandAblArmor.onDeselect.AddListener(ChangeInInput);
            _leftHandTotal.onDeselect.AddListener(ChangeInInput);
            _bodyArmor.onDeselect.AddListener(ChangeInInput);
            _bodyAblArmor.onDeselect.AddListener(ChangeInInput);
            _rightLegArmor.onDeselect.AddListener(ChangeInInput);
            _rightLegAblArmor.onDeselect.AddListener(ChangeInInput);
            _rightLegTotal.onDeselect.AddListener(ChangeInInput);
            _leftLegArmor.onDeselect.AddListener(ChangeInInput);
            _leftLegAblArmor.onDeselect.AddListener(ChangeInInput);
            _leftLegTotal.onDeselect.AddListener(ChangeInInput);

            _weaponSkill.onDeselect.AddListener(ChangeInInput);
            _weaponSkillSuper.onDeselect.AddListener(ChangeInInput);
            _ballisticSkill.onDeselect.AddListener(ChangeInInput);
            _ballisticSkillSuper.onDeselect.AddListener(ChangeInInput);
            _strength.onDeselect.AddListener(ChangeInInput);
            _strengthSuper.onDeselect.AddListener(ChangeInInput);
            _toughness.onDeselect.AddListener(ChangeInInput);
            _toughnessSuper.onDeselect.AddListener(ChangeInInput);
            _agility.onDeselect.AddListener(ChangeInInput);
            _agilitySuper.onDeselect.AddListener(ChangeInInput);
            _intelligence.onDeselect.AddListener(ChangeInInput);
            _intelligenceSuper.onDeselect.AddListener(ChangeInInput);
            _perception.onDeselect.AddListener(ChangeInInput);
            _perceptionSuper.onDeselect.AddListener(ChangeInInput);
            _willpower.onDeselect.AddListener(ChangeInInput);
            _willpowerSuper.onDeselect.AddListener(ChangeInInput);
            _fellowship.onDeselect.AddListener(ChangeInInput);
            _fellowshipSuper.onDeselect.AddListener(ChangeInInput);
            _influence.onDeselect.AddListener(ChangeInInput);
            _influenceSuper.onDeselect.AddListener(ChangeInInput);
            _inputShelter.onDeselect.AddListener(ChangeInInput);
            _initiative.onDeselect.AddListener(ChangeInInput);

            _half.onDeselect.AddListener(ChangeInInput);
            _full.onDeselect.AddListener(ChangeInInput);
            _natisk.onDeselect.AddListener(ChangeInInput);
            _run.onDeselect.AddListener(ChangeInInput);

            _buttonShelterHead.onClick.AddListener(CoverHeadPressed);
            _buttonShelterRightHand.onClick.AddListener(CoverRightHandPressed);
            _buttonShelterLeftHand.onClick.AddListener(CoverLeftHandPressed);
            _buttonShelterBody.onClick.AddListener(CoverBodyPressed);
            _buttonShelterRightLeg.onClick.AddListener(CoverRightLegPressed);
            _buttonShelterLeftLeg.onClick.AddListener(CoverLeftLegPressed);

            _buttonSaveScene.onClick.AddListener(SaveScenePressed);
            _buttonLoadScene.onClick.AddListener(LoadScenePressed);
            _buttonClearScene.onClick.AddListener(ClearScenePressed);

            _toggleHorde.onValueChanged.AddListener(ToggleHordePressed);

            _buttonShowNPCNature.onClick.AddListener(ShowNPCPressed);
        }


        private void OnDisable()
        {
            _buttonAddCharacter.onClick.RemoveAllListeners();
            _buttonStartBattle.onClick.RemoveAllListeners();
            _buttonEndBattle.onClick.RemoveAllListeners();
            _buttonNextTurn.onClick.RemoveAllListeners();
            _buttonHandbook.onClick.RemoveAllListeners();
            _buttonEndTurn.onClick.RemoveAllListeners();
            _buttonTakeDamage.onClick.RemoveAllListeners();

            _nameCharacter.onDeselect.RemoveAllListeners();
            _wounds.onDeselect.RemoveAllListeners();
            _headArmor.onDeselect.RemoveAllListeners();
            _headAblArmor.onDeselect.RemoveAllListeners();
            _headTotal.onDeselect.RemoveAllListeners();
            _rightHandArmor.onDeselect.RemoveAllListeners();
            _rightHandAblArmor.onDeselect.RemoveAllListeners();
            _rightHandTotal.onDeselect.RemoveAllListeners();
            _leftHandArmor.onDeselect.RemoveAllListeners();
            _leftHandAblArmor.onDeselect.RemoveAllListeners();
            _leftHandTotal.onDeselect.RemoveAllListeners();
            _bodyArmor.onDeselect.RemoveAllListeners();
            _bodyAblArmor.onDeselect.RemoveAllListeners();
            _rightLegArmor.onDeselect.RemoveAllListeners();
            _rightLegAblArmor.onDeselect.RemoveAllListeners();
            _rightLegTotal.onDeselect.RemoveAllListeners();
            _leftLegArmor.onDeselect.RemoveAllListeners();
            _leftLegAblArmor.onDeselect.RemoveAllListeners();
            _leftLegTotal.onDeselect.RemoveAllListeners();

            _weaponSkill.onDeselect.RemoveAllListeners();
            _weaponSkillSuper.onDeselect.RemoveAllListeners();
            _ballisticSkill.onDeselect.RemoveAllListeners();
            _ballisticSkillSuper.onDeselect.RemoveAllListeners();
            _strength.onDeselect.RemoveAllListeners();
            _strengthSuper.onDeselect.RemoveAllListeners();
            _toughness.onDeselect.RemoveAllListeners();
            _toughnessSuper.onDeselect.RemoveAllListeners();
            _agility.onDeselect.RemoveAllListeners();
            _agilitySuper.onDeselect.RemoveAllListeners();
            _intelligence.onDeselect.RemoveAllListeners();
            _intelligenceSuper.onDeselect.RemoveAllListeners();
            _perception.onDeselect.RemoveAllListeners();
            _perceptionSuper.onDeselect.RemoveAllListeners();
            _willpower.onDeselect.RemoveAllListeners();
            _willpowerSuper.onDeselect.RemoveAllListeners();
            _fellowship.onDeselect.RemoveAllListeners();
            _fellowshipSuper.onDeselect.RemoveAllListeners();
            _influence.onDeselect.RemoveAllListeners();
            _influenceSuper.onDeselect.RemoveAllListeners();
            _inputShelter.onDeselect.RemoveAllListeners();
            _initiative.onDeselect.RemoveAllListeners();

            _half.onDeselect.RemoveAllListeners();
            _full.onDeselect.RemoveAllListeners();
            _natisk.onDeselect.RemoveAllListeners();
            _run.onDeselect.RemoveAllListeners();

            _buttonShelterHead.onClick.RemoveAllListeners();
            _buttonShelterRightHand.onClick.RemoveAllListeners();
            _buttonShelterLeftHand.onClick.RemoveAllListeners();
            _buttonShelterBody.onClick.RemoveAllListeners();
            _buttonShelterRightLeg.onClick.RemoveAllListeners();
            _buttonShelterLeftLeg.onClick.RemoveAllListeners();

            _toggleHorde.onValueChanged.RemoveAllListeners();
        }

        public void ShowCharacter(Character character)
        {
            _nameCharacter.text = character.ShowingName;
            _wounds.text = character.Wounds.ToString();

            UpdateArmors(character);

            _half.text = character.Half.ToString();
            _full.text = character.Full.ToString();
            _natisk.text = character.Natisk.ToString();
            _run.text = character.Run.ToString();

            _weaponSkill.text = character.WeaponSkill.ToString();
            _weaponSkillSuper.text = character.WeaponSkillSuper.ToString();

            _ballisticSkill.text = character.BallisticSkill.ToString();
            _ballisticSkillSuper.text = character.BallisticSkillSuper.ToString();

            _strength.text = character.Strength.ToString();
            _strengthSuper.text = character.StrengthSuper.ToString();

            _toughness.text = character.Toughness.ToString();
            _toughnessSuper.text = character.ToughnessSuper.ToString();

            _agility.text = character.Agility.ToString();
            _agilitySuper.text = character.AgilitySuper.ToString();

            _intelligence.text = character.Intelligence.ToString();
            _intelligenceSuper.text = character.IntelligenceSuper.ToString();

            _perception.text = character.Perception.ToString();
            _perceptionSuper.text = character.PerceptionSuper.ToString();

            _willpower.text = character.Willpower.ToString();
            _willpowerSuper.text = character.WillpowerSuper.ToString();

            _fellowship.text = character.Fellowship.ToString();
            _fellowshipSuper.text = character.FellowshipSuper.ToString();

            _influence.text = character.Influence.ToString();
            _influenceSuper.text = character.InfluenceSuper.ToString();
            _initiative.text = character.Initiative.ToString();

            _toggleHorde.SetIsOnWithoutNotify(character.IsHorde);

            UpdateFeatureList(character);
        }

        public void UpdateCharactersList(List<Character> characters)
        {
            List<ItemInList> items = new List<ItemInList>();
            items.AddRange(_characters);
            ClearListWithItems(items);
            _characters.Clear();
            foreach (Character character in characters)
            {
                ItemWithSignals item = Instantiate(_characterInListPrefab, _contentWithCharacters);
                item.ChooseThis += ShowThisCharacter.Invoke;
                item.AdditionalButtonAction += RemoveThisCharacterPressed;
                item.Initialize(character.ShowingName, character.IsTurned);
                _characters.Add(item);
            }
        }

        public void UpdateArmors(Character character)
        {
            _headArmor.text = (character.ArmorHead + character.ShelterHead).ToString();
            _headAblArmor.text = character.ArmorAblHead.ToString();
            _headTotal.text = (character.ArmorTotalHead + character.ShelterHead).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterHead, character.ShelterHead);

            _rightHandArmor.text = (character.ArmorRightHand + character.ShelterRightHand).ToString();
            _rightHandAblArmor.text = character.ArmorAblRightHand.ToString();
            _rightHandTotal.text = (character.ArmorTotalRightHand + character.ShelterRightHand).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterRightHand, character.ShelterRightHand);

            _leftHandArmor.text = (character.ArmorLeftHand + character.ShelterLeftHand).ToString();
            _leftHandAblArmor.text = character.ArmorAblLeftHand.ToString();
            _leftHandTotal.text = (character.ArmorTotalLeftHand + character.ShelterLeftHand).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterLeftHand, character.ShelterLeftHand);

            _bodyArmor.text = (character.ArmorBody + character.ShelterBody).ToString();
            _bodyAblArmor.text = character.ArmorAblBody.ToString();
            _bodyTotal.text = (character.ArmorTotalBody + character.ShelterBody).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterBody, character.ShelterBody);

            _rightLegArmor.text = (character.ArmorRightLeg + character.ShelterRightLeg).ToString();
            _rightLegAblArmor.text = character.ArmorAblRightLeg.ToString();
            _rightLegTotal.text = (character.ArmorTotalRightLeg + character.ShelterRightLeg).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterRightLeg, character.ShelterRightLeg);

            _leftLegArmor.text = (character.ArmorLeftLeg + character.ShelterLeftLeg).ToString();
            _leftLegAblArmor.text = character.ArmorAblLeftLeg.ToString();
            _leftLegTotal.text = (character.ArmorTotalLeftLeg + character.ShelterLeftLeg).ToString();
            SetActiveORDeactiveSpriteForButtonShelter(_imageButtonShelterLeftLeg, character.ShelterLeftLeg);

            _inputShelter.text = character.ShelterPoint.ToString();
        }

        public void ClearCharacterInputs()
        {
            _nameCharacter.text = "";
            _wounds.text = "";
            _headArmor.text = "";
            _headAblArmor.text = "";
            _headTotal.text = "";
            _rightHandArmor.text = "";
            _rightHandAblArmor.text = "";
            _rightHandTotal.text = "";
            _leftHandArmor.text = "";
            _leftHandAblArmor.text = "";
            _leftHandTotal.text = "";
            _bodyArmor.text = "";
            _bodyAblArmor.text = "";
            _rightLegArmor.text = "";
            _rightLegAblArmor.text = "";
            _rightLegTotal.text = "";
            _leftLegArmor.text = "";
            _leftLegAblArmor.text = "";
            _leftLegTotal.text = "";

            _weaponSkill.text = "";
            _weaponSkillSuper.text = "";
            _ballisticSkill.text = "";
            _ballisticSkillSuper.text = "";
            _strength.text = "";
            _strengthSuper.text = "";
            _toughness.text = "";
            _toughnessSuper.text = "";
            _agility.text = "";
            _agilitySuper.text = "";
            _intelligence.text = "";
            _intelligenceSuper.text = "";
            _perception.text = "";
            _perceptionSuper.text = "";
            _willpower.text = "";
            _willpowerSuper.text = "";
            _fellowship.text = "";
            _fellowshipSuper.text = "";
            _influence.text = "";
            _influenceSuper.text = "";
            _inputShelter.text = "";

            _half.text = "";
            _full.text = "";
            _natisk.text = "";
            _run.text = "";
            ClearListWithItems(_features);
        }

        public void ShowStartBattleButton()
        {
            _buttonStartBattle.gameObject.SetActive(true);
            _buttonEndBattle.gameObject.SetActive(false);
        }

        public void ShowEndBattleButton()
        {
            _buttonStartBattle.gameObject.SetActive(false);
            _buttonEndBattle.gameObject.SetActive(true);
        }

        private void SetActiveORDeactiveSpriteForButtonShelter(Image image, int amount)
        {
            if (amount > 0)
                image.sprite = _activeShelter;
            else
                image.sprite = _deactiveShelter;
        }

        private void UpdateFeatureList(Character character)
        {
            ClearListWithItems(_features);
            List<Trait> allFeatures = new List<Trait>();
            allFeatures.AddRange(character.Skills);
            allFeatures.AddRange(character.Features);
            allFeatures.AddRange(character.Talents);
            allFeatures.AddRange(character.PsyPowers);

            foreach (Trait feature in allFeatures)
                CreateFeatureInList(feature.Name, feature.Lvl);

            foreach (Equipment equipment in character.Equipments)
                CreateFeatureInList(equipment.Name, equipment.Amount);

            foreach (MechImplant implant in character.Implants)
                CreateFeatureInList(implant.Name);
        }

        private void CreateFeatureInList(string name, int amount = 0)
        {
            ItemInList item = Instantiate(_featurePrefab, _contentWithFeatures);
            item.ChooseThis += ShowThisFeature.Invoke;
            if (amount == 0)
                item.Initialize(name);
            else
                item.Initialize(name, $"{name}({amount})");
            _features.Add(item);
        }

        private void ClearListWithItems(List<ItemInList> items)
        {
            foreach (ItemInList item in items)
                Destroy(item.gameObject);

            items.Clear();
        }

        private void TakeDamagePressed() => TakeDamage?.Invoke();

        private void ChangeInInput(string text) => ChangeCharacterParameters?.Invoke();

        private void EndTurnPressed() => EndTurn?.Invoke();

        private void NextTurnPressed() => NextTurn?.Invoke();

        private void ShowHandbookPressed() => ShowHandbook?.Invoke();

        private void StartBattlePressed() => StartBattle?.Invoke();

        private void AddCharacterPressed() => AddCharacter?.Invoke();

        private void CoverLeftLegPressed() => CoverLeftLeg?.Invoke(_inputShelter.text);

        private void CoverRightLegPressed() => CoverRightLeg?.Invoke(_inputShelter.text);

        private void CoverBodyPressed() => CoverBody?.Invoke(_inputShelter.text);

        private void CoverLeftHandPressed() => CoverLeftHand?.Invoke(_inputShelter.text);

        private void CoverRightHandPressed() => CoverRightHand?.Invoke(_inputShelter.text);

        private void CoverHeadPressed() => CoverHead?.Invoke(_inputShelter.text);

        private void ClearScenePressed() => ClearScene?.Invoke();

        private void LoadScenePressed() => LoadScene?.Invoke();

        private void SaveScenePressed() => SaveScene?.Invoke();

        private void RemoveThisCharacterPressed(string name) => RemoveThisCharacter?.Invoke(name);

        private void ToggleHordePressed(bool arg0) => ToggleHorde?.Invoke();

        private void EndBattlePressed() => EndBattle?.Invoke();

        private void ShowNPCPressed() => ShowNPC?.Invoke();
    }
}

