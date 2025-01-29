using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UIButtonManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_CallStartGame RSE_CallStartGame;
    [SerializeField] RSE_CallRestartGame RSE_CallRestartGame;
    [SerializeField] RSE_CallMainMenu RSE_CallMainMenu;
    [SerializeField] RSE_CallQuitGame RSE_CallQuitGame;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallStartGame.action += StartGame;
        RSE_CallRestartGame.action += RestartGame;
        RSE_CallMainMenu.action += MainMenu;
        RSE_CallQuitGame.action += QuitGame;
    }
    private void OnDisable()
    {
        RSE_CallStartGame.action -= StartGame;
        RSE_CallRestartGame.action -= RestartGame;
        RSE_CallMainMenu.action -= MainMenu;
        RSE_CallQuitGame.action -= QuitGame;
    }
    private void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    private void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    private void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}