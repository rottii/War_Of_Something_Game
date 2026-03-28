using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    public GameObject UpgradePanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void BuyUnit(SoldierData unit)
    {
        GameManager.Instance.purchasedUnits.Add(unit);
    }

    public void OpenUpgradeTab(SoldierData unit)
    {
        UpgradePanel.SetActive(true);
    }

    public void UpgradeUnit(SoldierData unit)
    {

    }

    public void SelectUnit(SoldierData unit)
    {
        GameManager.Instance.selectedUnits.Add(unit);
    }

}
