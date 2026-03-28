using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int score { get; private set; } = 50;

    public List<SoldierData> purchasedUnits = new List<SoldierData>();
    public List<SoldierData> selectedUnits = new List<SoldierData>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScore(int amount)
    {
        score += amount;
    }
    public void GameOver()
    {
        if ( score <= 0)
        {
            System.Console.WriteLine("YOU LOST");
        }
        else if (score >= 100)
        {
            System.Console.WriteLine("YOU WON");
        }
    }
}
