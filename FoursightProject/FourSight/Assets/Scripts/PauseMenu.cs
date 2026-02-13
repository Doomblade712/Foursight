using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject pauseMenuPanel;
    public GameObject optionsMenuPanel;

    [Header("Settings")]
    public KeyCode pauseKey = KeyCode.Escape;

    private bool isPaused = false;
    private OptionsMenu optionsMenu;

    void Start()
    {
        if (optionsMenuPanel)
        {
            optionsMenu = optionsMenuPanel.GetComponent<OptionsMenu>();
        }

        if (pauseMenuPanel)
            pauseMenuPanel.SetActive(false);

        if (optionsMenuPanel)
            optionsMenuPanel.SetActive(false);
    }

    void Update()
    {
       
    }

    public void togglePaused(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            if (optionsMenuPanel && optionsMenuPanel.activeSelf)
            {
                BackToPauseMenu();
            }
            else
            {
                Resume();
            }
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (pauseMenuPanel)
            pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f; 
        isPaused = true;


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        if (pauseMenuPanel)
            pauseMenuPanel.SetActive(false);

        if (optionsMenuPanel)
            optionsMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

    }

    public void OpenOptions()
    {
        if (pauseMenuPanel)
            pauseMenuPanel.SetActive(false);

        if (optionsMenuPanel)
        {
            optionsMenuPanel.SetActive(true);

            if (optionsMenu)
                optionsMenu.OpenOptionsMenu();
        }
    }

    public void BackToPauseMenu()
    {
        if (optionsMenuPanel)
        {
            optionsMenuPanel.SetActive(false);


            if (optionsMenu)
                optionsMenu.SaveSettings();
        }

        if (pauseMenuPanel)
            pauseMenuPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Getter for other scripts
    public bool IsPaused()
    {
        return isPaused;
    }
}