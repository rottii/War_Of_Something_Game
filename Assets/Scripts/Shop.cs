using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    public GameObject UpgradePanelUI;

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
        UpgradePanelUI.SetActive(true);
        UpgradePanel upgradePanelScript = UpgradePanelUI.GetComponent<UpgradePanel>();
        upgradePanelScript.SetUpPanel(unit);
        //Upgrading happens in UpgradeSlot.cs    
    }

    public void SelectUnit(SoldierData unit)
    {
        GameManager.Instance.selectedUnits.Add(unit);
    }

}
