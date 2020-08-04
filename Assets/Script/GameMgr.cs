using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public Text score_text;
    public Slider healthSlider;
    public PlayerMove m_playMove;

    public List<int> num_of_enemy;
    public List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    public int wave, score, initiated_enemies;
    public float respawn_gap;

    public float curSpawnTime;

    public List<GameObject> released_enemies;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        score = 0;
        initiated_enemies = 0;
        curSpawnTime = respawn_gap;

        if (!m_playMove)
        {
            m_playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        }
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

        updateUI();
    }

    void updateUI()
    {
        score_text.text = "Money: " + score.ToString();
        healthSlider.value = m_playMove.healthVal;
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
    }
    
    void addScore()
    {
        score = score + (wave * 100);
    }

    void Respawn()
    {
        initiated_enemies++;
        //randomize the pos
        int initpos = Random.Range(0, spawnPoints.Count);
        GameObject enemyCreated = (GameObject)Instantiate(enemyPrefab, spawnPoints[initpos].transform.position, Quaternion.identity);
        released_enemies.Add(enemyCreated);
    }
}
