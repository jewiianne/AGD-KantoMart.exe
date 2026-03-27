using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsUIPanel;

    void Awake()
    {
        settingsUIPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("KantoMart");
    }

    //SettingsUI
    public void SettingsPanel()
    {
        settingsUIPanel.SetActive(true);
    }

    public void SettingsPanelClose()
    {
        settingsUIPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
