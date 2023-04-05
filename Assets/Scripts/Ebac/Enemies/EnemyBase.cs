using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float StartLife = 10f;

        [SerializeField] private float _currentLife;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
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
    }
}
