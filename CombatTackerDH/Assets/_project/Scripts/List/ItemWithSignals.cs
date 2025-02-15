using UnityEngine.UI;
using UnityEngine;
using System;

namespace CombarTracker
{
    public class ItemWithSignals : ItemInList
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _redSignal, _greenSignal;
        [SerializeField] private Button _additionalButton;

        public event Action<string> AdditionalButtonAction;

        public void SetGreenStatus() => _image.sprite = _greenSignal;

        public void SetRedStatus() => _image.sprite = _redSignal;

        public void Initialize(string name, bool isTurned)
        {
            base.Initialize(name);
            if (isTurned)
                SetRedStatus();
            else
                SetGreenStatus();

            _additionalButton.onClick.AddListener(AdditionalButtonPressed);
        }

        private void AdditionalButtonPressed() => AdditionalButtonAction?.Invoke(Name);

    }
}

