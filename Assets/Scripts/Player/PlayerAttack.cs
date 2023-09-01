using System;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _rayDistance;
        private float _time;

        public void Init(InputSystem input) => input.Player.Hit.performed += _ => Hit();

        private void FixedUpdate() => _time += Time.fixedDeltaTime;

        private void Hit()
        {
            if (_time < 0.4f) return;
            
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,_rayDistance) && hit.collider.gameObject.TryGetComponent(out Enemy.Enemy enemy))
            {
                enemy.Hit();
                _time = 0;
            }
        }
    }
}