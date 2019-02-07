using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemies {

    public PlayerController m_player;
    public float m_speed;

    private void Start()
    {
        AddMeToList();
        transform.localScale *= Random.Range(1.0f, 2.0f);
        m_speed *= (1 / transform.localScale.x);
        m_player = GameController.Instance.m_player;
    }

    private void Update()
    {
        if (!GameController.Instance.m_isGameStopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, m_speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameController.Instance.PlayerEnemyCollision(this);
            StartCoroutine(DestroyAfterSound());
        }

        if (collision.tag == "Bullet")
        {
            GameController.Instance.BulletEnemyCollision(this, collision.GetComponent<BulletScript>());
            Destroy(collision.gameObject);
            StartCoroutine(DestroyAfterSound());
        }
    }

    public void AddMeToList()
    {
        GameController.Instance.m_enemiesList.Add(this);
    }

    public void DestroyMe()
    {
        if(this!=null) Destroy(this.gameObject);
    }

    public IEnumerator DestroyAfterSound()
    {
        transform.position = Vector2.one * 999;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
