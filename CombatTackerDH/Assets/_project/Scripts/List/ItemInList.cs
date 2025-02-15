
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace CombarTracker
{
    public class ItemInList : MonoBehaviour, IItemForList
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private Button _button;

        public event Action<string> ChooseThis;
        private string _name;
        public string Name => _name;


        private void OnEnable() => _button.onClick.AddListener(ChooseThisPressed);

        private void OnDisable() => _button.onClick.RemoveAllListeners();

        public virtual void Initialize(string name)
        {
            _name = name;
            textName.text = name;
            gameObject.SetActive(true);
        }

        public virtual void Initialize(string name, string nameWithAmount)
        {
            _name = name;
            textName.text = nameWithAmount;
            gameObject.SetActive(true);
        }

        private void ChooseThisPressed() => ChooseThis?.Invoke(_name);

    }
}

