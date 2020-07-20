using System;
using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Базовая модель снаряда
    /// </summary>
    public abstract class Ammunition : BaseObject
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining[] _timeRemainings;

        public AmmunitionType Type = AmmunitionType.Bullet;

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            _timeRemainings = new TimeRemaining[2];
            Destroy(gameObject, _timeToDestruct);
            _timeRemainings[0] = new TimeRemaining(LossOfDamage, 1.0f, true);
            //CR: можно передававть метод, в котором присутствует отписка?
            _timeRemainings[1] = new TimeRemaining(ReturnAmmoToPool, _timeToDestruct, false);
        }

        /// <summary>
        /// Придать начальную силу снаряду
        /// </summary>
        /// <param name="dir">Вектор силы</param>
        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody)
                return;
            Rigidbody.AddForce(dir);
        }

        /// <summary>
        /// Потеря урона снаряда со временем
        /// </summary>
        private void LossOfDamage()
            => _curDamage -= _lossOfDamageAtTime;

        protected void ReturnAmmoToPool()
        {
            tag = GetEnumMemberName();
            CacheObjectRepo.Instance.ReturnObjToPool(gameObject, tag);
            foreach (ITimeRemaining timeRemaining in _timeRemainings)
            {
                timeRemaining.RemoveTimeRemaining();
            }
        }

        public string GetEnumMemberName()
            => Enum.GetName(typeof(AmmunitionType), Type).ToLower();
    }
}