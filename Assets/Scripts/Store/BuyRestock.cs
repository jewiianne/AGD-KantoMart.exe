using UnityEngine;

public class BuyRestock : MonoBehaviour
{   
    public GameObject blueChipsPrefab;
    public GameObject redChipsPrefab;
    public GameObject redBiscuitsPrefab;
    public GameObject yellowBiscuisPrefab;
    public GameObject cigarettesPrefab;

    public void BuyBlueChips()
    {
        if(MoneyManager.Instance.currentMoney >= 80)
        {
            MoneyManager.Instance.currentMoney -= 80;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnBlueChips();
        }
    }

    public void SpawnBlueChips()
    {
        Instantiate(blueChipsPrefab, transform.position, Quaternion.identity);
    } 

    public void BuyRedChips()
    {
        if(MoneyManager.Instance.currentMoney >= 80)
        {
            MoneyManager.Instance.currentMoney -= 80;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnRedChips();
        }
    }

    public void SpawnRedChips()
    {
        Instantiate(redChipsPrefab, transform.position, Quaternion.identity);
    } 

    public void BuyRedBiscuits()
    {
        if(MoneyManager.Instance.currentMoney >= 60)
        {
            MoneyManager.Instance.currentMoney -= 60;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnRedBiscuits();
        }
    }

    public void SpawnRedBiscuits()
    {
        Instantiate(redBiscuitsPrefab, transform.position, Quaternion.identity);
    } 

    public void BuyYellowBiscuits()
    {
        if(MoneyManager.Instance.currentMoney >= 60)
        {
            MoneyManager.Instance.currentMoney -= 60;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnYellowBiscuits();
        }
    }

    public void SpawnYellowBiscuits()
    {
        Instantiate(yellowBiscuisPrefab, transform.position, Quaternion.identity);
    } 

    public void BuyCigarettes()
    {
        if(MoneyManager.Instance.currentMoney >= 300)
        {
            MoneyManager.Instance.currentMoney -= 300;
            MoneyManager.Instance.MoneyText.text = MoneyManager.Instance.currentMoney.ToString();

            SpawnCigarettes();
        }
    }

    public void SpawnCigarettes()
    {
        Instantiate(cigarettesPrefab, transform.position, Quaternion.identity);
    } 
}
