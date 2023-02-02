using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersManager : MonoBehaviour
{
    public static InvadersManager sharedInstace;
    [SerializeField]
    private List<InvadersController> Invaders;

    [SerializeField]
    private GameObject m_beamInvaderPrefab;

    [SerializeField]
    private GameObject player;
    private List<GameObject> m_beamList = new List<GameObject>();
    private int m_rows = 5;
    private int m_columns = 11;

    private float m_offSetX = -10.0f;

    private float m_offSetY = 0.0f;

    private float m_movementSpeed = 1.0f;


    private Vector3 m_direction = Vector2.zero;
    private Vector3 m_leftEdgeScreen;
    private Vector3 m_rightEdgeScreen;

    private float m_fireRate = 1.0f;
    private int m_allInvaders;
    private int m_invadersAlive;
    public int InvadersAlive { get => m_invadersAlive; set => m_invadersAlive = value; }
    public Vector3 Direction { get => m_direction; set => m_direction = value; }
    // public float FireRate { get => m_fireRate; set => m_fireRate = value; }

    const float SPEED_INCREMENT = 0.15f;

    // Start is called before the first frame update
    void Awake(){
        CreateGrid();
    }
    void Start()
    {
        if(sharedInstace == null)
        {
            sharedInstace = this;
        }
        m_leftEdgeScreen = Camera.main.ViewportToWorldPoint(Vector3.zero);
        m_rightEdgeScreen = Camera.main.ViewportToWorldPoint(Vector3.right);

        m_allInvaders = m_rows * m_columns;
        m_invadersAlive = m_allInvaders;
    }

    public void StartShooting ()
    {
        InvokeRepeating("InvaderShooting", m_fireRate, m_fireRate);
    }

    public void StopShooting ()
    {
        CancelInvoke("InvaderShooting");
    }

    void Update()
    {
        this.transform.position  += m_direction * m_movementSpeed * Time.deltaTime;
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            } else if (m_direction == Vector3.right && invader.position.x >= (m_rightEdgeScreen.x - 1.0f)) {
                FlipAndDown();
            }

            else if (m_direction == Vector3.left && invader.position.x <= (m_leftEdgeScreen.x + 1.0f)) {
                FlipAndDown();
            } 
            else if (invader.position.y <= player.transform.position.y + 0.5f)
            {
                GameManager.sharedInstace.GameOver();
            }

        }
    }

    void FlipAndDown () {
        m_direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
        m_movementSpeed += SPEED_INCREMENT;

        foreach (Transform invader in this.transform)
        {
            InvadersController invaderController = invader.GetComponent<InvadersController>();
            invaderController.AccelarateAnimation(SPEED_INCREMENT);
        }
    }

    void InvaderShooting () {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1.0f / (float) this.InvadersAlive))
            {
                GetBeam(invader.position);
            }
        }
    }

    public GameObject GetBeam(Vector3 position)
    {
        for (int i = 0; i < m_beamList.Count; i++)
        {   
            if(!m_beamList[i].activeInHierarchy)
            {
                m_beamList[i].SetActive(true);
                m_beamList[i].transform.position = position;
                m_beamList[i].transform.rotation = Quaternion.identity;
                return m_beamList[i];
            }
        }
        GameObject laser = Instantiate(this.m_beamInvaderPrefab, position, Quaternion.identity) as GameObject;
        m_beamList.Add(laser);
        return laser;
        
    }

    void CreateGrid(){
        for (int row = 0; row < m_rows; row++)
        {
            for (int col = 0; col < m_columns; col++)
            {
                Vector3 position = new Vector3(0.0f, row * 2.0f, 0.0f);
                if(row <= 1) {
                    PositionateInvader(0, col, position);
                } else if (row >= 2 && row < 4)
                {
                    PositionateInvader(1, col, position);
                } else if (row == 4)
                {
                    PositionateInvader(2, col, position);
                }
            }
        }
    }

    void PositionateInvader(int index, int column, Vector3 position)
    {
        InvadersController invader;
        invader = Instantiate(this.Invaders[index], this.transform);
        position.x = m_offSetX + (column * 2.0f);
        position.y = m_offSetY + position.y;
        invader.transform.position = position;
    }
}
