using UnityEngine;

public class StorePanel : MonoBehaviour
{
    public GameObject storePanelUI;
    void Start()
    {
        storePanelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStorePanel()
    {
        storePanelUI.SetActive(true);
    }

    public void CloseStorePanel()
    {
        storePanelUI.SetActive(false);
    }
}
