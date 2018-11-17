using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public static int currentNumberOfEnemies;
    public int maxNumberOfEnemiesPerWave;

    [SerializeField]
    private GameObject[] enemiesLevelOne;
    [SerializeField]
    private GameObject[] enemiesLevelTwo;
    [SerializeField]
    private GameObject[] enemiesLevelThree;

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
        GameObject newEnemy = null;
        if(currentNumberOfEnemies >= maxNumberOfEnemiesPerWave)
        {
            EndWave();
            return;
        }
        switch (LevelController.Instance.currentLevel)
        {
            case 1:
                newEnemy = Instantiate(enemiesLevelOne[Random.Range(0, enemiesLevelOne.Length)], transform);
                currentNumberOfEnemies++;
                break;
            case 2:
                newEnemy = Instantiate(enemiesLevelTwo[Random.Range(0, enemiesLevelTwo.Length)], transform);
                currentNumberOfEnemies++;
                break;
            case 3:
                newEnemy = Instantiate(enemiesLevelThree[Random.Range(0, enemiesLevelThree.Length)], transform);
                currentNumberOfEnemies++;
                break;
            default:
                newEnemy = Instantiate(enemiesLevelOne[Random.Range(0, enemiesLevelOne.Length)], transform);
                currentNumberOfEnemies++;
                break;
        }
        
        listOfEnemies.Add(newEnemy);
    }

    private void EndWave()
    {
        Debug.Log("End of wave!");
        if(listOfEnemies.Count == 0)
        {
            Debug.Log("all enemies are dead!");
            LevelController.Instance.FinishedLevel();
        }

    }

    public void DeleteEnemyFromList(GameObject targetEnemy)
    {
        if (listOfEnemies.Contains(targetEnemy))
            listOfEnemies.Remove(listOfEnemies.Find(x => x.gameObject == targetEnemy));
    }

    public void DeleteEnemies()
    {
        for (int i = listOfEnemies.Count -1; i >= 0; i--)
        {
            Destroy(listOfEnemies[i]);
        }
        listOfEnemies.Clear();
    }
}
