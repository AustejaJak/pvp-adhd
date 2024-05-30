using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        var savePath = Path.Combine(Application.persistentDataPath, "userdata.txt");

        if (!File.Exists(savePath))
        {
            CreateSaveFile(savePath);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SwitchScene()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CreateSaveFile(string savePath)
    {
        File.WriteAllText(savePath, "");
    }
}