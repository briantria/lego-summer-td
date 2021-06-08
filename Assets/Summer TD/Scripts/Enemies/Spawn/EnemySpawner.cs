using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class EnemySpawner : MonoBehaviour, IAction
    {
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private Transform _target;

        private LevelSpawnData _levelSpawnData;

        #region Unity Messages
        private void OnEnable()
        {
            GameLoopController.OnChangeGameState += OnChangeGameState;
        }

        private void OnDisable()
        {
            GameLoopController.OnChangeGameState -= OnChangeGameState;
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 spawnArea = new Vector3(_spawnArea.x, 4, _spawnArea.y);
            Vector3 spawnCenter = transform.position;
            spawnCenter.y = spawnArea.y;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(spawnCenter, spawnArea);
        }
        #endregion

        public void Activate()
        {
            _levelSpawnData = Resources.Load<LevelSpawnData>("LevelSpawnData/Level_0");
            StartCoroutine(StartLevel(_levelSpawnData.SpawnList));
        }

        #region Routines
        private IEnumerator StartLevel(List<WaveSpawnData> waveSpawnList)
        {
            foreach (WaveSpawnData waveData in waveSpawnList)
            {
                yield return new WaitForSeconds(waveData.Delay);
                for (int idx = waveData.MaxSpawnCount; idx > 0; --idx)
                {
                    yield return SpawnEnemy(waveData.Interval, waveData.EnemyPrefab);
                }
            }
        }

        private IEnumerator SpawnEnemy(float waitTime, GameObject enemyPrefab)
        {
            GameObject enemyObj = Instantiate(enemyPrefab, transform);
            Vector2 pos = _spawnArea * 0.5f;
            pos.x = Random.Range(-pos.x, pos.x);
            pos.y = Random.Range(-pos.y, pos.y);
            Transform enemyTransform = enemyObj.transform;
            enemyTransform.localPosition = new Vector3(pos.x, 0, pos.y);

            Frog frog = enemyObj.GetComponent<Frog>();
            frog.SetTarget(_target);
            yield return new WaitForSeconds(waitTime);
        }
        #endregion

        #region Game State Handlers

        #endregion

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.LevelIntro:
                    break;

                case GameState.ShootMode:
                    Activate();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
