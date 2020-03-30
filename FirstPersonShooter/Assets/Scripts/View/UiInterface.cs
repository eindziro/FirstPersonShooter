using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class UiInterface
    {
        private FlashLightUiText _flashLightUiText;

        /// <summary>
        /// Текстовое представление запаса энергии источника света
        /// </summary>
        public FlashLightUiText FlashLightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                return _flashLightUiText;
            }
        }

        private FlashLightUiBar _flashLightUiBar;

        /// <summary>
        /// Представление бара запаса энергии источника света
        /// </summary>
        public FlashLightUiBar FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                return _flashLightUiBar;
            }
        }

        private WeaponUiText _weaponUiText;

        /// <summary>
        /// Текствое представление боезапаса
        /// </summary>
        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                return _weaponUiText;
            }
        }

        private SelectionObjMessageUi _selectionObjMessageUi;

        /// <summary>
        /// Представление текстовой информации о выделенном объекте
        /// </summary>
        public SelectionObjMessageUi SelectionObjMessageUi
        {
            get
            {
                if (!_selectionObjMessageUi)
                    _selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
                return _selectionObjMessageUi;
            }
        }    
    }
}