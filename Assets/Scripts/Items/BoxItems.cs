using UnityEngine;

[CreateAssetMenu(fileName = "BoxItems", menuName = "KantoMart/BoxItems")]

[System.Serializable]
public class BoxItems : ScriptableObject
{
    public Items itemData;
    public string boxItemName;
    public GameObject boxItemPrefab;
    public Sprite boxItemSprite;
    public float boxItemPrice;
    public float boxBasePrice;
    public int boxItemInsideCount = 5;
    public static BoxItems Instance;

    void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (boxBasePrice > 0)
        {
            boxItemPrice = boxBasePrice;
        }
    }
}
