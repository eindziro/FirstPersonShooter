using System;
using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class Aim : MonoBehaviour, ICollision, ISelectObj
    {
        private bool _isDead;
        private float _timeToDestroy = 10.0f;

        public float Hp = 100;
        public event EventHandler OnPointChanged = delegate { };
        
        public void CollisionEnter(InfoCollision info)
        {
            if(_isDead)
                return;
            if (Hp > 0)
                Hp -= info.Damage;
            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject,_timeToDestroy);
                OnPointChanged.Invoke(this,null);
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return gameObject.name;
        }
    }
}