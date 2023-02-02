using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbd2d;
    [Header("Movement")]
    private float m_horizontalMovement;
    [SerializeField] 
    private float movementVelocity;
    [SerializeField] 
    private float damp;

    Animator animator;

    private Vector3 velocity = Vector3.zero;
    private float xBoundaries;
    private Vector2 screenBounds;
    private float m_movementOffSet = 0.02f;

    [SerializeField]
    private GameObject m_laser;

    private float objectWidth;
    private List<GameObject> laserPool = new List<GameObject>();

    private int score;
    private bool canShoot;

    public int PlayerScore { get => score; set => score = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    private int m_lives = 3;

    // const string STAT_ALIVE = "IsAlive"; TO DO



    void Start()
    {
        // animator = GetComponent<Animator>(); TO DO
        // animator.SetBool(STAT_ALIVE, true); TO DO
        canShoot = true;
        score = 0;
        rbd2d = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalMovement = Input.GetAxisRaw("Horizontal") * movementVelocity;
        if (CanShoot){
            Shoot();
        }
    }

    void FixedUpdate () {
        Move(m_horizontalMovement * Time.fixedDeltaTime);
    }

    void Move(float move){
        Vector3 targetVelocity = new Vector2(move, 0f);
        rbd2d.velocity = Vector3.SmoothDamp(rbd2d.velocity, targetVelocity, ref velocity, damp);
        if (transform.position.x <= -screenBounds.x + objectWidth)
        {
            EdgeBoundaries(m_movementOffSet);
        } else if (transform.position.x >= screenBounds.x - objectWidth)
        {
            EdgeBoundaries(-m_movementOffSet);
        }
    }

    void EdgeBoundaries(float movementOffSet){
        Vector3 position = this.transform.position;
        rbd2d.velocity = Vector3.zero;
        position.x = transform.position.x + movementOffSet;
        transform.position = position;
    }

    void Shoot(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetLaser();
            CanShoot = false;
        }
    }

    public GameObject GetLaser () {
        for (int i = 0; i < laserPool.Count; i++)
        {   
            if(!laserPool[i].activeInHierarchy)
            {
                laserPool[i].SetActive(true);
                laserPool[i].transform.position = this.transform.position;
                laserPool[i].transform.rotation = this.transform.rotation;
                laserPool[i].SetActive(true);
                return laserPool[i];
            }
        }
        GameObject laser = Instantiate(m_laser, this.transform.position, this.transform.rotation) as GameObject;
        laserPool.Add(laser);
        return laser;
    }

    public void IncreaseScore (int invaderScore)
    {
        PlayerScore += invaderScore;
        Debug.Log(PlayerScore);
        UpdateText score = GameObject.Find("Score").GetComponent<UpdateText>();
        score.ChangeText(PlayerScore);
    }

    public void Die ()
    {
        m_lives -=1;
        Debug.Log(m_lives);
        UpdateText lives = GameObject.Find("Lives").GetComponent<UpdateText>();
        lives.ChangeText(m_lives);
        if(m_lives == 0)
        {
            GameManager.sharedInstace.GameOver();
            CanShoot = false;
            movementVelocity = 0.0f;
        }
    }
}
