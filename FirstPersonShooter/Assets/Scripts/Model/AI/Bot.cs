using System;
using UnityEngine;
using UnityEngine.AI;

namespace FirstPersonShooter
{
    public sealed class Bot : BaseObject, IExecute
    {
        public float Hp = 100;
        public Vision Vision;
        public Weapon Weapon;
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }

        private float _waitTime = 4;
        private BotState _botState;
        private Vector3 _point;
        private float _stoppingDistance = 2;
        private ITimeRemaining _timeRemaining;

        private BotState BotState
        {
            get => _botState;
            set
            {
                _botState = value;
                switch (value)
                {
                    case BotState.None:
                        Color = Color.white;
                        break;
                    case BotState.Patrol:
                        Color = Color.green;
                        break;
                    case BotState.Inspection:
                        Color = Color.yellow;
                        break;
                    case BotState.DetectEnemies:
                        Color = Color.red;
                        break;
                    case BotState.Dead:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        public event Action<Bot> OnDieChanged;

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            _timeRemaining = new TimeRemaining(ResetBotState, _waitTime);
        }

        private void OnEnable()
        {
            var bodyBot = GetComponent<BodyBot>();
            if (bodyBot != null)
                bodyBot.OnApplyDamageChange += SetDamage;

            var botHead = GetComponent<BotHead>();
            if (botHead != null)
                botHead.OnApplyDamageChanged += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponent<BodyBot>();
            if (bodyBot != null)
                bodyBot.OnApplyDamageChange -= SetDamage;

            var botHead = GetComponent<BotHead>();
            if (botHead != null)
                botHead.OnApplyDamageChanged -= SetDamage;
        }

        public void Execute()
        {
            if (BotState == BotState.Dead)
                return;

            if (BotState != BotState.DetectEnemies)
            {
                if (!Agent.hasPath)
                {
                    if (BotState != BotState.Inspection)
                    {
                        if (BotState != BotState.Patrol)
                        {
                            BotState = BotState.Patrol;
                            _point = Patrol.GenericRandomGenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = 0;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                BotState = BotState.Inspection;
                                _timeRemaining.AddTimeRemaining();
                            }
                        }
                    }
                }

                if (Vision.Detect(transform, Target))
                {
                    BotState = BotState.DetectEnemies;
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    Agent.stoppingDistance = _stoppingDistance;
                }

                if (Vision.Detect(transform, Target))
                {
                    Weapon.Fire();
                }
                else
                {
                    MovePoint(Target.position);
                }
                
                //todo: как потерять врага из виду? 
            }
        }

        private void ResetBotState()
        {
            BotState = BotState.None;
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(_point);
        }

        private void SetDamage(InfoCollision collision)
        {
            if (Hp > 0)
                Hp -= collision.Damage;
            if (Hp <= 0)
            {
                BotState = BotState.Dead;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tmpRgbd = child.GetComponent<Rigidbody>();
                    if (!tmpRgbd)
                    {
                        tmpRgbd = child.gameObject.AddComponent<Rigidbody>();
                    }

                    Destroy(child.gameObject, 5);

                    OnDieChanged?.Invoke(this);
                }
            }
        }
    }
}