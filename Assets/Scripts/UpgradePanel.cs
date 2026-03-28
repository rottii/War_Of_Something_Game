using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TMP_Text unitName;
    [SerializeField] TMP_Text upgradeCostText;

    [SerializeField] Image unitImage;

    private int upgradeCost;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpPanel(SoldierData unit)
    {
        unitName.text = unit.name;
        unitImage.sprite = unit.icon;
    }
}
