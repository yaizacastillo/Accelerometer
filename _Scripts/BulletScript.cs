using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IBullets
{
    public float m_speed;
    float m_destroyTimer = 0.0f;
    float m_timeToDestroy = 5.0f;

    private void Start()
    {
        AddMeToList();
    }

    void Update () {
        if (!GameController.Instance.m_isGameStopped)
        {
            transform.position += transform.up * m_speed * Time.deltaTime;
        }

        m_destroyTimer += Time.deltaTime;

        if(m_destroyTimer >= m_timeToDestroy)
        {
            Destroy(this.gameObject);
        }
	}

    public void AddMeToList()
    {
        GameController.Instance.m_bulletsList.Add(this);
    }

    public void DestroyMe()
    {
        if (this != null) Destroy(this.gameObject);
    }
}
