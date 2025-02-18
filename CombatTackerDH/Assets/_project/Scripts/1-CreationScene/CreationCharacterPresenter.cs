using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using static UnityEditor.Progress;
using UnityEngine.TextCore.Text;

namespace CombarTracker
{
    public class CreationCharacterPresenter : IPresenter
    {
        public event Action Close;
        private CreationCharacterView _view;
        private Creators _creators;
        private LvlFactory _lvlFactory;
        private AudioManager _audioManager;
        private CanDestroyView _tempView;
        private CanDestroyView _propertyView;
        private List<MechImplant> _implants = new List<MechImplant>();
        private List<Equipment> _equipments = new List<Equipment>();
        private List<Trait> _traits = new List<Trait>();
        private List<Trait> _skills = new List<Trait>();
        private List<Trait> _talents = new List<Trait>();
        private List<Trait> _psyPowers = new List<Trait>();

        public CreationCharacterPresenter(CreationCharacterView view, Creators creators, LvlFactory lvlFactory, AudioManager audioManager)
        {
            _view = view;
            _creators = creators;
            _lvlFactory = lvlFactory;
            _audioManager = audioManager;
            Subscribe();
        }

        private delegate void MethodForListCreateNew();
        private delegate void MethodForListChooseOne(string name);

        private void Subscribe()
        {
            _view.AddArmor += ShowArmors;
            _view.AddBallistic += ShowRange;
            _view.AddFeature += ShowFeatures;
            _view.AddGrenade += ShowGrenades;
            _view.AddImplant += ShowImplants;
            _view.AddPsypower += Showpsypowers;
            _view.AddSkill += ShowSkills;
            _view.AddTalent += ShowTalents;
            _view.AddThing += ShowThings;
            _view.AddWeapon += ShowWeapons;
            _view.Close += CancelPressed;
            _view.ChangeLvl += ChangeLvl;
            _view.Done += Done;
            _view.RemoveTrait += RemoveTrait;
            _view.Warning += _audioManager.PlayWarning;
            _view.CalculateAll += CalculateArmors;
        }

        private void Unscribe()
        {
            _view.AddArmor -= ShowArmors;
            _view.AddBallistic -= ShowRange;
            _view.AddFeature -= ShowFeatures;
            _view.AddGrenade -= ShowGrenades;
            _view.AddImplant -= ShowImplants;
            _view.AddPsypower -= Showpsypowers;
            _view.AddSkill -= ShowSkills;
            _view.AddTalent -= ShowTalents;
            _view.AddThing -= ShowThings;
            _view.AddWeapon -= ShowWeapons;
            _view.Close -= CancelPressed;
            _view.ChangeLvl -= ChangeLvl;
            _view.Done -= Done;
            _view.RemoveTrait -= RemoveTrait;
            _view.Warning -= _audioManager.PlayWarning;
            _view.CalculateAll -= CalculateArmors;
        }

        private void ShowArmors()
        {
            _audioManager.PlayClick();
            List<Armor> armors = new List<Armor>();
            foreach (Equipment equipment in _creators.Equipments)
                if (equipment.TypeEq == Equipment.TypeEquipment.Armor)
                {
                    Armor armor = (Armor)equipment;
                    armors.Add(armor);
                }

            ShowList(armors, ShowNewArmorForm, ChooseEquipment, "Выберите броню");
        }

        private void ChooseEquipment(string name)
        {
            _audioManager.PlayDone();
            Debug.Log($"Добавили новый {name}");
            _equipments.Add(_creators.GetEquipmentByName(name));
            CloseAllListAndForms();
            UpdateFeaturesView();
            UpdateWeapons();
        }

        private void ShowNewArmorForm()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            NewArmor newArmor = _lvlFactory.Get(TypeScene.CreationArmor).GetComponent<NewArmor>();
            newArmor.Close += CloseCreationForm;
            newArmor.Cancel += CloseForm;
            newArmor.ReturnNewEquipment += AddNewEquipment;
            newArmor.WrongInput += _audioManager.PlayWarning;
            _tempView = newArmor;
            newArmor.Initialize();
        }

        private void CloseCreationForm()
        {
            _audioManager.PlayCancel();
            _tempView.DestroyView();
        }
        private void AddNewEquipment(Equipment equipment)
        {
            CloseAllListAndForms();
            _audioManager.PlayDone();
            _equipments.Add(equipment);
            _creators.Equipments.Add(equipment);
            //_tempView.DestroyView();
            UpdateFeaturesView();
            UpdateWeapons();
        }

