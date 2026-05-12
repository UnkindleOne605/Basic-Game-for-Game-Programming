using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenMenu : MonoBehaviour
{
    public void sceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
