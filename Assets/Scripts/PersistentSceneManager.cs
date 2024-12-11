using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneManager : MonoBehaviour
{
    private static PersistentSceneManager instance;

    public static PersistentSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("PersistentSceneManager instance is null. Make sure it is present in the scene.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of the SceneManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    /// <summary>
    /// Loads a scene by name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads a scene by its build index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene in the build settings.</param>
    public void LoadScene(int sceneIndex)
    {
        Debug.Log("Loadscene" + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
