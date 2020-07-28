using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FirstPersonShooter
{
    public sealed class Bot : BaseObject, IExecute,IHeal
    {
        #region Fields & Properties

        [SerializeField] private float MaxHp = 100;
        public float Hp;
        public Vision Vision;
        public Weapon Weapon;
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; set; }

        private float _waitTime = 4;
        private BotState _botState;
        public float StoppingDistance = 2;
        private ITimeRemaining _timeRemaining;
        [HideInInspector] public Vector3 Point;

        #region State

        private BotStateMachine _stateMachine;

        public PatrolState PatrolState;
        public DetectEnemiesState DetectEnemiesState;
        public InspectionState InspectionState;
        public DeadState DeadState;

        #endregion

        #endregion

        #region Events

        //todo: Роман, реализовал реакция на попадание таким образом. Нормально?
        public event Action WasAttacked = delegate { };
        public event Action<Bot> OnDieChanged;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            Hp = MaxHp;
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

        private void Start()
        {
            _stateMachine = new BotStateMachine();
            PatrolState = new PatrolState(this, _stateMachine);
            DeadState = new DeadState(this, _stateMachine);
            InspectionState = new InspectionState(this, _stateMachine);
            DetectEnemiesState = new DetectEnemiesState(this, _stateMachine);

            _stateMachine.Initialize(PatrolState);
        }

        public void Execute()
        {
            _stateMachine.CurrentState.HandleInput();
            _stateMachine.CurrentState.LogicUpdate();
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(Point);
        }

        private void SetDamage(InfoCollision collision)
        {
            if (Hp > 0)
            {
                Hp -= collision.Damage;
                WasAttacked?.Invoke();
            }
            
            if (Hp <= 0)
            {
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }

                    tempRbChild.AddForce(collision.Dir * Random.Range(10, 300));

                    Destroy(child.gameObject, 10);
                    OnDieChanged?.Invoke(this);
                }
            }
        }


        public void SetHeal(float heal)
        {
            if ((Hp + heal) > MaxHp)
                Hp = MaxHp;
            else
                Hp += heal;
        }
    }
}