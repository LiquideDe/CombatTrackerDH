using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CreationCharacterView : CanDestroyView
{
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

    [SerializeField] private TMP_InputField _half, _full, _natisk, _run;

    [SerializeField]
    private Button _buttonDone, _buttonCancel, _buttonAddSkill, _buttonAddTalent, _buttonAddFeature, _buttonAddPsypower, _buttonAddImplant,
        _buttonAddWeapon, _buttonAddBallistic, _buttonAddGrenade, _buttonAddArmor, _buttonAddThing, _buttonCalculateAll;

    [SerializeField] private Transform _contentFeatures;
    [SerializeField] private ItemWithInput _featurePrefab;

    [SerializeField] private Transform _contentGuns;
    [SerializeField] private WeaponItemPassive _weaponItemPrefab;

    public event Action AddSkill, AddTalent, AddFeature, AddPsypower, AddImplant, AddWeapon, AddBallistic, AddGrenade, AddArmor, AddThing, Warning, CalculateAll;
    public event Action<SaveLoadCharacter> Done;
    public event Action<string> RemoveFeature;
    public event Action<string, int> ChangeLvl;

    private List<ItemWithInput> _featuresItems = new List<ItemWithInput>();
    private List<WeaponItemPassive> _guns = new List<WeaponItemPassive>();

    private void OnEnable()
    {
        _buttonDone.onClick.AddListener(DonePressed);
        _buttonCancel.onClick.AddListener(ClosePressed);
        _buttonAddSkill.onClick.AddListener(AddSkillPressed);
        _buttonAddTalent.onClick.AddListener(AddTalentPressed);
        _buttonAddFeature.onClick.AddListener(AddFeaturePressed);
        _buttonAddPsypower.onClick.AddListener(AddPsypowerPressed);
        _buttonAddImplant.onClick.AddListener(AddImplantPressed);
        _buttonAddWeapon.onClick.AddListener(AddWeaponPressed);
        _buttonAddBallistic.onClick.AddListener(AddBallisticPressed);
        _buttonAddGrenade.onClick.AddListener(AddGrenadePressed);
        _buttonAddArmor.onClick.AddListener(AddArmorPressed);
        _buttonAddThing.onClick.AddListener(AddThingPressed);
        _buttonCalculateAll.onClick.AddListener(CalculateAllPressed);
    }

    private void OnDisable()
    {
        _buttonDone.onClick.RemoveAllListeners();
        _buttonCancel.onClick.RemoveAllListeners();
        _buttonAddSkill.onClick.RemoveAllListeners();
        _buttonAddTalent.onClick.RemoveAllListeners();
        _buttonAddFeature.onClick.RemoveAllListeners();
        _buttonAddPsypower.onClick.RemoveAllListeners();
        _buttonAddImplant.onClick.RemoveAllListeners();
        _buttonAddWeapon.onClick.RemoveAllListeners();
        _buttonAddBallistic.onClick.RemoveAllListeners();
        _buttonAddGrenade.onClick.RemoveAllListeners();
        _buttonAddArmor.onClick.RemoveAllListeners();
        _buttonAddThing.onClick.RemoveAllListeners();
        _buttonCalculateAll.onClick.RemoveAllListeners();
    }

    public void UpdateListFeatures(List<Feature> features)
    {
        foreach (ItemWithInput item in _featuresItems)
            Destroy(item.gameObject);

        _featuresItems.Clear();

        foreach(Feature feature in features)
        {
            ItemWithInput item = Instantiate(_featurePrefab, _contentFeatures);
            item.ChooseThis += RemoveThisFeature;
            item.ChangeLvl += ChangeLvlPressed;
            item.Initialize(feature.Name, feature.Lvl);
            _featuresItems.Add(item);
        }
    }

    public void UpdateGuns(List<Weapon> weapons)
    {
        foreach (WeaponItemPassive itemPassive in _guns)
            Destroy(itemPassive.gameObject);

        _guns.Clear();

        foreach(Weapon weapon in weapons)
        {
            WeaponItemPassive passive = Instantiate(_weaponItemPrefab, _contentGuns);
            _guns.Add(passive);
            passive.Initialize(weapon);
        }
    }

    public int Toughness()
    {
        int.TryParse(_toughness.text, out int toughness);
        return toughness;
    }

    public int Agility()
    {
        int.TryParse(_agility.text, out int agility);
        return agility;
    }

    public void SetArmors(SaveLoadCharacter save)
    {
        _headAblArmor.text = save.armorAblHead.ToString();
        _headArmor.text = save.armorHead.ToString();
        _headTotal.text = save.armorTotalHead.ToString();

        _rightHandAblArmor.text = save.armorAblRightHand.ToString();
        _rightHandArmor.text = save.armorRightHand.ToString();
        _rightHandTotal.text = save.armorTotalRightHand.ToString();

        _leftHandAblArmor.text = save.armorAblLeftHand.ToString();
        _leftHandArmor.text = save.armorLeftHand.ToString();
        _leftHandTotal.text = save.armorTotalLeftHand.ToString();

        _bodyAblArmor.text = save.armorAblBody.ToString();
        _bodyArmor.text = save.armorBody.ToString();
        _bodyTotal.text = save.armorTotalBody.ToString();

        _rightLegAblArmor.text = save.armorAblRightLeg.ToString();
        _rightLegArmor.text = save.armorRightLeg.ToString();
        _rightLegTotal.text = save.armorTotalRightLeg.ToString();

        _leftLegAblArmor.text = save.armorAblLeftLeg.ToString();
        _leftLegArmor.text = save.armorLeftLeg.ToString();
        _leftLegTotal.text = save.armorTotalLeftLeg.ToString();

        _half.text = save.half.ToString();
        _full.text = save.full.ToString();
        _natisk.text = save.natisk.ToString();
        _run.text = save.run.ToString();

        if (save.toughnessSuper > 0)
            _toughnessSuper.text = save.toughnessSuper.ToString();

    }

    private void RemoveThisFeature(string name) => RemoveFeature?.Invoke(name);

    private void ChangeLvlPressed(string name, int lvl) => ChangeLvl?.Invoke(name, lvl);

    private void AddThingPressed() => AddThing?.Invoke();

    private void AddArmorPressed() => AddArmor?.Invoke();

    private void AddGrenadePressed() => AddGrenade?.Invoke();

    private void AddBallisticPressed() => AddBallistic?.Invoke();

    private void AddWeaponPressed() => AddWeapon?.Invoke();

    private void AddImplantPressed() => AddImplant?.Invoke();

    private void AddPsypowerPressed() => AddPsypower?.Invoke();

    private void AddFeaturePressed() => AddFeature?.Invoke();

    private void AddTalentPressed() => AddTalent?.Invoke();

    private void AddSkillPressed() => AddSkill?.Invoke();

    private void CalculateAllPressed() => CalculateAll?.Invoke();

    private void DonePressed()
    {
        if (_headTotal.text.Length > 0 && _rightHandTotal.text.Length > 0 && _leftHandTotal.text.Length > 0 && _bodyTotal.text.Length > 0 && _rightLegTotal.text.Length > 0 &&
           _leftLegTotal.text.Length > 0 && _weaponSkill.text.Length > 0 && _ballisticSkill.text.Length > 0 && _strength.text.Length > 0 && _toughness.text.Length > 0 &&
           _agility.text.Length > 0 && _intelligence.text.Length > 0 && _perception.text.Length > 0 && _willpower.text.Length > 0 && _fellowship.text.Length > 0 && _influence.text.Length > 0 && 
           _half.text.Length > 0 && _full.text.Length > 0 && _natisk.text.Length > 0 && _run.text.Length > 0 && _nameCharacter.text.Length > 0 && _wounds.text.Length > 0)
        {
            SaveLoadCharacter loadCharacter = new SaveLoadCharacter();
            loadCharacter.name = _nameCharacter.text;
            loadCharacter.showingName = _nameCharacter.text;

            int.TryParse(_headArmor.text, out loadCharacter.armorHead);
            int.TryParse(_headAblArmor.text, out loadCharacter.armorAblHead);
            int.TryParse(_headTotal.text, out loadCharacter.armorTotalHead);

            int.TryParse(_rightHandArmor.text, out loadCharacter.armorRightHand);
            int.TryParse(_rightHandAblArmor.text, out loadCharacter.armorAblRightHand);
            int.TryParse(_rightHandTotal.text, out loadCharacter.armorTotalRightHand);

            int.TryParse(_leftHandArmor.text, out loadCharacter.armorLeftHand);
            int.TryParse(_leftHandAblArmor.text, out loadCharacter.armorAblLeftHand);
            int.TryParse(_leftHandTotal.text, out loadCharacter.armorTotalLeftHand);

            int.TryParse(_bodyArmor.text, out loadCharacter.armorBody);
            int.TryParse(_bodyAblArmor.text, out loadCharacter.armorAblBody);
            int.TryParse(_bodyTotal.text, out loadCharacter.armorTotalBody);

            int.TryParse(_rightLegArmor.text, out loadCharacter.armorRightLeg);
            int.TryParse(_rightLegAblArmor.text, out loadCharacter.armorAblRightLeg);
            int.TryParse(_rightLegTotal.text, out loadCharacter.armorTotalRightLeg);

            int.TryParse(_leftLegArmor.text, out loadCharacter.armorLeftLeg);
            int.TryParse(_leftLegAblArmor.text, out loadCharacter.armorAblLeftLeg);
            int.TryParse(_leftLegTotal.text, out loadCharacter.armorTotalLeftLeg);

            int.TryParse(_weaponSkill.text, out loadCharacter.weaponSkill);
            int.TryParse(_ballisticSkill.text, out loadCharacter.ballisticSkill);
            int.TryParse(_strength.text, out loadCharacter.strength);
            int.TryParse(_toughness.text, out loadCharacter.toughness);
            int.TryParse(_agility.text, out loadCharacter.agility);
            int.TryParse(_intelligence.text, out loadCharacter.intelligence);
            int.TryParse(_perception.text, out loadCharacter.perception);
            int.TryParse(_willpower.text, out loadCharacter.willpower);
            int.TryParse(_fellowship.text, out loadCharacter.fellowship);
            int.TryParse(_influence.text, out loadCharacter.influence);

            int.TryParse(_weaponSkillSuper.text, out loadCharacter.weaponSkillSuper);
            int.TryParse(_ballisticSkillSuper.text, out loadCharacter.ballisticSkillSuper);
            int.TryParse(_strengthSuper.text, out loadCharacter.strengthSuper);
            int.TryParse(_toughnessSuper.text, out loadCharacter.toughnessSuper);
            int.TryParse(_agilitySuper.text, out loadCharacter.agilitySuper);
            int.TryParse(_intelligenceSuper.text, out loadCharacter.intelligenceSuper);
            int.TryParse(_perceptionSuper.text, out loadCharacter.perceptionSuper);
            int.TryParse(_willpowerSuper.text, out loadCharacter.willpowerSuper);
            int.TryParse(_fellowshipSuper.text, out loadCharacter.fellowshipSuper);
            int.TryParse(_influenceSuper.text, out loadCharacter.influenceSuper);

            int.TryParse(_half.text, out loadCharacter.half);
            int.TryParse(_full.text, out loadCharacter.full);
            int.TryParse(_natisk.text, out loadCharacter.natisk);
            int.TryParse(_run.text, out loadCharacter.run);

            int.TryParse(_wounds.text, out loadCharacter.wounds);

            Done?.Invoke(loadCharacter);
        }
        else
            Warning?.Invoke();
    }

    


}