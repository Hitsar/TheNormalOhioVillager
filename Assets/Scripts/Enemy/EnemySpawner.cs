using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private Enemy[] _enemies;
        private int _maxSecondForSpawn = 15;
        private int _secondForSpawn = 10;
        private float _time;

        private void Start() => _enemies = FindObjectsOfType<Enemy>();

        private void FixedUpdate()
        {
            _time += Time.fixedDeltaTime;
            if (_time < _secondForSpawn) return;
            
            _enemies[Random.Range(0, _enemies.Length)].Hacking();
            
            if (_maxSecondForSpawn == 10)
            {
                _secondForSpawn = 9;
            }
            else
            {
                _maxSecondForSpawn--;
                _secondForSpawn = Random.Range(9, _maxSecondForSpawn + 1);
            }
            _time = 0;
        }
    }
}