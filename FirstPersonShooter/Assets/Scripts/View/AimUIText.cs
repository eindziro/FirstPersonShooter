using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FirstPersonShooter
{
    public class AimUIText : MonoBehaviour
    {
        private List<Aim> _aims;
        private Text _text;
        private int _countPoint;

        private void Awake()
        {
            _aims = FindObjectsOfType<Aim>().ToList();
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

        private void UpdatePoint(object o, EventArgs args)
        {
            var pointTxt = "очков";
            ++_countPoint;
            if (_countPoint >= 5) pointTxt = "очков";
            else if (_countPoint == 1) pointTxt = "очко";
            else if (_countPoint < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_countPoint} {pointTxt}";
            if (o is Aim aim)
            {
                aim.OnPointChanged -= UpdatePoint;
                _aims.Remove(aim);
            }
        }
    }
}