using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D collider) 
    {
        if(collider.tag == "ExitLaserZone"){
            gameObject.SetActive(false);
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.CanShoot = true;
        } else if (collider.tag == "Invader") {
            gameObject.SetActive(false);
            InvadersController invader = collider.GetComponent<InvadersController>();
            invader.Die();
        } else if (collider.tag == "Wall")
        {
            gameObject.SetActive(false);
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.CanShoot = true;
            WallController wall = collider.GetComponent<WallController>();
            wall.BreakWall();
        } else if (collider.tag == "MysteryShip")
        {
            gameObject.SetActive(false);
            MothershipController mysteryShip = GameObject.Find("MysteryShip").GetComponent<MothershipController>();
            mysteryShip.Die();
        }
        ;
    }
}
