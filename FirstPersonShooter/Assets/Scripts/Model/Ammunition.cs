using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Базовая модель снаряда
    /// </summary>
    public abstract class Ammunition:BaseObject
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;
        private TimeRemaining _timeRemaining;

        public AmmunitionType Type = AmmunitionType.Bullet;

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            Destroy(gameObject,_timeToDestruct);
            _timeRemaining = new TimeRemaining(LossOfDamage,1.0f,true);
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
        
    }
}