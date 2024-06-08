using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the next scene (assuming it's named "GameScene")
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        #if UNITY_EDITOR
        // If running in the editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpenOptions()
    {
        // Load the options scene or display options UI
        // SceneManager.LoadScene("OptionsScene");
        Debug.Log("Options Clicked");
    }
}
