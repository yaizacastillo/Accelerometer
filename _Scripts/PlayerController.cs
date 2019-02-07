using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController m_gameController;

    public Color m_hasAcceletometer;
    public Color m_notHasAcceletometer;

    public float m_rotationSpeed;

    [Range(0.0f, 1.0f)] public float m_threshold;

    public GameObject m_bulletPrefab;


    private void Start()
    {
        GetComponent<SpriteRenderer>().color = SystemInfo.supportsAccelerometer ? m_hasAcceletometer : m_notHasAcceletometer;
    }

    private void Update()
    {
        if (!GameController.Instance.m_isGameStopped)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.acceleration.x > m_threshold || Input.acceleration.x < -m_threshold)
                {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, transform.eulerAngles.z - Input.acceleration.x * Time.deltaTime * m_rotationSpeed);
                }
            }

            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Vector2 l_mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 l_direction = (l_mouseScreenPosition - (Vector2)transform.position).normalized;

                transform.up = l_direction;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(m_bulletPrefab, transform.position + transform.up, transform.rotation);
    }
}
