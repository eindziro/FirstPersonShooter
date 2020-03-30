namespace FirstPersonShooter
{
    /// <summary>
    /// Автоматическое оружие
    /// </summary>
    public sealed class AutomaticGun : Weapon
    {
        public override void Fire()
        {
            if (_isReady) 
                return;
            if (Clip.CountAmmunition <= 0) 
                return;
            var tmpAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            tmpAmmunition.AddForce(_barrel.forward*_force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}