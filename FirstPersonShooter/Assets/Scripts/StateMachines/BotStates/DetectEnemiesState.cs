using System;
using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class DetectEnemiesState:State
    {
        private ITimeRemaining _timeRemaining;
        private const float _lostTargetTime = 4f;
        
        public DetectEnemiesState(Bot bot, BotStateMachine stateMachine) : base(bot, stateMachine)
        {
            _timeRemaining = new TimeRemaining(LostTarget,_lostTargetTime);
        }

        public override void Enter()
        {
            base.Enter();
            _bot.Color = Color.red;
            _bot.OnDieChanged += OnDie;
        }

        public override void Exit()
        {
            base.Exit();
            _bot.OnDieChanged -= OnDie;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //бот стреляет лишь догнав
            //бот либо догоняет, либо стреляет
            if (Math.Abs(_bot.Agent.stoppingDistance - _bot.StoppingDistance) > Mathf.Epsilon)
            {
                _bot.Agent.stoppingDistance = _bot.StoppingDistance;
            }
            if (_bot.Vision.Detect(_bot.transform, _bot.Target))
            {
                //todo: Роман, реализовал так потерю персонажа, не очень ли затратно?
                _timeRemaining.RemoveTimeRemaining();
                _bot.Weapon.Fire();
            }
            else
            {
                _timeRemaining.AddTimeRemaining();
                _bot.MovePoint(_bot.Target.position);
            }
        }
        private void OnDie(Bot bot)
        {
            _stateMachine.ChangeState(_bot.DeadState);
        }

        private void LostTarget()
        {
            _stateMachine.ChangeState(_bot.PatrolState);
        }
    }
}