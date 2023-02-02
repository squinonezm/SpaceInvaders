using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersController : MonoBehaviour
{
    Animator m_animator;
    [SerializeField]
    private int scoreValue;
    // Start is called before the first frame update
    public int ScoreValue { get => scoreValue; }
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccelarateAnimation (float velocity)
    {
        m_animator.speed += velocity * 2.0f;
    }

    public void Die ()
    {
        gameObject.SetActive(false);
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.IncreaseScore(ScoreValue);
        player.CanShoot = true;
        InvadersManager.sharedInstace.InvadersAlive -= 1;
    }
}
