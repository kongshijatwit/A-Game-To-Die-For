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

    [Header("Card Objects")]
    [SerializeField] private GameObject cardGroup;
    [SerializeField] private GameObject selectedCardsGroup;

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

    public void SendToBoard(GameObject playerCard)
    {
        // Pull out RPS selection from GameObject
        RPS playerChoice = playerCard.GetComponent<CardHandler>().getChoice();
        RPS aiChoice = (RPS)UnityEngine.Random.Range(0, 3);

        // Calculate
        Debug.Log("Player picked: " + playerChoice + " / AI picked: " + aiChoice);
        Calculate(playerChoice, aiChoice);

        // Transfer selected card, then drop rest
        SelectionParentTransfer(playerCard.transform);
    }

    private void TakeDamage(Slider recipiant, float amount)
    {
        recipiant.value -= amount;
        Math.Round(recipiant.value, 2);
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

    private void SelectionParentTransfer(Transform child)
    {
        child.transform.parent = selectedCardsGroup.transform;
        foreach (Transform t in cardGroup.transform)
        {
            t.position = new Vector3(t.position.x, 0, t.position.y);
            t.GetComponent<CardHandler>().enabled = false;
        }
        //cardGroup.GetComponent<CardGroup>().MoveDown();
    }

#region 
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
#endregion

#region Other Card Conditions
    private void PlayerAddLife()
    {
        Debug.Log("PlayerAddLife");
    }

    private void RandomCard()
    {
        Debug.Log("RandomCard");
    }
#endregion
    
}
