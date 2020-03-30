using UnityEngine;
using UnityEngine.UI;
namespace FirstPersonShooter
{
    /// <summary>
    /// Представление для выделенных объектов
    /// </summary>
    public sealed class SelectionObjMessageUi : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public string Text
        {
            set => _text.text = $"{value}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}