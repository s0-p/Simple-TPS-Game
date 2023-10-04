using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("최소 스폰 시간"), SerializeField]
    float _spawnMinTime = 1f;
    [Header("최대 스폰 시간"), SerializeField]
    float _spawnMaxTime = 3f;
    [Header("스폰 위치"), SerializeField]
    Transform[] _spawnPos;
    [Header("적 프리팹"), SerializeField]
    GameObject[] _enemyPrefs;
    void Start() { StartCoroutine(Crt_Spawn()); }
    IEnumerator Crt_Spawn()
    {
        do
        {
            Spawn();

            float spawnTime = Random.Range(_spawnMinTime, _spawnMaxTime);
            yield return new WaitForSeconds(spawnTime);

        } while (true);
    }
    void Spawn()
    {
        int spawnPosIdx = Random.Range(0, _spawnPos.Length);
        int prefIdx = Random.Range(0, _enemyPrefs.Length);

        Instantiate(_enemyPrefs[prefIdx], _spawnPos[spawnPosIdx].position, _spawnPos[spawnPosIdx].rotation);
    }
}
