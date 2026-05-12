using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused;
    public InputAction pause;

    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }
    
    void Start()
    {
        Debug.Log("Start");
        pauseMenuUI.SetActive(false); 
    }

    void Update()
    {
        if(pause.triggered)
        {
            Debug.Log("Escape Pressed");
            if(pauseMenuUI.activeSelf)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
