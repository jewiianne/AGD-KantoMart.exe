using UnityEngine;
using UnityEngine.EventSystems;

public class BookPanel : MonoBehaviour, IPointerDownHandler
{
    public GameObject bookUIPanel;

    public void OnPointerDown(PointerEventData eventData)
    {
        bookUIPanel.SetActive(true);
    }
}
