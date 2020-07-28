using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class PatrolState:State
    {
        public PatrolState(Bot bot, BotStateMachine stateMachine) : base(bot, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bot.Color = Color.green;
            _bot.WasAttacked += ReactEnemiesWasDetected;
            _bot.OnDieChanged += OnDie;
        }

        public override void Exit()
        {
            base.Exit();
            _bot.WasAttacked -= ReactEnemiesWasDetected;
            _bot.OnDieChanged -= OnDie;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!_bot.Agent.hasPath)
            {
                _bot.Point = Patrol.GenericRandomGenericPoint(_bot.transform);
                _bot.MovePoint(_bot.Point);
                _bot.Agent.stoppingDistance = 0;
                if ((_bot.Point-_bot.transform.position).sqrMagnitude<=1)
                {
                    _stateMachine.ChangeState(_bot.InspectionState);
                }
            }
            
            if (_bot.Vision.Detect(_bot.transform, _bot.Target))
                ReactEnemiesWasDetected();
            
        }
        
        private void ReactEnemiesWasDetected()
        {
            _stateMachine.ChangeState(_bot.DetectEnemiesState);
        }

        private void OnDie(Bot bot)
        {
            _stateMachine.ChangeState(_bot.DeadState);
        }

    }
}