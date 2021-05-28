using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class EnemySpawner : MonoBehaviour, IAction
    {
        [SerializeField] private float _spawnRadius;
        private LevelSpawnData _levelSpawnData;

        public void Activate()
        {
            Debug.Log("GO!");
            _levelSpawnData = Resources.Load<LevelSpawnData>("LevelSpawnData/Level_0");
            StartCoroutine(StartLevel(_levelSpawnData.SpawnList));
        }

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
            yield return new WaitForSeconds(waitTime);
            Debug.Log("SPAWN!");
            GameObject enemyObj = Instantiate(enemyPrefab, transform);
            Vector2 pos = Random.insideUnitCircle * Random.Range(0, _spawnRadius);
            Transform enemyTransform = enemyObj.transform;
            enemyTransform.localPosition = new Vector3(pos.x, 0, pos.y);
        }

        private void OnDrawGizmosSelected()
        {
            /*
             
             var theta:float = 0;
             var x:float = Radius*Mathf.Cos(theta);
             var y:float = Radius*Mathf.Sin(theta);
             var pos:Vector3 = T.position+Vector3(x,0,y);
             var newPos:Vector3 = pos;
             var lastPos:Vector3 = pos;
             for(theta = 0.1;theta<Mathf.PI*2;theta+=0.1){
              x = Radius*Mathf.Cos(theta);
              y = Radius*Mathf.Sin(theta);
              newPos = T.position+Vector3(x,0,y);
              Gizmos.DrawLine(pos,newPos);
              pos = newPos;
             }
             Gizmos.DrawLine(pos,lastPos);
             */

            Gizmos.color = Color.yellow;
            float theta = 0.0f;
            float x = _spawnRadius * Mathf.Cos(theta);
            float y = _spawnRadius * Mathf.Sin(theta);
            Vector3 pos = transform.position + new Vector3(x, 0, y);
            Vector3 newPos = pos;
            Vector3 lastPos = pos;

            for (theta = 0.5f; theta < Mathf.PI * 2; theta += 0.5f)
            {
                x = _spawnRadius * Mathf.Cos(theta);
                y = _spawnRadius * Mathf.Sin(theta);
                newPos = transform.position + new Vector3(x, 0, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }

            Gizmos.DrawLine(pos, lastPos);
        }
    }
}
