using UnityEngine;
using System.Collections.Generic;

namespace FirstPersonShooter
{
    /// <summary>
    /// Базоывый класс оружия
    /// </summary>
    public abstract class Weapon : BaseObject
    {
        protected int _countClip = 5;
        protected int _maxCountAmmunition = 40;
        protected int _minCountAmmunition = 20;
        private Queue<Clip> _clips = new Queue<Clip>();


        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _rechargeTime = 0.2f;

        public Clip Clip;
        public int CountClip => _clips.Count;
        public Ammunition Ammunition;
        public AmmunitionType[] AmmunitionTypes = {AmmunitionType.Bullet};

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            for (int i = 0; i < _countClip; i++)
            {
                AddClip(new Clip
                {
                    CountAmmunition = Random.Range(_minCountAmmunition,
                        _maxCountAmmunition)
                });
            }

            ReloadClip();
        }

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        /// <summary>
        /// Перезарядка (если есть боезапас)
        /// </summary>
        public void ReloadClip()
        {
            if (CountClip <= 0)
                return;
            Clip = _clips.Dequeue();
        }

        /// <summary>
        /// Стрелять
        /// </summary>
        public abstract void Fire();
    }
}