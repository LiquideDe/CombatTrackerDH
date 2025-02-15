using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace CombarTracker
{
    public class NPCView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textPhysical, _textBehavior, _textNature, _textSecrets;
        [SerializeField] private Button _buttonGenerate, _buttonClose;
        private NPC _npc = new NPC();
        private AudioManager _audioManager;

        [Inject] private void Construct(AudioManager audioManager) => _audioManager = audioManager;

        private void OnEnable()
        {
            _buttonGenerate.onClick.AddListener(GenerateTexts);
            _buttonClose.onClick.AddListener(CloseNPC);
        }

        private void OnDisable()
        {
            _buttonGenerate.onClick.RemoveAllListeners();
            _buttonClose.onClick.RemoveAllListeners();
        }

        private void CloseNPC()
        {
            _audioManager.PlayCancel();
            Destroy(gameObject);
        }

        private void GenerateTexts()
        {
            _audioManager.PlayClick();
            _textPhysical.text = _npc.GetPhysicalTrait();
            _textBehavior.text = _npc.GetBehaivor();
            _textNature.text = _npc.GetNature();
            _textSecrets.text = _npc.GetSecret();
        }
    }
}

