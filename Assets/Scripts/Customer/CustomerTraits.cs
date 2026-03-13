using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

[CreateAssetMenu(fileName = "New Customer", menuName = "KantoMart/Customer")]
public class CustomerTraits : ScriptableObject
{
    public static CustomerTraits Instance;
    public string customerName;
    public List<GameObject> customerPrefabs;

    public enum CustomerType {New, Casual, Loyal, Student, Tambay, Marites};
    public CustomerType customerType;

    [Header("Traits")]
    public bool canUtang;
    public bool isTroubleMaker;

    [Header("Reputation")]
    public int reputationPenalty;

    void Awake()
    {
        Instance = this;
    }
}
