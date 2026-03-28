using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<SoldierData> availableUnits;

    public Transform slotContainer;
    public GameObject slotPrefab;

    private List<SoldierSlot> spawnedSlots = new List<SoldierSlot>();

    private int currentIndex = 0;
    [SerializeField] string tagToAssign;

    void Start()
    {
        availableUnits = new List<SoldierData>(GameManager.Instance.selectedUnits);

        GenerateSlots();
        UpdateSelection();
    }

    void Update()
    {
        // Don't do anything if there are no soldiers
        if (spawnedSlots.Count == 0) return;

        //Controlling the arrow
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < 3.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -4.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            if (currentIndex >= spawnedSlots.Count) currentIndex = 0;

            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = spawnedSlots.Count - 1;

            UpdateSelection();
        }


        //Spawning a unit
        if (Input.GetKeyDown(KeyCode.Space) && spawnedSlots[currentIndex].timer <= 0)
        {
            SpawnUnit();
        }
    }

    void UpdateSelection()
    {
        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            bool isTheSelectedOne = (i == currentIndex);
            spawnedSlots[i].SetHighlight(isTheSelectedOne);
        }
    }

    void SpawnUnit()
    {
        SoldierData selectedData = spawnedSlots[currentIndex].GetData();
        GameObject newUnit = Instantiate(selectedData.prefab, transform.position, transform.rotation);
        for (int i = 0; i < spawnedSlots.Count; i++)
            spawnedSlots[i].ResetTimer();
        newUnit.tag = tagToAssign;
    }

    void GenerateSlots()
    {
        foreach (Transform child in slotContainer) Destroy(child.gameObject);
        spawnedSlots.Clear();

        foreach (SoldierData unit in availableUnits)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotContainer);
            SoldierSlot slotScript = newSlot.GetComponent<SoldierSlot>();

            if (slotScript != null)
            {
                slotScript.Setup(unit);
                spawnedSlots.Add(slotScript); // Add to our tracking list
            }
        }
    }
}
