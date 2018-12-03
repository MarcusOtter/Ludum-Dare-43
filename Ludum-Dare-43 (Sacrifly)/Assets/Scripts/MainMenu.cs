using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleTutorial()
    {
        _tutorial.SetActive(!_tutorial.activeInHierarchy);
    }
}
