using System;
using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class BodyBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange;

        public void CollisionEnter(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }
    }
}