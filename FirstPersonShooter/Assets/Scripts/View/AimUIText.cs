using System;
using UnityEngine;
using UnityEngine.UI;

namespace FirstPersonShooter
{
    public class AimUIText:MonoBehaviour
    {
        private Aim[] _aims;
        private Text _text;
        private int _countPoint;

        private void Awake()
        {
            _aims = FindObjectsOfType<Aim>();
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (Aim aim in _aims)
            {
                aim.OnPointChanged += UpdatePoint;
            }
        }

        private void OnDisable()
        {
            foreach (Aim aim in _aims)
            {
                aim.OnPointChanged -= UpdatePoint;
            }
        }

        private void UpdatePoint()
        {
            var pointTxt = "очков";
            ++_countPoint;
            if (_countPoint >= 5) pointTxt = "очков";
            else if (_countPoint == 1) pointTxt = "очко";
            else if (_countPoint < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_countPoint} {pointTxt}";
            //todo отписаться удалить и списка
        }
    }
}