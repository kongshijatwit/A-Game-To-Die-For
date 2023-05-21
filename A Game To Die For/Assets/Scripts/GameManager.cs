using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Slider playerHealth;
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
        cardGroup.SetActive(false);

        // Do AI things
        RPS aiChoice = (RPS)Random.Range(0, 3);

        // Calculate
        Debug.Log("Player picked: " + playerChoice + " / AI picked: " + aiChoice);

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
    }

    private void PlayerDraw()
    {
        Debug.Log("PlayerDraw");
    }

    private void PlayerLose()
    {
        Debug.Log("PlayerLose");
    }

    private void PlayerAddLife()
    {
        Debug.Log("PlayerAddLife");
    }

    private void RandomCard()
    {
        Debug.Log("RandomCard");
    }
}
