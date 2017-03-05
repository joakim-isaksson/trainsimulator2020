using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class TrainSpawner : MonoBehaviour
    {
        public GameObject TrainPrefab;

        public float StartingSpawnTimeMin;
        public float StartingSpawnTimeMax;
        public float SpawnSpeedUpMin;
        public float SpawnSpeedUpMax;
        public float MinSpawnTime;

        float SpawnTime;
        float TimeUntilNextSpawn;

        void Awake()
        {

        }

        void Start()
        {
            if (Random.value > 0.5f) Instantiate(TrainPrefab, transform.position, transform.rotation);

            SpawnTime = Random.Range(StartingSpawnTimeMin, StartingSpawnTimeMax);
            TimeUntilNextSpawn = SpawnTime;
        }

        void Update()
        {
            if (GameManager.GameOver) return;

            TimeUntilNextSpawn -= Time.deltaTime;
            if (TimeUntilNextSpawn <= 0)
            {
                Instantiate(TrainPrefab, transform.position, transform.rotation);

                SpawnTime = Mathf.Max(SpawnTime - Random.Range(SpawnSpeedUpMin, SpawnSpeedUpMax), MinSpawnTime);
                TimeUntilNextSpawn = SpawnTime;
            }
        }
    }
}