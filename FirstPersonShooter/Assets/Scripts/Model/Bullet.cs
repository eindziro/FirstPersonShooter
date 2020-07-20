using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class Bullet : Ammunition
    {
        private void OnCollisionEnter(Collision collision) // todo своя обработка полета и получения урона
        {
            // дописать доп урон
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage,Type, Rigidbody.velocity));
            }

            ReturnAmmoToPool();
        }
    }
}