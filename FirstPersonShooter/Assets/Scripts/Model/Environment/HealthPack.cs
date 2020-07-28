using System;
using UnityEngine;

namespace FirstPersonShooter.Environment
{
    public class HealthPack:MonoBehaviour
    {
        [SerializeField] private float _heal;
        private void OnCollisionEnter(Collision other)
        {
            var setHeal = other.gameObject.GetComponent<IHeal>();
            if (setHeal != null)
            {
                setHeal.SetHeal(_heal);
            }
        }
    }
}