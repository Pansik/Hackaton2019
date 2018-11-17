using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance;

    public int currentLevel = 0;
    [Tooltip("Attach map prefab to be loaded")]
    public GameObject[] levels;

    public EnemySpawner enemySpawner;

    public AudioSource audioSource;
    public AudioClip level1, level2, level3;
    private AudioClip currentClip;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        currentClip = level1;
        audioSource.clip = currentClip;
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
        currentLevel++;
        if (currentLevel == 1)
            currentClip = level2;
        if (currentLevel == 2)
            currentClip = level3;
        audioSource.clip = currentClip;
        audioSource.Play();
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
