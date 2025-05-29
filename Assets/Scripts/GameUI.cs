using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{

    [SerializeField] private Button[] buttons;
    [SerializeField] private GameManager GameManager;
    public GameObject player;
    public SaveManager SaveManagers;
    public Transform Gamepoint;
    private void Awake()
    {
        SaveManagers = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("NewGame"))
        {
            startGame();
        }
        else
        {      
            PlayerPrefs.DeleteKey("NewGame");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        GameManager.MainMenu();
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        GameManager.MainMenu();
    }
    public void ResumeGame()
    {
        GameManager.ResumeGame();

    }
    public void Chonlai()
    {
        SaveManagers.LoadGame();
        ResumeGame();
    }
    //public void SaveButton()
    //{
    //    //SaveManagers.SaveGame(player.transform.position);
    //}
    //public void ContinueButton()
    //{
    //    if (SaveManager.SaveExists())
    //    {
    //       // SaveData data = SaveManager.LoadGame();
    //       // SceneManager.LoadScene(data.sceneName);
    //       // SaveManagers.LoadGameAndApply();
           
    //        Time.timeScale = 1;
    //    }
    //    else
    //    {
    //        Debug.Log("No save found!");
    //    }
    //}
    public void NewGameButton()
    {
        SaveManager.DeleteSave();
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(0);     
        ResumeGame();
        Time.timeScale = 1;
    }
    public void Chonman()
    {
        GameManager.SelectLevel();
    }
    public void SelectLevel(Transform gamepoint )
    {
        ResumeGame();
        player.transform.position =gamepoint.position;
    }
    public void Spawner(GameObject enemy)
    {
        ResumeGame();
        if(enemy != null)
        {
            enemy.SetActive(true);
        }
        
    }
    public void Alllies(GameObject allies)
    {
        ResumeGame();
        if (allies != null)
        {
                allies.SetActive(true);
        }

    }

}
