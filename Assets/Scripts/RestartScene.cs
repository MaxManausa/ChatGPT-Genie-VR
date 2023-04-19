using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartScene : MonoBehaviour
{
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
