using UnityEngine;
using UnityEngine.UI;

namespace FirstPersonShooter
{
    /// <summary>
    /// Представление бара знергии источника света
    /// </summary>
    public sealed class FlashLightUiBar:MonoBehaviour
    {
        private Image _bar;

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        /// <summary>
        /// Заполнение объекта
        /// </summary>
        public float Fill
        {
            set => _bar.fillAmount = value;
        }

        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value"></param>
        public void SetActive(bool value)
        {
            _bar.gameObject.SetActive(value);
        }

        /// <summary>
        /// Установить цвет
        /// </summary>
        public void SetColor(Color color)
        {
            _bar.color = color;
        }
    }
}