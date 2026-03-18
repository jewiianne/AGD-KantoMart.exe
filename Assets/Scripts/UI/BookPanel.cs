using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookPanel : MonoBehaviour, IPointerDownHandler
{
    public GameObject bookUIPanel;
    public GameObject reputationBar;
    public GameObject startUI;
    public GameObject endUI;
    public GameObject openPanel;
    public GameObject playerInventory;

    public void Start()
    {
        bookUIPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bookUIPanel.SetActive(true);
        reputationBar.SetActive(false);
        startUI.SetActive(false);
        endUI.SetActive(false);
        openPanel.SetActive(false);
        playerInventory.SetActive(false);
    }

    public void CloseBookPanel()
    {
        bookUIPanel.SetActive(false);
        reputationBar.SetActive(true);
        startUI.SetActive(true);
        endUI.SetActive(true);
        openPanel.SetActive(true);
        playerInventory.SetActive(true);
    }
}
