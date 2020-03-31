using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Базовая модель противников
    /// </summary>
    public abstract class Enemy : BaseObject, ICollision
    {
        protected bool _isDead;
        [SerializeField] protected float Hp = 100.0f;

        public EnemyType Type;

        public virtual void CollisionEnter(InfoCollision info)
        {
            if (_isDead)
                return;
            if (Hp > 0)
                Hp -= info.Damage;
        }
    }
}