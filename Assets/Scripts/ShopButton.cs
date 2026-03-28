using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private Button button;
    public SoldierData unit;
    private TMP_Text buttonText;
    private bool isUpgrade = false;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (!isUpgrade)
        {
            Shop.Instance.BuyUnit(unit);
            buttonText.text = "Upgrade";
            isUpgrade = true;
        }
        else
        {
            Shop.Instance.OpenUpgradeTab(unit);
        }

        //transform.parent.gameObject.SetActive(false);
        //Destroy(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        // Good practice: remove the listener when this button is destroyed
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
