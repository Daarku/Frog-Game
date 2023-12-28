using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game ended")]
    [SerializeField] private GameObject gameOverSceen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Game paused")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        gameOverSceen.SetActive(false);
        pauseScreen.SetActive(false);
     }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void GameOver()
    {
        gameOverSceen.SetActive(true);
        SoundManager.instance.Playsound(gameOverSound);
    }

    public void PauseGame(bool status) {
        pauseScreen.SetActive(status);
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
