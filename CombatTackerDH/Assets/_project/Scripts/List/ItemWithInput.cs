using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace CombarTracker
{
    public class ItemWithInput : ItemInList
    {
        [SerializeField] TMP_InputField _inputLvl;
        public event Action<string, int> ChangeLvl;
        private string _nameItem;

        private void OnEnable() => _inputLvl.onDeselect.AddListener(ChangeLvlPressed);


        private void OnDisable() => _inputLvl.onDeselect.RemoveAllListeners();

        public void Initialize(string name, int lvl)
        {
            _nameItem = name;
            _inputLvl.text = lvl.ToString();
            base.Initialize(name);
        }

        private void ChangeLvlPressed(string text)
        {
            int.TryParse(_inputLvl.text, out int lvl);
            ChangeLvl?.Invoke(_nameItem, lvl);
        }
    }
}

