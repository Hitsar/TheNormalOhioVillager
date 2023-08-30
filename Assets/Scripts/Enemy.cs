using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Animator _enemyAnimator;
        private Animator _windowAnimator;
        
        private bool _isPenetration;
        private bool _isHacking;

        private float _hackingTimer;
        private float _penetrationTimer;
        private float _fallTimer;

        private Collider _collider;
        private byte _health = 5;

        public event Action OnLose;

        private void Start()
        {
            _enemyAnimator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
            _windowAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_isHacking)
            {
                _hackingTimer += Time.fixedDeltaTime;
                if (_hackingTimer >= 13) OnLose?.Invoke();
            }

            if (_isPenetration)
            {
                _penetrationTimer += Time.fixedDeltaTime;
                if (_penetrationTimer >= 5) OnLose?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _enemyAnimator.SetTrigger("Penetration");
            _windowAnimator.SetTrigger("Open");
            _isHacking = false;
            _isPenetration = true;
        }

        private void OnTriggerExit(Collider other) => OnLose?.Invoke();

        private void Fall()
        {
            _collider.enabled = false;
            _enemyAnimator.SetTrigger("Fall");
            _isHacking = false;
            _isPenetration = false;
        }

        public void Hacking()
        {
            _collider.enabled = true;
            _enemyAnimator.SetTrigger("Hacking");
            _isHacking = true;
        }

        public void Hit()
        {
            _health--;
            if (_health >= 0) Fall();
        }
    }
}