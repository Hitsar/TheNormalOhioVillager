using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        private float _maxSecondForSpawn = 15;
        private float _secondForSpawn = 15;
        private float _time;

        private void FixedUpdate()
        {
            _time += Time.fixedDeltaTime;
            if (_time >= _secondForSpawn)
            {
                _enemies[Random.Range(0, _enemies.Length)].Hacking();
                if (_maxSecondForSpawn == 10)
                {
                    _secondForSpawn = 9;
                }
                else
                {
                    _maxSecondForSpawn--;
                    _secondForSpawn = Random.Range(9, _maxSecondForSpawn);
                }
                _time = 0;
            }
        }
    }
}