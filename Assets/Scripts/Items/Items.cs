using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "KantoMart/Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public float itemPrice;
    public Sprite itemSprite;
    public GameObject items;

    [Header("Restrictions")]
    public bool isRestricted;
    public int stockCount;
    public int maxStock;
}