using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    private int enemyCount;
    //1-2-3- �eklinde enemy ��kmas� i�in
    public int waveNumber = 1;
    void Start()
    {
        SpawnEnemyWave(waveNumber);

        //Powerup yollas�n diye ilk ba�ta
        Instantiate(powerupPrefab,GenerateSpawnPosition(),powerupPrefab.transform.rotation);
    }
    private void Update()
    {
       
        //Enemy scriptinden getirmek i�in
        enemyCount=FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            //Powerup yollas�n diye her s�f�rlamadas
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
          Instantiate(enemyPrefab,GenerateSpawnPosition(),enemyPrefab.transform.rotation);

        }


    }
    private Vector3 GenerateSpawnPosition()
    {
        //Bu k�sm� starttan al�p buraya koyduk.
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
}
