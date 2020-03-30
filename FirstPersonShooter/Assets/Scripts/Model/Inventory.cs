using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Класс инвентаря
    /// </summary>
    public sealed class Inventory : IInitialization
    {
        private Weapon[] _weapons = new Weapon[5];

        /// <summary>
        /// Оружия в инвентаре
        /// </summary>
        public Weapon[] Weapons => _weapons;

        public FlashLightModel FlashLight { get; private set; }

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>()
                .GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in _weapons)
            {
                weapon.IsVisible = false;
            }

            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }
        
        //todo Добавить функционал

        public void RemoveWeapon(Weapon weapon)
        {
            
        }
    }
}