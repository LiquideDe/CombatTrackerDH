using System;
using UnityEngine;

namespace CombarTracker
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Holder/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [SerializeField] private GameObject _mainScene;
        [SerializeField] private GameObject _listUniversal, _listWithNewButton;
        [SerializeField] private GameObject _creationThing, _creationMelee, _creationGrenade, _creationRange, _creationArmor, _creationImplant;
        [SerializeField] private GameObject _creationCharacter, _textInfo, _damagePanel, _creationTalent;
        [SerializeField] private GameObject _npcNature, _loading;

        public GameObject Get(TypeScene typeScene)
        {
            switch (typeScene)
            {
                case TypeScene.ListUniversal:
                    return _listUniversal;

                case TypeScene.MainScene:
                    return _mainScene;

                case TypeScene.ListWithNewButton:
                    return _listWithNewButton;

                case TypeScene.CreationArmor:
                    return _creationArmor;

                case TypeScene.CreationRange:
                    return _creationRange;

                case TypeScene.CreationGrenade:
                    return _creationGrenade;

                case TypeScene.CreationImplant:
                    return _creationImplant;

                case TypeScene.CreationThing:
                    return _creationThing;

                case TypeScene.CreationMelee:
                    return _creationMelee;

                case TypeScene.CreatorCharacter:
                    return _creationCharacter;

                case TypeScene.TextInfo:
                    return _textInfo;

                case TypeScene.DamagePanel:
                    return _damagePanel;

                case TypeScene.CreationTalent:
                    return _creationTalent;

                case TypeScene.NPCNature:
                    return _npcNature;

                case TypeScene.Loading:
                    return _loading;

                default:
                    throw new ArgumentException(nameof(TypeScene));
            }
        }
    }
}


