using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_wallSpritesList;
    private int m_resistanceOfWall;
    // Start is called before the first frame update

    public int ResistanceWall { get => m_resistanceOfWall; set => m_resistanceOfWall = value; }


    void Start()
    {
        m_resistanceOfWall = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakWall()
    {
        ResistanceWall -= 1;
        if (ResistanceWall < 0)
        {
            gameObject.SetActive(false);
        } else {
            ChangeSprite(ResistanceWall);
        }
    }

    void ChangeSprite(int spriteIndex)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = m_wallSpritesList[spriteIndex];
    }
}
