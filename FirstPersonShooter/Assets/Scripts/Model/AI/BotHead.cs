using System;
using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class BotHead : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChanged;

        public void CollisionEnter(InfoCollision info)
        {
            OnApplyDamageChanged?.Invoke(new InfoCollision(info.Damage * Consts.DamageSettings.CriticalDamageScale,
                info.Type, info.ObjCollision,info.Contact, info.Dir));
        }
    }
}