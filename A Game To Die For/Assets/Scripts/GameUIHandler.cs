using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameoverMenu;
    public static event Action gameover;

    public void ShowGameOverMenu()
    {
        gameover();
        gameoverMenu.SetActive(true);
    }

    public void PlayAgainButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
