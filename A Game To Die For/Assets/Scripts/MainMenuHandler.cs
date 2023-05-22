using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private Animator reaperAnim;

    public void StartButtonClicked()
    {
        StartCoroutine(nameof(LoadNextScene));
        reaperAnim.SetTrigger("Move");
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
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
    }
}
