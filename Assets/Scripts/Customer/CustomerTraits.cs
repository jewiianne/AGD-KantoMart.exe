using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Customer", menuName = "KantoMart/Customer")]
public class CustomerTraits : ScriptableObject
{
    public string customerName;
    public List<GameObject> customerPrefabs;

    public enum CustomerType {New, Casual, Loyal, Student, Tambay, Marites};
    public CustomerType customerType;

    [Header("Traits")]
    public bool canUtang;
    public bool isTroubleMaker;
}
