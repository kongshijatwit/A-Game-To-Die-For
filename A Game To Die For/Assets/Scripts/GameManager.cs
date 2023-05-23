using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Canvas Stuff")]
    [SerializeField] private GameUIHandler gui;
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider reaperHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private const float FIXED_DAMAGE = 0.25f;

    [Header("Card Objects")]
    [SerializeField] private GameObject cardGroup;
    [SerializeField] private GameObject selectedCardsGroup;
    [SerializeField] private GameObject aiCardSelected;
    private RandomCardPicker randomCardPicker;

    [Header("AI Card Prefabs")]
    [SerializeField] private Transform[] aiCardPrefabs;

    private void Awake() 
    {
        instance = this;
        randomCardPicker = new RandomCardPicker(aiCardPrefabs);
    }

    public void SendToBoard(GameObject playerCard)
    {
        // AI picks a card
        GameObject aiCard = Instantiate(aiCardPrefabs[UnityEngine.Random.Range(0, 3)].gameObject, aiCardSelected.transform);
        aiCard.GetComponent<Animator>().enabled = false;
        aiCard.layer = LayerMask.NameToLayer("Default");

        // Pull out RPS enum from GameObjects
        RPS playerChoice = playerCard.GetComponent<CardHandler>().getChoice();
        RPS aiChoice = aiCard.GetComponent<CardHandler>().getChoice();

        // Transfer selected card, then drop rest
        playerCard.GetComponent<Animator>().enabled = false;
        playerCard.layer = LayerMask.NameToLayer("Default");
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
        foreach (Transform child in selectedCardsGroup.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in aiCardSelected.transform)
        {
            Destroy(child.gameObject);
        }
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

#region Player Conditions
    private void PlayerWin()
    {
        Debug.Log("PlayerWin");
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
        Debug.Log("PlayerLose");
        TakeDamage(playerHealth, FIXED_DAMAGE);
        FMODUnity.RuntimeManager.PlayOneShot("event:/HpLoss", GetComponent<Transform>().position);
        if (playerHealth.value <= 0)
        {
            gui.ShowGameOverMenu();
        }
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
        TakeDamage(playerHealth, -FIXED_DAMAGE);
    }
#endregion
}
