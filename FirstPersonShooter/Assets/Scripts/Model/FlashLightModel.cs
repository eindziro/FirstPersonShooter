using System;
using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Модель источника света
    /// </summary>
    public sealed class FlashLightModel : BaseObject
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _batteryChargeMax;
        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;

        public FlashLightActiveType State { get; private set; }
        public float BatteryChargeCurrent { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vecOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        public void Switch(FlashLightActiveType flashState)
        {
            switch (flashState)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vecOffset;
                    Transform.rotation = _goFollow.rotation;
                    State = FlashLightActiveType.On;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    State = FlashLightActiveType.Off;
                    break;
                case FlashLightActiveType.Charging:
                    _light.enabled = false;
                    State = FlashLightActiveType.Charging;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(flashState), flashState, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vecOffset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _goFollow.rotation,
                _speed * Time.deltaTime);
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Зарядить источник света
        /// </summary>
        public bool ChargeBattery()
        {
            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += Time.deltaTime;
                return true;
            }
            return false;
        }
    }
}