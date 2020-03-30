using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Контроллер источника света
    /// </summary>
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        private FlashLightModel _flashLightModel;
        private FlashLightUi _flashLightUi;

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUi>();
        }

        /// <summary>
        /// Включить источник света (если есть заряд)
        /// </summary>
        public override void On()
        {
            if (IsActive)
                return;
            if (_flashLightModel.BatteryChargeCurrent <= 0)
                return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);
            _flashLightUi.SetActive(true);
        }

        /// <summary>
        /// Отключить источник света
        /// </summary>
        public override void Off()
        {
            if (!IsActive)
                return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            _flashLightUi.SetActive(false);
        }

        public void Charge()
        {
            _flashLightModel.Switch(FlashLightActiveType.Charging);
        }

        /// <summary>
        /// "Квант" действия источника света (каждый кадр)
        /// </summary>
        public void Execute()
        {
            if (!IsActive)
                return;

            switch (_flashLightModel.State)
            {
                case FlashLightActiveType.Charging:
                    if (!_flashLightModel.ChargeBattery())
                        Off();
                    break;
                case FlashLightActiveType.On:
                    _flashLightModel.Rotation();
                    if (_flashLightModel.EditBatteryCharge())
                        _flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
                    else
                        Charge();
                    break;
            }
        }
    }
}