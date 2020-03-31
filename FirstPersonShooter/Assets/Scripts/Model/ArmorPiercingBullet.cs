using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Бронебойный снаряд
    /// <remarks>Пробивает насквозь и продолжает полет, но уменьшаается урон</remarks>
    /// </summary>
    public sealed class ArmorPiercingBullet : Ammunition
    {
        private float _additionalDamage = 5.0f;
        private float _lossDamageAfterCollision = 2.0f;

        protected override void Awake()
        {
            base.Awake();
            Type = AmmunitionType.ArmorPiercingBullet;
            _curDamage += _additionalDamage;
        }

        private void OnCollisionEnter(Collision collision) // todo своя обработка полета и получения урона
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage,Type, Rigidbody.velocity));
            }

            LossDamageAfterCollision();
        }

        private void LossDamageAfterCollision()
        {
            _curDamage -= _lossDamageAfterCollision;
        }
    }
}