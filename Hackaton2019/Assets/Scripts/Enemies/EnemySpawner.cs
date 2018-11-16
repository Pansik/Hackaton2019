using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static int currentNumberOfEnemies;
    public int maxNumberOfEnemiesPerWave;

    [SerializeField]
    private GameObject[] enemies;

    private List<GameObject> listOfEnemies;

    private float timeSinceLastSpawn = 0f;
    [SerializeField]
    private float timeBetweenSpawns = 2f;

	// Use this for initialization
	void Start () {
        listOfEnemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }

		
	}

    private void SpawnEnemy()
    {
        if(currentNumberOfEnemies >= maxNumberOfEnemiesPerWave)
        {
            EndWave();
            return;
        }
        var newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], transform);
        listOfEnemies.Add(newEnemy);
        currentNumberOfEnemies++;
    }

    private void EndWave()
    {
        Debug.Log("End of wave!");
        //Destroy(gameObject);
        //listOfEnemies.Clear();
        if(listOfEnemies.Count == 0)
        {
            Debug.Log("all enemies are dead!");
            Destroy(gameObject);
        }

    }

    public void DeleteEnemyFromList(GameObject targetEnemy)
    {
        if (listOfEnemies.Contains(targetEnemy))
            listOfEnemies.Remove(listOfEnemies.Find(x => x.gameObject == targetEnemy));
    }
}
