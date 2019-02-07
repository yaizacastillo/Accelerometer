using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject m_enemyPrefab;

    Vector2 m_spawnPosition;

    public int m_startNumberEnemies = 1;
    [HideInInspector] public int  m_numberEnemies;

    private void Start()
    {
        m_numberEnemies = m_startNumberEnemies;
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < m_numberEnemies; i++)
        {
            int rdm = Random.Range(1, 5);

            switch (rdm)
            {
                case 1: //top
                    m_spawnPosition.x = Random.Range(-9.0f, 9.0f);
                    m_spawnPosition.y = 6.0f;
                    break;
                case 2: //bottom
                    m_spawnPosition.x = Random.Range(-9.0f, 9.0f);
                    m_spawnPosition.y = -6.0f;
                    break;
                case 3: //right
                    m_spawnPosition.x = 9.0f;
                    m_spawnPosition.y = Random.Range(-6.0f, 6.0f);
                    break;
                case 4: //left
                    m_spawnPosition.x = -9.0f;
                    m_spawnPosition.y = Random.Range(-6.0f, 6.0f); ;
                    break;
            }

            GameObject l_enemy = Instantiate(m_enemyPrefab);
            l_enemy.transform.position = m_spawnPosition;
            l_enemy.transform.LookAt(FindObjectOfType<PlayerController>().transform.position);
            l_enemy.transform.eulerAngles = new Vector3(0.0f, 0.0f, l_enemy.transform.eulerAngles.z);
        }

        m_numberEnemies ++;

    }

}
