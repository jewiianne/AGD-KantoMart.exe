using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void BackToMainMenu(string KantoMart)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(KantoMart);
    }
}
