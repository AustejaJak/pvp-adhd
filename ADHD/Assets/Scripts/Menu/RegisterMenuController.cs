using UnityEngine;
using TMPro;
using System;
using System.IO;

public class UserAuthenticator : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public GameObject button;

    private string savePath;
    private bool isLoggedIn = false;
    private string loggedInUsername = "";

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "userdata.txt");
        Debug.Log("Save file created at: " + savePath);
        isLoggedIn = false;
        SetUIForLoggedOut();
    }

    public void ToggleLogin()
    {
        if (isLoggedIn)
        {
            Logout();
        }
        else
        {
            Login();
        }
    }

    private void Login()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        bool empty = false;
        if (string.IsNullOrEmpty(username))
        {
            usernameField.placeholder.GetComponent<TMP_Text>().text = "Please enter a username";
            empty = true;
        }
        if (string.IsNullOrEmpty(password))
        {
            passwordField.placeholder.GetComponent<TMP_Text>().text = "Please enter a password";
            empty = true;
        }
        if (empty) return;


        if (!IsUserRegistered(username, password))
        {
            RegisterUser(username, password);
        }

        isLoggedIn = true;
        loggedInUsername = username;
        SetUIForLoggedIn();
    }

    private void Logout()
    {
        isLoggedIn = false;
        loggedInUsername = "";
        SetUIForLoggedOut();
    }

    private void SetUIForLoggedIn()
    {
        usernameField.interactable = false;
        passwordField.text = "******";
        passwordField.interactable = false;
        button.GetComponentInChildren<TMP_Text>().text = "Logout";
    }

    private void SetUIForLoggedOut()
    {
        usernameField.interactable = true;
        passwordField.interactable = true;
        button.GetComponentInChildren<TMP_Text>().text = "Login/Register";
    }

    private bool IsUserRegistered(string username, string password)
    {
        if (File.Exists(savePath))
        {
            string[] lines = File.ReadAllLines(savePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && parts[0].Trim().Equals(username) && parts[1].Trim().Equals(password))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void RegisterUser(string username, string password)
    {
        string userData = $"{username}:{password}";
        using (StreamWriter writer = File.AppendText(savePath))
        {
            writer.WriteLine(userData);
        }
    }
}
