using System;
using System.Collections;
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
    [SerializeField] private GameObject aiCardSelected;

    [Header("AI Card Prefabs")]
    [SerializeField] private GameObject[] aiCardPrefabs;

    private void Awake() 
    {
        instance = this;
    }

    public void SendToBoard(GameObject playerCard)
    {
        // AI picks a card
        Transform aiCard = Instantiate(aiCardPrefabs[UnityEngine.Random.Range(0, 3)].transform, aiCardSelected.transform);
        aiCard.GetComponent<Animator>().enabled = false;
        aiCard.gameObject.layer = LayerMask.NameToLayer("Default");

        // Pull out RPS enum from GameObjects
        RPS playerChoice = playerCard.GetComponent<CardHandler>().getChoice();
        RPS aiChoice = aiCard.GetComponent<CardHandler>().getChoice();

        // Transfer selected card, then drop rest
        playerCard.transform.parent = selectedCardsGroup.transform;
        cardGroup.GetComponent<CardGroup>().MoveRight();

        // Calculate
        Debug.Log("Player picked: " + playerChoice + " / AI picked: " + aiChoice);
        Calculate(playerChoice, aiChoice);

        StartCoroutine(nameof(WaitForResults));
    }

    private IEnumerator WaitForResults()
    {
        yield return new WaitForSeconds(4f);
        Destroy(selectedCardsGroup.transform.GetChild(0).gameObject);
        Destroy(aiCardSelected.transform.GetChild(0).gameObject);
        cardGroup.GetComponent<CardGroup>().MoveLeft();
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
