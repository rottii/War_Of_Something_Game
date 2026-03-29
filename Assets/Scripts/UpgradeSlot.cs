using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeSlot : MonoBehaviour
{
    public TextMeshProUGUI statNameText;
    public TextMeshProUGUI statUpgradeCostText;

    [SerializeField] private Button upgradeButton;

    public void SetupSlot(string unitName, UpgradableUnitStats statData)
    {
        string statID = unitName + statData.statID;//Adding unit name so it's unique
        int currentLevel = PlayerPrefs.GetInt(statID, 1);//1 is the default level

        statNameText.text = statData.displayName + ": "
                  + (statData.baseValue + statData.statIncreasePerLevel * currentLevel).ToString() + "(+"
                  + statData.statIncreasePerLevel.ToString() + ")";

        statUpgradeCostText.text = "$" + (statData.baseCost + statData.costIncreasePerLevel * currentLevel).ToString();

        //Upgrading the stat
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt(statID, currentLevel + 1);
            PlayerPrefs.Save();//Just to be sure it saves 

            SetupSlot(unitName, statData);
        });
    }
}
