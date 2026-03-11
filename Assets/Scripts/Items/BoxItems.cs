using UnityEngine;

[CreateAssetMenu(fileName = "BoxItems", menuName = "KantoMart/BoxItems")]

[System.Serializable]
public class BoxItems : ScriptableObject
{
    public Items itemData;
    public string boxItemName;
    public GameObject boxItemPrefab;
    public Sprite boxItemSprite;
    public int boxItemPrice;
    public int boxItemInsideCount = 5;
}
