using UnityEngine;

namespace FirstPersonShooter
{
     public sealed class DeadState:State
    {
        public DeadState(Bot bot, BotStateMachine stateMachine) : base(bot, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bot.Color = Color.gray;
        }
    }
}