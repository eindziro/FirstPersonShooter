namespace FirstPersonShooter
{
    /// <summary>
    /// Бронебойное оружие
    /// </summary>
    public sealed class ArmorPiercingGun: Weapon
    {
        protected override void Awake()
        {
            base.Awake();
            _countClip = 4;
            _maxCountAmmunition = 10;
            _minCountAmmunition = 5;
        }
         
        public override void Fire()
        {
            if (_isReady) 
                return;
            if (Clip.CountAmmunition <= 0) 
                return;
            var  obj = CacheObjectRepo.Instance.SpawnCachedObject(Ammunition.GetEnumMemberName(),
                _barrel.position, _barrel.rotation,PoolingStratagy.EnqueueManuallyLater);
            //CR: Роман, посмотри пж, можно ли так делать?
            var tmpAmmunition = GetComponent<Ammunition>();
            tmpAmmunition.AddForce(_barrel.forward*_force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}