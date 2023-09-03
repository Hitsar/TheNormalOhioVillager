using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _rayDistance;
        private Animator _animator;
        private AudioSource _audioHit;
        private float _time;

        public void Init(InputSystem input)
        {
            input.Player.Hit.performed += _ => _animator.SetTrigger("Hit");
            _audioHit = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate() => _time += Time.fixedDeltaTime;

        private void Hit()
        {
            if (_time < 0.4f) return;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _rayDistance) 
                && hit.collider.gameObject.TryGetComponent(out Enemy.Enemy enemy)) enemy.TakeDamage();
            _time = 0;
            _audioHit.Play();
        }
    }
}