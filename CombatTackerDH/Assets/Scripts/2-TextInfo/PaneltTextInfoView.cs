using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PaneltTextInfoView : CanDestroyView
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _buttonClose;

    private void OnEnable() => _buttonClose.onClick.AddListener(ClosePressed);

    private void OnDisable() => _buttonClose.onClick.RemoveAllListeners();

    public void Initialize(string text) => _text.text = text;    

}
