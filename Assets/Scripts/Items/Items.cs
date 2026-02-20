using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "KantoMart/Item")]
public class Items : ScriptableObject
{
    public string ItemName;
    public float ItemPrice;
    public Sprite itemSprite;

    [Header("Restrictions")]
    public bool isRestricted;
    public int stockCount;
    public int maxStock;
}