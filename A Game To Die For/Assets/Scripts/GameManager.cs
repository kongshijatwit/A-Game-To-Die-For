using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health Bars")]
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider reaperHealth;
    private const float FIXED_DAMAGE = 0.25f;

    [Header("Card")]
    [SerializeField] private GameObject cardGroup;

    private void Awake() 
    {
        instance = this;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            cardGroup.SetActive(true);
        }
    }

    public void SendToBoard(RPS playerChoice)
    {
        // Do AI things
        RPS aiChoice = (RPS)UnityEngine.Random.Range(0, 3);

        // Calculate
        Debug.Log("Player picked: " + playerChoice + " / AI picked: " + aiChoice);
        Calculate(playerChoice, aiChoice);

        cardGroup.SetActive(false);
    }

    private void Calculate(RPS playerChoice, RPS aiChoice)
    {
        if (playerChoice == aiChoice) 
        { 
            PlayerDraw();
            return;
        }

        switch (playerChoice)
        {
            case RPS.MYSTERY:
                RandomCard();
                return;

            case RPS.LIFE:
                PlayerAddLife();
                return;
        }

        switch (playerChoice, aiChoice)
        {
            case (RPS.ROCK, RPS.PAPER):
                PlayerLose();
                break;
            case (RPS.PAPER, RPS.SCISSORS):
                PlayerLose();
                break;
            case (RPS.SCISSORS, RPS.ROCK):
                PlayerLose();
                break;
            default:
                PlayerWin();
                return;
        }
    }

    private void PlayerWin()
    {
        Debug.Log("PlayerWin");
        TakeDamage(reaperHealth, FIXED_DAMAGE);
    }

    private void PlayerLose()
    {
        Debug.Log("PlayerLose");
        TakeDamage(playerHealth, FIXED_DAMAGE);
    }

    private void PlayerDraw()
    {
        Debug.Log("PlayerDraw");
    }

    private void PlayerAddLife()
    {
        Debug.Log("PlayerAddLife");
    }

    private void RandomCard()
    {
        Debug.Log("RandomCard");
    }

    private void TakeDamage(Slider recipiant, float amount)
    {
        recipiant.value -= amount;
        Math.Round(recipiant.value, 2);
    }
}
