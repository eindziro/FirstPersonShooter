using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Класс инвентаря
    /// </summary>
    public sealed class Inventory : IInitialization
    {
        private List<Weapon> _weapons = new List<Weapon>(5);
        private int _currentActiveWeapon;

        public FlashLightModel FlashLight { get; private set; }

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>()
                .GetComponentsInChildren<Weapon>().ToList();
            foreach (Weapon weapon in _weapons)
            {
                weapon.IsVisible = false;
            }

            _currentActiveWeapon = -1;
            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }

        /// <summary>
        /// Активировать указанное оружие
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Weapon ActivateWeapon(int index)
        {
            if (_weapons.Count < index)
                return null;
            _currentActiveWeapon = index;
            return _weapons[index];
        }

        public Weapon ActivateWeapon(bool isNext)
        {
            if (isNext)
            {
                if (_weapons.Count < ++_currentActiveWeapon)
                {
                    _currentActiveWeapon = 0;
                    return _weapons[0];
                }
            }
            else
            {
                if (--_currentActiveWeapon <= 0)
                {
                    _currentActiveWeapon = _weapons.Count;
                    return _weapons.LastOrDefault();
                }
            }

            return _weapons[_currentActiveWeapon];
        }

        /// <summary>
        /// Выкинуть оружие
        /// </summary>
        /// <param name="weapon"></param>
        public void RemoveWeapon(Weapon weapon)
        {
            //CR: не знаю как сделать по нормальному
            var todelete = ServiceLocatorMonoBehaviour.GetService<CharacterController>()
                .GetComponentsInChildren<Weapon>().FirstOrDefault(x=>x.Name ==weapon.Name).transform.parent = null;
        }
    }
}