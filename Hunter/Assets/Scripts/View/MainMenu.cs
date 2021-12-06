using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Slider[] _sliders;
    private Button _playButton;
    private Button _quitButton;

    public void Start()
    {
        _sliders = FindObjectsOfType<Slider>();
        foreach (var slider in _sliders)
        {
            slider.onValueChanged.AddListener(delegate { OnSliderChanged(slider); });
        }

        _playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        _playButton.onClick.AddListener(delegate { OnPlayButtonClick(); });

        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        _quitButton.onClick.AddListener(delegate { OnQuitButtonClick(); });
    }

    public void OnSliderChanged(Slider slider)
    {
        Text text = slider.GetComponentInChildren<Text>();
        text.text = slider.value.ToString();
    }

    public void OnPlayButtonClick()
    {
        foreach (var slider in _sliders)
        {
            string name = slider.name.Substring(0, slider.name.Length - 6);
            EntityFactory.AnimalsNumber.Add(name, (int)slider.value);
        }

        SceneManager.LoadScene("MainScene");
    }


    public void OnQuitButtonClick()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
