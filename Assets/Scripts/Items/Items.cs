using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "KantoMart/Item")]

[System.Serializable]
public class Items : ScriptableObject
{
    public string itemName;
    public float itemPrice;
    public float basePrice;
    public GameObject itemPrefab;
    public Sprite itemSprite;

    [Header("Restrictions")]
    public bool isAgeRestricted;
    public bool isStackable;
    public int stockCount;
    public int maxStock;

    public static Items Instance;

    void Awake()
    {
        Instance = this;
    }   

    public float GetFairPrice()
    {
        return itemPrice;
    }

    public void OnEnable()
    {
        itemPrice = basePrice;
    }
}