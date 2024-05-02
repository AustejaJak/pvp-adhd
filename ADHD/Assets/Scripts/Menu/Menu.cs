using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SwitchScene(string sceneName)
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(sceneName);
    }
}