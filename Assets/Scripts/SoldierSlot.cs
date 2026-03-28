using UnityEngine;
using UnityEngine.UI;

public class SoldierSlot : MonoBehaviour
{
    public Image iconDisplay;
    public Image background;
    private SoldierData myData;
    bool isSetup = false;
    public float timer, spawnTime;

    private void Update()
    {
        if (isSetup)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else timer = 0;
            UpdateSlotColor();
        }
    }
    public void Setup(SoldierData data)
    {
        myData = data;
        iconDisplay.sprite = data.icon;
        timer = data.timeToSpawn;
        spawnTime = data.timeToSpawn;
        isSetup = true;
    }

    public void SetHighlight(bool isSelected)
    {
        background.color = isSelected ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1f);
    }

    public SoldierData GetData()
    {
        return myData;
    }

    private void UpdateSlotColor()
    {
        float progressPercentage = timer / spawnTime;
        float currentBrightness = Mathf.Lerp(1f, 0.2f, progressPercentage);
        iconDisplay.color = new Color(currentBrightness, currentBrightness, currentBrightness, 1f);
    }

    public void ResetTimer()
    {
        timer = spawnTime;
    }
}