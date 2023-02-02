using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameState m_currentGameState = GameState.menu;
    public static GameManager sharedInstace;

    public GameState CurrentGameState { get => m_currentGameState; set => m_currentGameState = value; }


    void Awake() {
        if (sharedInstace == null)
        {
            sharedInstace = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState (GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            
            // MenuManager.sharedInstance.ShowMainMenu(true);
        } else if (newGameState == GameState.inGame)
        {
            GameObject initialMenu = GameObject.Find("InitialMenu");
            MenuManager.sharedInstace.EnableInitialMenu(false);
            // initialMenu.SetActive(false);

            // Initialize grid movement and invaders shooting.
            InvadersManager.sharedInstace.Direction = Vector2.right;
            InvadersManager.sharedInstace.StartShooting();

            // MenuManager.sharedInstance.ShowMainMenu(false);
            // controller.StartGame();
        } else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstace.EnableGameOverMenu(true);
            InvadersManager.sharedInstace.Direction = Vector2.zero;
            InvadersManager.sharedInstace.StopShooting();
            MothershipController mysteryShip = GameObject.Find("MysteryShip").GetComponent<MothershipController>();
            mysteryShip.IsMoving = false;
        }

        this.m_currentGameState = newGameState;
    }
}
