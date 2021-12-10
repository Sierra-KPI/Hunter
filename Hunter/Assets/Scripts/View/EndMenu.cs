using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    private Button _playButton;
    private Button _quitButton;
    private Text _text;

    private string _startSceneName = "MainMenu";

    public void Start()
    {
        _text = GameObject.Find("GameText").GetComponent<Text>();
        _text.text = GameManager.EndText;

        _playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        _playButton.onClick.AddListener(delegate { OnPlayButtonClick(); });

        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { OnQuitButtonClick(); });
    }


    public void OnPlayButtonClick()
    {

        SceneManager.LoadScene(_startSceneName);
    }


    public void OnQuitButtonClick()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
