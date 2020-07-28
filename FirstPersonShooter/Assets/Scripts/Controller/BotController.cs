using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FirstPersonShooter
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        private readonly int _countBot = 5;
        private readonly List<Bot> _botList = new List<Bot>();

        public void Initialization()
        {
            for (int index = 0; index < _countBot; index++)
            {
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericRandomGenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>()
                        .transform),
                    Quaternion.identity);
                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                AddBotToList(tempBot);
            }
        }
        
        private void AddBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChanged += RemoveBotToList;
            }
        }
        
        private void RemoveBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                return;
            }

            bot.OnDieChanged -= RemoveBotToList;
            _botList.Remove(bot);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            foreach (var bot in _botList)
            {
                bot.Execute();
            }
        }
    }
}