using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController Instance;

    public int currentLevel = 0;
    [Tooltip("Attach map prefab to be loaded")]
    public GameObject[] levels;

    public GameObject[] doors;

    public EnemySpawner enemySpawner;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        debug();
    }

    private void debug()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            enemySpawner.DeleteEnemies();
        }
    }

    public void FinishedLevel()
    {
        Destroy(doors[currentLevel]);
        currentLevel++;
        EnemySpawner.Instance.DeleteEnemies();
        if (currentLevel < levels.Length && PlayerController.Instance.Died == false)
        {
            Debug.LogWarning("gotta rework leveling");
            NextLevel();
        }
        else
        {
            Debug.LogWarning("Game completed! buy dlc for more levels");
        }
    }

    private void NextLevel()
    {
        Debug.LogError("load next level");
        EnemySpawner.currentNumberOfEnemies = 0;
    }
}
