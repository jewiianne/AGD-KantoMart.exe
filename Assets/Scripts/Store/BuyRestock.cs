using UnityEngine;

public class BuyRestock : MonoBehaviour
{   
    public GameObject PiatusPrefab;
    public void BuyPiatus()
    {
        if(MoneyManager.Instance.currentMoney >= 80)
        {
            MoneyManager.Instance.currentMoney -= 80;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnPiatus();
        }
    }

    public void SpawnPiatus()
    {
        Instantiate(PiatusPrefab, transform.position, Quaternion.identity);
    }  
}
