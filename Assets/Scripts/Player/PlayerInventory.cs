using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<Items> currentItems = new List<Items>();
    public List<BoxItems> currentBoxItems = new List<BoxItems>();
    public List<GameObject> itemDisplayInventory;
    public int maxSlot = 5;
    public int selectedSlotIndex = 0;
    public static PlayerInventory Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (scroll > 0) selectedSlotIndex--;
            else if (scroll < 0) selectedSlotIndex++;

            if (selectedSlotIndex < 0) selectedSlotIndex = maxSlot - 1;
            if (selectedSlotIndex >= maxSlot) selectedSlotIndex = 0;

            UpdateInventoryUI();
            Debug.Log("Holding Slot: " + selectedSlotIndex);
        }
    }

    void SetSlot(int index, Sprite sprite)
    {
        itemDisplayInventory[index].SetActive(true);
        var image = itemDisplayInventory[index].GetComponentInChildren<UnityEngine.UI.Image>();
        if (image != null) image.sprite = sprite;

        if (index == selectedSlotIndex)
        {
            itemDisplayInventory[index].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);   
        }
        else
        {
            itemDisplayInventory[index].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }      
    }

    public bool CanReceiveItem()
    {
        return currentItems.Count < maxSlot;
    }

    public void ReceiveItem(Items item)
    {
        if(CanReceiveItem())
        {
            currentItems.Add(item);
            UpdateInventoryUI();
            Debug.Log("Added: " + item.itemName + " to inventory");
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public bool CanReceiveBox()
    {
        return currentItems.Count < maxSlot;
    }

    public void ReceiveBox(BoxItems box)
    {
        if (CanReceiveBox())
        {
            currentBoxItems.Add(Instantiate(box)); 
            UpdateInventoryUI();
            Debug.Log("Box added to inventory: " + box.boxItemName);
        }
    } 

    public void UpdateInventoryUI()
    {
        foreach (GameObject slot in itemDisplayInventory) slot.SetActive(false);

        int currentSlotIndex = 0;

        for (int i = 0; i < currentItems.Count; i++)
        {
            if (currentSlotIndex < itemDisplayInventory.Count)
            {
                SetSlot(currentSlotIndex, currentItems[i].itemSprite);
                currentSlotIndex++;
            }
        }

        for (int i = 0; i < currentBoxItems.Count; i++)
        {
            if (currentSlotIndex < itemDisplayInventory.Count)
            {
                SetSlot(currentSlotIndex, currentBoxItems[i].boxItemSprite); 
                currentSlotIndex++;
            }
        }
    }

    public void RemoveItem(Items item)
    {
        if (currentItems.Contains(item))
        {
            currentItems.Remove(item);
            Debug.Log("Removed: " + item.itemName);
            
            UpdateInventoryUI(); 
        }
    }
}
