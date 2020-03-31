namespace FirstPersonShooter
{
    public class ArmoredEnemy:Enemy
    {
        private float _reduceDamage = 2.0f;
        public override void CollisionEnter(InfoCollision info)
        {
            base.CollisionEnter(info);
            if (info.Type != AmmunitionType.ArmorPiercingBullet)
                Hp += _reduceDamage;
            
            _isDead = true;


        }
    }
}