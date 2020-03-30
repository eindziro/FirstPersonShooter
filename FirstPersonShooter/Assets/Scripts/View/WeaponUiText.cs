using System;
using UnityEngine;
using UnityEngine.UI;

namespace FirstPersonShooter
{
    /// <summary>
    /// Представление данных о текущем оружии
    /// </summary>
    public sealed class WeaponUiText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        /// <summary>
        /// Задать данные представления
        /// </summary>
        /// <param name="coutAmmunition">Кол-во снарядов в тек обойме</param>
        /// <param name="countClip">Кол-во запасных обойм</param>
        public void ShowData(int coutAmmunition, int countClip)
        {
            _text.text = $"{coutAmmunition/countClip}";
        }

        /// <summary>
        /// Вкл/Выкл отображение
        /// </summary>
        /// <param name="value"></param>
        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}