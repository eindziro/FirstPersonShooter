using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class InspectionState:State
    {
        private ITimeRemaining _timeRemaining;
        [SerializeField]private const float _inspectTime=4f;
        
        public InspectionState(Bot bot, BotStateMachine stateMachine) : base(bot, stateMachine)
        {
            _timeRemaining=new TimeRemaining(()=>_stateMachine.ChangeState(_bot.PatrolState),_inspectTime);
        }

        public override void Enter()
        {
            base.Enter();
            _bot.Color = Color.yellow;
            _bot.WasAttacked += ReactEnemiesWasDetected;
            _timeRemaining.AddTimeRemaining();
            _bot.OnDieChanged += OnDie;
            
        }

        public override void Exit()
        {
            base.Exit();
            _bot.WasAttacked -= ReactEnemiesWasDetected;
            _bot.OnDieChanged -= OnDie;
        }
        
        private void ReactEnemiesWasDetected()
        {
            _stateMachine.ChangeState(_bot.DetectEnemiesState);
            _timeRemaining.RemoveTimeRemaining();
        }
        
        private void OnDie(Bot bot)
        {
            _stateMachine.ChangeState(_bot.DeadState);
        }
    }
}