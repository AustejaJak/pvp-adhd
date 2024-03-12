using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
