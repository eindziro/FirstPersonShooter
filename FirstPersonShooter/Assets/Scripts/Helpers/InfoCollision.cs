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

        /// <summary>
        /// Информация о попадании снаряда
        /// </summary>
        /// <param name="damage">Полученный урон</param>
        /// <param name="dir">Вектор попадания</param>
        public InfoCollision(float damage, Vector3 dir = default)
        {
            _damage = damage;
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

    }
}