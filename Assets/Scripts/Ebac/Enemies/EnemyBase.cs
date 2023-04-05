using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Ebac.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float StartLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimatioEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
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
        }
        protected virtual void OnKill() 
        {
            Destroy(gameObject);
        }

        public void OnDamage(float f)
        {
            _currentLife -= f;

            if (_currentLife <= 0)
                Kill();
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimatioEase).From();
        }
        #endregion
    }
}
