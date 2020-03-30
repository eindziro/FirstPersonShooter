namespace FirstPersonShooter
{
    public abstract class BaseController
    {

        protected UiInterface UiInterface;

        protected BaseController()
        {
            UiInterface = new UiInterface();
        }
        /// <summary>
        /// Активность контроллера
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Влючить объект контроллер
        /// </summary>
        public virtual void On()
        {
            On(null);
        }

        /// <summary>
        /// Влючить объект контроллер
        /// </summary>
        public virtual void On(params BaseObject[] objs)
        {
            IsActive = true;
        }

        /// <summary>
        /// Отключить объект контроллер
        /// </summary>
        public virtual void Off()
        {
            IsActive = false;
        }
        
        /// <summary>
        /// Изменить состояние контроллера
        /// </summary>
        public void Switch(params BaseObject[] obj)
        {
            if (!IsActive)
                On(obj);
            else
                Off();
        }
        
        
        
    }
}