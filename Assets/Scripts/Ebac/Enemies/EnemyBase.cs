using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.Animation;

namespace Ebac.Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider hitBox;
        public FlashColor flashColor;
        public ParticleSystem particles;
        public bool lookAtPlayer = false;
        public float StartLife = 10f;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimatioEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Animation")]
        [SerializeField] private float _currentLife;
        [SerializeField] private AnimationBase _animationBase;

        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation)
                BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = StartLife;
        }

        protected virtual void Kill() 
        {
            OnKill();
            _animationBase.PlayerAnimationByTrigger(AnimationType.DEATH);
        }
        protected virtual void OnKill() 
        {
            if (hitBox != null) hitBox.enabled = false;
            Destroy(gameObject, 3f);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particles != null) particles.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= f;

            if (_currentLife <= 0)
                Kill();
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimatioEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayerAnimationByTrigger(animationType);
        }

        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
                transform.LookAt(_player.transform.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)
            {
                p.Damage(1);
            }
        }
    }
}
