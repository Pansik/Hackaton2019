using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {


    public int currentLevel = 0;
    public GameObject[] levels;

    public EnemySpawner enemySpawner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetKeyDown(KeyCode.H)){
            enemySpawner.DeleteEnemies();
        }
	}

    private void FinishedLevel()
    {
        currentLevel++;
        if (currentLevel < levels.Length)
        {
            NextLevel();
        }
        else
        {
            Debug.LogWarning("Game completed! buy dlc for more levels");
        }
    }

    private void NextLevel()
    {

    }
}
