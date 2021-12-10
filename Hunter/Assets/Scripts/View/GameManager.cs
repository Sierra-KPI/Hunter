using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string EndText = "GAME OVER!";
    private string _winningMessage = "YOU WIN!";
    private string _loosingMessage = "YOU LOSE!";

    private string _startSceneName = "MainMenu";
    private string _endSceneName = "EndScene";
    public bool isPaused = false;

    private static GameObject _pauseMenu;

    public void LoadWinningGameEnd()
    {
        EndText = _winningMessage;
        SceneManager.LoadScene(_endSceneName);
    }

    public void LoadLoosingGameEnd()
    {
        EndText = _loosingMessage;
        SceneManager.LoadScene(_endSceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_startSceneName);
    }

    public void LoadPauseMenu()
    {
        isPaused = !isPaused;
        _pauseMenu.SetActive(isPaused);
    }

    public void SetPauseMenu()
    {
        _pauseMenu = GameObject.Find("PauseMenu");

        var _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { QuitButton(); });

        var _resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        _resumeButton.onClick.AddListener(delegate { LoadPauseMenu(); });

        var _mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(delegate { LoadMainMenu(); });

        _pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void QuitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}

