using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstace;
    [SerializeField]
    private GameObject m_initialMenu;

    [SerializeField]
    private GameObject m_GameOverMenu;
    // Start is called before the first frame update
    void Awake()
    {
        if (sharedInstace == null)
        {
            sharedInstace = this;
        }
    }

    public void EnableInitialMenu(bool enabled)
    {
        m_initialMenu.SetActive(enabled);
    }

    public void EnableGameOverMenu(bool enabled)
    {
        m_GameOverMenu.SetActive(enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
