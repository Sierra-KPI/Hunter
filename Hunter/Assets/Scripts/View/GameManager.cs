using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static string EndText = "GAME OVER!";
    private static string _winningMessage = "YOU WIN!";
    private static string _loosingMessage = "YOU LOSE!";

    private static string _endSceneName = "EndScene";


    public static void LoadWinningGameEnd()
    {
        EndText = _winningMessage;
        SceneManager.LoadScene(_endSceneName);
    }

    public static void LoadLoosingGameEnd()
    {
        EndText = _loosingMessage;
        SceneManager.LoadScene(_endSceneName);
    }

}

