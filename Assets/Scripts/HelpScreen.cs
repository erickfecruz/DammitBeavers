using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreen : MonoBehaviour
{
    public GameObject Main;
    public GameObject HelpPanel;

    void Awake() => Back();

    public void StartGame()
    {
        SceneManager.LoadScene("PrimeiraFase");
    }

    public void Help()
    {
        Main.SetActive(false);
        HelpPanel.SetActive(true);
    }

    public void Exit() => Application.Quit();

    public void Back()
    {
        HelpPanel.SetActive(false);
        Main.SetActive(true);
    }

}
