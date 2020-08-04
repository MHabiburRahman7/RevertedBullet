using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public Text score_text, wave_text;
    public Slider healthSlider, towerHealthSlider;
    public NewPlayerMove m_playMove;

    public List<int> num_of_enemy;
    public List<Transform> spawnPoints;
    public GameObject enemyPrefab, gameOverObj;
    public int wave, score, initiated_enemies;
    public float respawn_gap;

    public Transform left, right;

    public float curSpawnTime;

    public List<GameObject> released_enemies;
    public GameObject[] availableTowers;

    private float TotalTowerHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        score = 0;
        initiated_enemies = 0;
        curSpawnTime = respawn_gap;

        if (!m_playMove)
        {
            m_playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMove>();
        }

        if(availableTowers.Length == 0)
        {
            availableTowers = GameObject.FindGameObjectsWithTag("Tower");
        }

        for(int i=0; i<availableTowers.Length; i++)
        {
            TotalTowerHealth += availableTowers[i].GetComponent<TowerMgr>().Health;
        }

        Invoke("updateUI", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnTime -= 1 * Time.deltaTime;
        if (curSpawnTime <= 0 && (initiated_enemies < num_of_enemy[wave]))
        {
            Respawn();
            curSpawnTime = respawn_gap;
        }
        if(initiated_enemies > 0)
            checkEnemy();

        if (wave == num_of_enemy.Count)
            GameOver();
        //updateUI();
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        gameOverObj.GetComponent<GameOver>().score = score.ToString();
    }

    public void updateUI()
    {
        score_text.text = "Money: " + score.ToString();
        wave_text.text = "Wave: " + wave.ToString();
        healthSlider.value = m_playMove.healthVal;

        float tempHealth = 0;
        for(int i=0; i<availableTowers.Length; i++)
        {
            tempHealth += availableTowers[i].GetComponent<TowerMgr>().Health;
        }
        towerHealthSlider.value = (tempHealth / TotalTowerHealth) * 100;
    }

    void checkEnemy()
    {
        for(int i=0; i<released_enemies.Count; i++)
        {
            if (released_enemies[i] == null)
            {
                released_enemies.RemoveAt(i);
                addScore();
            }
        }

        if (released_enemies.Count == 0)
            nextWave();
    }

    void nextWave()
    {
        //update wave, and information
        wave++;
        //we also can make temporary pause here if u want
        initiated_enemies = 0;

        updateUI();
    }
    
    void addScore()
    {
        score = score + (wave * 100);
        updateUI();
    }

    void Respawn()
    {
        initiated_enemies++;
        //randomize the pos
        int initpos = Random.Range(0, spawnPoints.Count);
        GameObject enemyCreated = (GameObject)Instantiate(enemyPrefab, spawnPoints[initpos].transform.position, Quaternion.identity);
        enemyCreated.GetComponent<EnemyAi>().InitWaypoint(left, right);
        released_enemies.Add(enemyCreated);
    }
}
