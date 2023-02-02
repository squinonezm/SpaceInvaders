using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.Die();
        } else if (collider.tag == "ExitLaserZone")
        {
            gameObject.SetActive(false);
        } else if (collider.tag == "Wall")
        {
            gameObject.SetActive(false);
            WallController wall = collider.GetComponent<WallController>();
            wall.BreakWall();
        }
    }
}
