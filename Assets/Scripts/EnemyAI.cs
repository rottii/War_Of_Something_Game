using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public List<SoldierData> availableUnits;

    public Transform slotContainer;
    public GameObject slotPrefab;

    private List<SoldierSlot> spawnedSlots = new List<SoldierSlot>();

    private int slotIndex;
    [SerializeField] string tagToAssign;

    //AI stuff
    public float[] laneYPositions = {3.5f, 2.5f, 1.5f, 0.5f, -0.5f, -1.5f, -2.5f, -3.5f, -4.5f};
    List<int> threatenedLanes = new List<int>();
    [Range(0f, 1f)] public float spawnChance = 0.95f; //95% spawns in the lane which player has units
    void Start()
    {
        GenerateSlots();
        UpdateSelection();
    }

    void Update()
    {
        // Don't do anything if there are no soldiers
        if (spawnedSlots.Count == 0) return;

        if (spawnedSlots[slotIndex].timer <= 0)
        {
            SpawnUnit();
            UpdateSelection();
        }
    }

    void SpawnUnit()
    {
        SoldierData selectedData = spawnedSlots[slotIndex].GetData();
        GameObject newUnit = Instantiate(selectedData.prefab, transform.position, transform.rotation);
        for (int i = 0; i < spawnedSlots.Count; i++)
            spawnedSlots[i].ResetTimer();
        newUnit.tag = tagToAssign;
    }

    void UpdateSelection()
    {
        slotIndex = Random.Range(0, spawnedSlots.Count);
        int chosenLaneIndex = -1;

        threatenedLanes.Clear();

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            bool isTheSelectedOne = (i == slotIndex);
            spawnedSlots[i].SetHighlight(isTheSelectedOne);
        }

        for (int i = 0; i < laneYPositions.Length; i++)
        {
            bool hasPlayer = LaneManager.Instance.DoesLaneHaveTag(i, "Player");
            bool hasEnemy = LaneManager.Instance.DoesLaneHaveTag(i, "Enemy");

            if (hasPlayer && !hasEnemy)
            {
                threatenedLanes.Add(i);
            }
        }
        if (threatenedLanes.Count > 0)
        {
            float diceRoll = Random.value;

            if (diceRoll <= spawnChance)
            {
                int randomTarget = Random.Range(0, threatenedLanes.Count);
                chosenLaneIndex = threatenedLanes[randomTarget];
            }
            else
            {
                chosenLaneIndex = Random.Range(0, laneYPositions.Length);
            }
        }
        else
        {
            chosenLaneIndex = Random.Range(0, laneYPositions.Length);
        }

        float spawnY = laneYPositions[chosenLaneIndex];

        transform.position = new Vector3(transform.position.x, spawnY, 1);

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
/*
1.Change highlight mechanic - done
2.Make the ai spawn units based on player's unit positions - done
 */


