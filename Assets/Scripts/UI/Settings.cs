using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject settingsUIPanel;
    public GameObject controlsUIPanel;
    private bool isPaused = false;

    void Start()
    {
        settingsUIPanel.SetActive(false);
        controlsUIPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                CloseSettingsUIPanel();
            else
                OpenSettingsUIPanel();
        }
    }

    public void OpenSettingsUIPanel()
    {
        isPaused = true;
        settingsUIPanel.SetActive(true);
 
        Time.timeScale = 0f; 
    }

    public void CloseSettingsUIPanel()
    {
        isPaused = false;
        settingsUIPanel.SetActive(false);
        controlsUIPanel.SetActive(false);
        
        Time.timeScale = 1f; 
    }

    public void OpenControlsUIPanel()
    {
        controlsUIPanel.SetActive(true);
        settingsUIPanel.SetActive(false);
    }

    public void CloseControlUIPanel()
    {
        controlsUIPanel.SetActive(false);
        settingsUIPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}