using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookPanel : MonoBehaviour, IPointerDownHandler
{
    public GameObject bookUIPanel;
    public GameObject reputationBar;
    public GameObject reputationStatus;
    public GameObject startUI;
    public GameObject endUI;

    public GameObject playerInventory;

    public void Start()
    {
        bookUIPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bookUIPanel.SetActive(true);
        reputationBar.SetActive(false);
        reputationStatus.SetActive(false);
        startUI.SetActive(false);
        endUI.SetActive(false);
        playerInventory.SetActive(false);
    }

    public void CloseBookPanel()
    {
        bookUIPanel.SetActive(false);
        reputationBar.SetActive(true);
        reputationStatus.SetActive(true);
        startUI.SetActive(true);
        endUI.SetActive(false);
        playerInventory.SetActive(true);
    }
}
