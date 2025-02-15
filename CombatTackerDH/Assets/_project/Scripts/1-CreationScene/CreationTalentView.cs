using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace CombarTracker
{
    public class CreationTalentView : CanDestroyView
    {
        [SerializeField] Button _buttonDone, _buttonClose;
        [SerializeField] TMP_InputField _inputName, _inputDescription;

        public event Action<Trait> ReturnNewTalent;

        private void OnEnable()
        {
            _buttonClose.onClick.AddListener(ClosePressed);
            _buttonDone.onClick.AddListener(DonePressed);
        }

        private void OnDisable()
        {
            _buttonClose.onClick.RemoveAllListeners();
            _buttonDone.onClick.RemoveAllListeners();
        }

        private void DonePressed()
        {
            Trait talent = new Trait(_inputName.text, _inputDescription.text);
            ReturnNewTalent?.Invoke(talent);
        }
    }
}

