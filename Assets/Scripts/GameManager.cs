using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject gamePause;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameOverMenu()
    {
        selectLevel.SetActive(false);
        gameOver.SetActive(true);
        gamePause.SetActive(false);
        gameWin.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 0;
    }
    public void gameWinMenu()
    {
        selectLevel.SetActive(false);
        gameWin.SetActive(true);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 0;
    }
    public void gamePauseMenu()
    {
        selectLevel.SetActive(false);
        gamePause.SetActive(true);
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        selectLevel.SetActive(false);
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ResumeGame()
    {
        selectLevel.SetActive(false);
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        selectLevel.SetActive(false);
        Time.timeScale = 0;
    }
    public void SelectLevel()
    {
        selectLevel.SetActive(true);
        mainMenu.SetActive(true);
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 0;
    }
}
