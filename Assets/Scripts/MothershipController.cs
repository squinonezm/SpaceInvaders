using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipController : MonoBehaviour
{
    [SerializeField]
    private int m_score = 1000;
    private float m_velocity = 5.0f;
    private Vector3 m_direction = Vector2.left;
    private bool m_isMoving;

    public bool IsMoving { get => m_isMoving; set => m_isMoving = value; }
    
    private Vector3 m_leftEdgeScreen;

    private Vector3 m_initialPosition = new Vector3(17, 10, 0);

    private float m_timerSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        m_leftEdgeScreen = Camera.main.ViewportToWorldPoint(Vector3.zero);
        m_timerSpawn = GetRandomTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_timerSpawn >= 0)
        {
            m_timerSpawn -= Time.deltaTime;
        } else if (m_timerSpawn < 0 && IsMoving == false)
        {
            IsMoving = true;
        }
        if(IsMoving)
        {
            this.transform.position  += m_direction * m_velocity * Time.deltaTime;
        }
    }

    float GetRandomTimer ()
    {
        float timer = Random.Range(10, 20);
        return timer;
    }

    public void Die ()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.IncreaseScore(m_score);
        player.CanShoot = true;
        RestartPosition();
    }

    private void RestartPosition ()
    {
        IsMoving = false;
        this.transform.position = m_initialPosition;
        m_timerSpawn = GetRandomTimer();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "ExitMotherShip")
        {
            RestartPosition();
        }
    }
}
