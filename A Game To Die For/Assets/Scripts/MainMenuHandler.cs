using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject reaper;

    public void StartButtonClicked()
    {
        StartCoroutine(nameof(LoadNextScene));
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click", GetComponent<Transform>().position);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click", GetComponent<Transform>().position);
    }

    private IEnumerator LoadNextScene()
    {
        mainMenuContainer.SetActive(false);
        reaper.transform.Translate(new Vector3(0, 0, 1f), Space.World);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
    }
}
