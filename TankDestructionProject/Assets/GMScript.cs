using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GMScript : MonoBehaviour{

    [SerializeField]
    Text clock;

    [SerializeField]
    float RoundLimit;

    [SerializeField]
    Text P1HP;

    [SerializeField]
    float PlayerOneHealth;

    [SerializeField]
    Text Score;

    [SerializeField]
    float BuildingsToDestroy;

    public float destroyedBuildings;

    [SerializeField]
    Text Results;

    bool P1Victory;

    bool P2Victory;

    void Start ()
    {
        InvokeRepeating("GameClock", 0, 1);
        P1Victory = false;
        P2Victory = false;
	}
	
	void Update ()
    {
        UpdatePlayerHealth();
        UpdateBuildingDestructionProgress();
        UpdateResults();
	}

    void UpdateResults()
    {
        if (P1Victory)
        {
            Results.text = "PLAYER 1 WINS!";
        }

        if (P2Victory)
        {
            Results.text = "PLAYER 2 WINS!";
        }
    }

    void UpdateBuildingDestructionProgress()
    {
        Score.text = "Buildings Razed: " + destroyedBuildings;

        if (destroyedBuildings >= BuildingsToDestroy)
        {
            P1Victory = true;
        }
    }

    void UpdatePlayerHealth()
    {
        P1HP.text = "Armor: " + PlayerOneHealth;

        if (PlayerOneHealth <= 0)
        {
            P1HP.text = "DESTROYED";
            P2Victory = true;
        }
    }

    void GameClock()
    {
        if (!P1Victory && !P2Victory)
        {
            RoundLimit--;
            clock.text = "Time Remaining: " + RoundLimit;

            if (RoundLimit <= 0)
            {
                clock.text = "TIME'S UP!";
            }
        }
        else
        {
            clock.text = "Time Remaining: " + RoundLimit;
        }
    }
}
