namespace FirstPersonShooter
{
    public abstract  class State
    {
        //todo: Роман, попытался реализовать паттерн "Состояние". Оцените пожалуйста. Что надо поменять?
        protected BotStateMachine _stateMachine;
        protected Bot _bot;

        protected State(Bot bot, BotStateMachine stateMachine)
        {
            _bot = bot;
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            
        }
        
        public virtual void HandleInput()
        {
            
        }
        
        public virtual void LogicUpdate()
        {

        }
        
        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }

    }
}