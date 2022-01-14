using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public enum SceneName { Menu, Gameplay }

    private string CurrentScene => SceneManager.GetActiveScene().name;

    public void ReloadScene() => SceneManager.LoadSceneAsync(CurrentScene);
    public void LoadScene(SceneName name) => SceneManager.LoadSceneAsync(name.ToString());
}