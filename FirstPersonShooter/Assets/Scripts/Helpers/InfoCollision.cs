using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Информация о попадании снаряда
    /// </summary>
    public readonly struct InfoCollision
    {
        private readonly Vector3 _dir;
        private readonly float _damage;
        private readonly AmmunitionType _type;
        private readonly Transform _objCollision;
        private readonly ContactPoint _contact;

        /// <summary>
        /// Информация о попадании снаряда
        /// </summary>
        /// <param name="damage">Полученный урон</param>
        /// <param name="dir">Вектор попадания</param>
        public InfoCollision(float damage, AmmunitionType type, Transform objCollision, ContactPoint contact,
            Vector3 dir = default)
        {
            _damage = damage;
            _type = type;
            _objCollision = objCollision;
            _contact = contact;
            _dir = dir;
        }

        /// <summary>
        /// Вектор попадания
        /// </summary>
        public Vector3 Dir => _dir;

        /// <summary>
        /// Полученный урон
        /// </summary>
        public float Damage => _damage;

        /// <summary>
        /// Тип попавшего урона
        /// </summary>
        public AmmunitionType Type => _type;

        public Transform ObjCollision => _objCollision;

        public ContactPoint Contact => _contact;
    }
}