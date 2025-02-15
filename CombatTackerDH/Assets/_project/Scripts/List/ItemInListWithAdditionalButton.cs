using UnityEngine;
using UnityEngine.UI;
using System;

namespace CombarTracker
{
    public class ItemInListWithAdditionalButton : ItemInList
    {
        [SerializeField] private Button _additionalButton;

        public event Action<string> AdditionalButtonAction;

        private void OnEnable() => _additionalButton.onClick.AddListener(AdditionalButtonPressed);


        private void OnDisable() => _additionalButton.onClick.RemoveAllListeners();

        private void AdditionalButtonPressed() => AdditionalButtonAction?.Invoke(Name);
    }
}

