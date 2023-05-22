using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool pauseMenuActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenuActive);
            pauseMenuActive = !pauseMenuActive;
        }
    }
}
