using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const float FIXED_DAMAGE = 0.25f;

    public static GameManager instance;

    // Can put these in a GameUIHandler.cs script and reference the GameObject
    [Header("Canvas Stuff")]
    [SerializeField] private GameUIHandler gui;
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider reaperHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Card Objects")]
    [SerializeField] private GameObject cardGroup;
    private GameObject aiSelectedCard = null;
    private GameObject playerSelectedCard = null;

    [Header("AI Card Prefabs")]
    [SerializeField] private Transform[] aiCardPrefabs;

    private void Awake() 
    {
        instance = this;
    }

    public void SendToBoard(GameObject playerCard)
    {
        // AI picks a card
        aiSelectedCard = Instantiate(aiCardPrefabs[UnityEngine.Random.Range(0, 3)].gameObject);
        aiSelectedCard.transform.position = new Vector3(0f, 2.3f, 3f);
        CardToStatic(aiSelectedCard);

        // Pull out RPS enum from GameObjects
        RPS playerChoice = playerCard.GetComponent<CardHandler>().getChoice();
        RPS aiChoice = aiSelectedCard.GetComponent<CardHandler>().getChoice();

        // Transfer selected card, then drop rest
        playerSelectedCard = playerCard;
        playerSelectedCard.transform.parent.DetachChildren();
        CardToStatic(playerSelectedCard);
        cardGroup.GetComponent<CardGroup>().MoveRight();

        // Calculate
        Calculate(playerChoice, aiChoice);
        Invoke(nameof(RespawnCards), 3f);
    }

    private void RespawnCards()
    {
        Destroy(aiSelectedCard);
        Destroy(playerSelectedCard);
        cardGroup.GetComponent<CardGroup>().MoveLeft();
    }

    private void Calculate(RPS playerChoice, RPS aiChoice)
    {
        if (playerChoice == aiChoice) return;

        switch (playerChoice, aiChoice)
        {
            case (RPS.ROCK, RPS.PAPER):
            case (RPS.PAPER, RPS.SCISSORS):
            case (RPS.SCISSORS, RPS.ROCK):
                PlayerLose();
                break;
            case (RPS.MYSTERY, _):
                PlayerMystery();
                break;
            case (RPS.LIFE, _):
                PlayerAddLife();
                break;
            default:
                PlayerWin();
                return;
        }
    }

    private void CardToStatic(GameObject card)
    {
        card.GetComponent<Animator>().enabled = false;
        card.layer = LayerMask.NameToLayer("Default");
    }

    private void TakeDamage(Slider recipiant, float amount)
    {
        recipiant.value -= amount;
        Math.Round(recipiant.value, 2);
    }

#region Player Conditions
    private void PlayerWin()
    {
        TakeDamage(reaperHealth, FIXED_DAMAGE);
        FMODUnity.RuntimeManager.PlayOneShot("event:/ReaperLoss", GetComponent<Transform>().position);
        if (reaperHealth.value <= 0)
        {
            score++;
            scoreText.text = "Matches Survived : " + score;
            reaperHealth.value = 1;
        }
    }

    private void PlayerLose()
    {
        TakeDamage(playerHealth, FIXED_DAMAGE);
        FMODUnity.RuntimeManager.PlayOneShot("event:/HpLoss", GetComponent<Transform>().position);
        if (playerHealth.value <= 0)
        {
            gui.ShowGameOverMenu();
        }
    }
#endregion

#region Other Card Conditions
    private void PlayerMystery()
    {
        TakeDamage(reaperHealth, FIXED_DAMAGE*2);
        if (reaperHealth.value <= 0)
        {
            score++;
            scoreText.text = "Matches Survived : " + score;
            reaperHealth.value = 1;
        }
    }

    private void PlayerAddLife()
    {
        TakeDamage(playerHealth, -FIXED_DAMAGE);
    }
#endregion

    public float GetPlayerHealth()
    {
        return playerHealth.value;
    }
}
