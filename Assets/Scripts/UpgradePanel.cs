using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TMP_Text unitName;
    [SerializeField] Image unitImage;

    public Transform upgradablesContainer;
    public GameObject upgradeSlot;

    public void SetUpPanel(SoldierData unit)
    {
        unitName.text = unit.soldierName;
        unitImage.sprite = unit.icon;

        //Change this into recycling 
        foreach (Transform child in upgradablesContainer)
        {
            Destroy(child.gameObject);
        }

        //Spawns speed, health etc. values and upgrading button
        foreach (UpgradableUnitStats stat in unit.unitStats)
        {
            GameObject newSlotObj = Instantiate(upgradeSlot, upgradablesContainer);

            UpgradeSlot slot = newSlotObj.GetComponent<UpgradeSlot>();

            slot.SetupSlot(unit.name, stat);
        }
    }
}
