using UnityEngine;
using System.Collections.Generic;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance { get; private set; }

    public List<GameObject>[] lanes = new List<GameObject>[9];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i] = new List<GameObject>();
        }
    }

    public void AddUnitToLane(int laneIndex, GameObject unit)
    {
        lanes[laneIndex].Add(unit);
    }

    public void RemoveUnitFromLane(int laneIndex, GameObject unit)
    {
        lanes[laneIndex].Remove(unit);
    }

    public bool DoesLaneHaveTag(int laneIndex, string tag)
    {
        foreach (GameObject unit in lanes[laneIndex])
        {
            if (unit != null)
            {
                if (unit.CompareTag(tag)) return true;
            }
        }
        return false;
    }
}