        private void ShowRange() => ShowList(ShowWeapons(Equipment.TypeEquipment.Range), ShowNewRangeForm, ChooseEquipment, "Выберите стрелковое");

        private void ShowNewRangeForm()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            NewRange newRange = _lvlFactory.Get(TypeScene.CreationRange).GetComponent<NewRange>();
            newRange.Cancel += CloseForm;
            newRange.NeedInProperties += ShowPropertiesList;
            newRange.ReturnNewEquipment += AddNewEquipment;
            newRange.WrongInput += _audioManager.PlayWarning;
            _tempView = newRange;
            newRange.Initialize();
        }

        private void CloseForm(CanDestroyView gameObject)
        {
            gameObject.DestroyView();
        }

        private void ShowPropertiesList()
        {
            _audioManager.PlayClick();
            ShowList(_creators.WeaponProp, AddProperty, "Выберите особенность", true);
        }

        private void AddProperty(string name)
        {
            _audioManager.PlayDone();
            //CloseAllListAndForms();
            _propertyView.DestroyView();

            CreatorNewEquipment creatorNewEquipment = (CreatorNewEquipment)_tempView;
            creatorNewEquipment.AddProperty(name);
        }

        private List<Weapon> ShowWeapons(Equipment.TypeEquipment typeEquipment)
        {
            _audioManager.PlayClick();
            List<Weapon> weapons = new List<Weapon>();
            foreach (Equipment equipment in _creators.Equipments)
                if (equipment.TypeEq == typeEquipment)
                {
                    Weapon weapon = (Weapon)equipment;
                    weapons.Add(weapon);
                }

            return weapons;
        }

        private void ShowGrenades() => ShowList(ShowWeapons(Equipment.TypeEquipment.Grenade), ShowGrenadeNewForm, ChooseEquipment, "Выберите гранату");

        private void ShowGrenadeNewForm()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            NewGrenade newGrenade = _lvlFactory.Get(TypeScene.CreationGrenade).GetComponent<NewGrenade>();
            newGrenade.Cancel += CloseForm;
            newGrenade.NeedInProperties += ShowPropertiesList;
            newGrenade.ReturnNewEquipment += AddNewEquipment;
            newGrenade.WrongInput += _audioManager.PlayWarning;
            _tempView = newGrenade;
            newGrenade.Initialize();
        }

        private void ShowWeapons() => ShowList(ShowWeapons(Equipment.TypeEquipment.Melee), ShowMeleeNewForm, ChooseEquipment, "Выберите оружие ближнего боя");

        private void ShowMeleeNewForm()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            NewMelee newMelee = _lvlFactory.Get(TypeScene.CreationMelee).GetComponent<NewMelee>();
            newMelee.Cancel += CloseForm;
            newMelee.NeedInProperties += ShowPropertiesList;
            newMelee.ReturnNewEquipment += AddNewEquipment;
            newMelee.WrongInput += _audioManager.PlayWarning;
            _tempView = newMelee;
            newMelee.Initialize();
        }

        private void ShowThings()
        {
            List<Equipment> equipments = new List<Equipment>();
            foreach (Equipment equipment in _creators.Equipments)
                if (equipment.TypeEq == Equipment.TypeEquipment.Thing)
                    equipments.Add(equipment);
            ShowList(equipments, ShowNewEquipment, ChooseEquipment, "Выберите предмет");
        }

        private void ShowNewEquipment()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            CreatorNewEquipment newEquipment = _lvlFactory.Get(TypeScene.CreationThing).GetComponent<CreatorNewEquipment>();
            newEquipment.Cancel += CloseForm;
            newEquipment.ReturnNewEquipment += AddNewEquipment;
            newEquipment.WrongInput += _audioManager.PlayWarning;
            _tempView = newEquipment;
            newEquipment.Initialize();
        }

        private void ShowSkills()
        {
            _audioManager.PlayClick();
            ShowList(_creators.Skills, AddSkill, "Выберите навык");
        }

        private void AddSkill(string name) => AddTraitToList(_creators.GetSkillByName(name), _skills);

        private void ShowFeatures()
        {
            _audioManager.PlayClick();
            ShowList(_creators.Features, AddTrait, "Выберите черту");
        }

        private void AddTrait(string name) => AddTraitToList(_creators.GetTraitByName(name), _traits);

        private void ShowTalents()
        {
            _audioManager.PlayClick();
            ShowList(_creators.Talents, ShowCreationNewTalent, AddTalent, "Выберите талант");
        }

        private void ShowCreationNewTalent()
        {
            _audioManager.PlayClick();
            CloseAllListAndForms();
            CreationTalentView creationTalent = _lvlFactory.Get(TypeScene.CreationTalent).GetComponent<CreationTalentView>();
            creationTalent.Close += CloseCreationForm;
            creationTalent.ReturnNewTalent += AddNewTalent;
            _tempView = creationTalent;
        }

        private void AddNewTalent(Trait talent)
        {
            _audioManager.PlayDone();
            SaveLoadEnemyTalent saveLoadEnemy = new SaveLoadEnemyTalent();
            saveLoadEnemy.name = talent.Name;
            saveLoadEnemy.description = talent.Description;
            var path = Path.Combine($"{Application.dataPath}/StreamingAssets/EnemyTalent/", saveLoadEnemy.name + ".JSON");
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(saveLoadEnemy));
            File.WriteAllLines(path, data);

            _talents.Add(talent);
            _creators.Talents.Add(talent);
            CloseAllListAndForms();
            UpdateFeaturesView();
        }

        private void AddTalent(string name) => AddTraitToList(_creators.GetTalentByName(name), _talents);

        private void Showpsypowers()
        {
            _audioManager.PlayClick();
            ShowList(_creators.PsyPowers, AddPsypower, "Выберите пси силу");
        }

        private void AddPsypower(string name) => AddTraitToList(_creators.GetPsypowerByName(name), _psyPowers);

        private void AddTraitToList(Trait trait, List<Trait> traits)
        {
            _audioManager.PlayDone();
            traits.Add(new Trait(trait));
            CloseAllListAndForms();
            UpdateFeaturesView();
        }

        private void ShowImplants()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            ShowList(_creators.Implants, ShowImplantForm, AddImplant, "Выберите имплант");
        }

        private void AddImplant(string name)
        {
            _audioManager.PlayDone();
            _implants.Add(new MechImplant(_creators.GetMechImplantByName(name)));
            CloseAllListAndForms();
            UpdateFeaturesView();
        }

        private void ShowImplantForm()
        {
            CloseAllListAndForms();
            _audioManager.PlayClick();
            NewImplant newImplant = _lvlFactory.Get(TypeScene.CreationImplant).GetComponent<NewImplant>();
            newImplant.Cancel += CloseForm;
            newImplant.ReturnImplant += AddNewImplant;
            newImplant.WrongInput += _audioManager.PlayWarning;
            _tempView = newImplant;
            newImplant.Initialize();
        }

        private void AddNewImplant(MechImplant implant)
        {
            _audioManager.PlayDone();
            _implants.Add(implant);
            _creators.Implants.Add(implant);
            UpdateFeaturesView();
            _tempView.DestroyView();

        }

        private void ShowList<T>(List<T> listWithItems, MethodForListCreateNew methodForCreate, MethodForListChooseOne methodForChoose, string nameOfList) where T : IName
        {
            CloseAllListAndForms();

            ListWithNewItemsAndNewButton listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewButton).GetComponent<ListWithNewItemsAndNewButton>();
            listWithNewItems.Close += CloseAllListAndForms;
            listWithNewItems.Close += _audioManager.PlayCancel;
            listWithNewItems.AddNewItem += methodForCreate.Invoke;
            listWithNewItems.ChooseThis += methodForChoose.Invoke;
            listWithNewItems.Initialize(listWithItems, nameOfList);
            _tempView = listWithNewItems;
        }

        private void ShowList<T>(List<T> listWithItems, MethodForListChooseOne methodForChoose, string nameOfList, bool isProperty = false) where T : IName
        {
            if (isProperty == false)
                CloseAllListAndForms();

            ListWithNewItems listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewButton).GetComponent<ListWithNewItems>();
            if (isProperty)
                listWithNewItems.Close += CloseProperty;
            else
                listWithNewItems.Close += CloseAllListAndForms;
            listWithNewItems.Close += _audioManager.PlayCancel;
            listWithNewItems.ChooseThis += methodForChoose.Invoke;
            listWithNewItems.Initialize(listWithItems, nameOfList);

            if (isProperty == false)
                _tempView = listWithNewItems;
            else
                _propertyView = listWithNewItems;
        }

        private void CancelPressed()
        {
            _audioManager.PlayCancel();
            Unscribe();
            _view.DestroyView();
            Close?.Invoke();

        }

        private void CloseProperty()
        {
            _propertyView.DestroyView();
            _propertyView = null;
        }

        private void CloseAllListAndForms()
        {
            if (_tempView != null)
                _tempView.DestroyView();
            _tempView = null;
        }

        private void Done(SaveLoadCharacter save)
        {
            _audioManager.PlayDone();
            Unscribe();
            Character character = new Character(save, _implants, _equipments, _traits, _skills, _talents, _psyPowers);
            new SaveCharacter(character);
            _creators.Characters.Add(character);
            _view.DestroyView();
            Close?.Invoke();
        }

        private void RemoveTrait(string name)
        {
            _audioManager.PlayCancel();
            if (RemoveTraitFromlist(name, _skills)) { }
            else if (RemoveTraitFromlist(name, _talents)) { }
            else if (RemoveTraitFromlist(name, _traits)) { }
            else if (RemoveTraitFromlist(name, _psyPowers)) { }
            else if (RemoveTraitFromlist(name, _equipments)) { }
            else if (RemoveTraitFromlist(name, _implants)) { }

            UpdateFeaturesView();
            UpdateWeapons();
        }

        private void ChangeLvl(string name, int lvl)
        {
            if (ChangeLvlInList(name, lvl, _skills)) { }
            else if (ChangeLvlInList(name, lvl, _traits)) { }
            else
            {
                foreach (Equipment equipment in _equipments)
                {
                    if (string.Compare(name, equipment.Name) == 0)
                        equipment.Amount = lvl;
                }
            }

            UpdateFeaturesView();
        }

        private bool ChangeLvlInList(string name, int lvl, List<Trait> list)
        {
            _audioManager.PlayClick();
            foreach (Trait feature in list)
                if (string.Compare(name, feature.Name) == 0)
                {
                    feature.Lvl = lvl;
                    return true;
                }

            return false;
        }

        private bool RemoveTraitFromlist<T>(string name, List<T> list) where T : IName
        {
            foreach (T item in list)
                if (string.Compare(name, item.Name, true) == 0)
                {
                    list.Remove(item);
                    return true;
                }
            return false;
        }

        private void UpdateFeaturesView()
        {
            List<Trait> features = new List<Trait>();
            features.AddRange(_skills);
            features.AddRange(_traits);
            features.AddRange(_talents);
            features.AddRange(_psyPowers);

            foreach (Equipment equipment in _equipments)
                features.Add(new Trait(equipment.Name, equipment.Amount));

            foreach (MechImplant implant in _implants)
                features.Add(new Trait(implant.Name));

            _view.UpdateListTraits(features);
        }

        private void UpdateWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (Equipment equipment in _equipments)
                if (equipment is Weapon)
                    weapons.Add((Weapon)equipment);

            _view.UpdateGuns(weapons);
        }

        private void CalculateArmors()
        {
            _audioManager.PlayClick();
            int.TryParse(_view.WeaponSkill.text, out int weaponSkill);
            int.TryParse(_view.BallisticSkill.text, out int ballisticSkill);
            int.TryParse(_view.Strength.text, out int strength);
            int.TryParse(_view.Toughness.text, out int toughness);
            int.TryParse(_view.Agility.text, out int agility);
            int.TryParse(_view.Intelligence.text, out int intelligence);
            int.TryParse(_view.Perception.text, out int perception);
            int.TryParse(_view.Willpower.text, out int willpower);
            int.TryParse(_view.Fellowship.text, out int fellowship);

            toughness /= 10;

            SaveLoadCharacter save = new SaveLoadCharacter();

            foreach (Trait trait in _traits)            
                switch (trait.Name)
                {
                    case "Сверхъестественная Навык Рукопашной":
                        save.weaponSkillSuper = trait.Lvl + weaponSkill / 10;
                        break;
                    case "Сверхъестественная Навык Стрельбы":
                        save.ballisticSkillSuper = trait.Lvl + ballisticSkill / 10;
                        break;
                    case "Сверхъестественная Сила":
                        save.strengthSuper = trait.Lvl + strength / 10;
                        break;
                    case "Сверхъестественная Выносливость":
                        save.toughnessSuper += trait.Lvl;
                        break;
                    case "Сверхъестественная Ловкость":
                        save.agilitySuper = trait.Lvl + agility / 10;
                        break;
                    case "Сверхъестественная Интеллект":
                        save.intelligenceSuper = trait.Lvl + intelligence / 10;
                        break;
                    case "Сверхъестественная Восприятие":
                        save.perceptionSuper = trait.Lvl + perception / 10;
                        break;
                    case "Сверхъестественная Сила Воли":
                        save.willpowerSuper = trait.Lvl + willpower / 10;
                        break;
                    case "Сверхъестественная Общительность":
                        save.fellowshipSuper = trait.Lvl + fellowship / 10;
                        break;
                    case "Демонический":
                        save.toughnessSuper += trait.Lvl;
                        break;
                    case "Машина":
                        AddAmountToArmor(save, trait.Lvl);
                        break;
                    case "Природная броня":
                        AddAmountToArmor(save, trait.Lvl);
                        break;
                }          

            if (save.toughnessSuper > 0)
                save.toughnessSuper += toughness;

            foreach (MechImplant implant in _implants)
            {
                if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.Head)
                {
                    save.armorHead += implant.Armor;
                    save.armorTotalHead += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.RightHand)
                {
                    save.armorRightHand += implant.Armor;
                    save.armorTotalRightHand += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.LeftHand)
                {
                    save.armorLeftHand += implant.Armor;
                    save.armorTotalLeftHand += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.Body)
                {
                    save.armorBody += implant.Armor;
                    save.armorTotalBody += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.RightLeg)
                {
                    save.armorRightLeg += implant.Armor;
                    save.armorRightLeg += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.LeftLeg)
                {
                    save.armorLeftLeg += implant.Armor;
                    save.armorLeftLeg += implant.BonusToughness;
                }
            }

            List<Armor> armors = new List<Armor>();
            List<Armor> shields = new List<Armor>();
            foreach (Equipment equipment in _equipments)
                if (equipment.TypeEq == Equipment.TypeEquipment.Armor)
                    armors.Add((Armor)equipment);
                else if(equipment.TypeEq == Equipment.TypeEquipment.Shield)
                    shields.Add((Armor)equipment);

            if(shields.Count >= 1)
            {
                save.armorHead += shields.Max(a => a.DefHead);
                save.armorLeftHand += shields.Max(a => a.DefHands);
                save.armorBody += shields.Max(a => a.DefBody);
                save.armorLeftLeg += shields.Max(a => a.DefLegs);
                save.armorRightLeg += shields.Max(a => a.DefLegs);
            }

            if (armors.Count >= 1)
            {
                save.armorHead += armors.Max(a => a.DefHead);
                save.armorLeftHand += armors.Max(a => a.DefHands);
                save.armorRightHand += armors.Max(a => a.DefHands);
                save.armorBody += armors.Max(a => a.DefBody);
                save.armorLeftLeg += armors.Max(a => a.DefLegs);
                save.armorRightLeg += armors.Max(a => a.DefLegs);
            }

            if(save.toughnessSuper > toughness)
                toughness = save.toughnessSuper;

            save.armorTotalHead += save.armorHead + toughness;
            save.armorTotalRightHand += save.armorRightHand +  toughness;
            save.armorTotalLeftHand += save.armorLeftHand + toughness;
            save.armorTotalBody += save.armorBody + toughness;
            save.armorTotalRightLeg += save.armorRightLeg + toughness;
            save.armorTotalLeftLeg += save.armorLeftLeg + toughness;

            agility /= 10;
            agility += save.agilitySuper;

            save.half = agility;
            save.full = agility * 2;
            save.natisk = agility * 3;
            save.run = agility * 6;

            _view.SetArmors(save);
        }

        private void AddAmountToArmor(SaveLoadCharacter save, int amount)
        {
            save.armorBody += amount;
            save.armorHead += amount;
            save.armorLeftHand += amount;
            save.armorLeftLeg += amount;
            save.armorRightHand += amount;
            save.armorRightLeg += amount;
        }
    }

}
