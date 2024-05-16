using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Collections;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public DatabaseManager DBManager;
    
    public GameObject errorMessage;

    private void Start()
    {
    }

    public void Register()
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

        if(DBManager.RegisterUser(username, password))
        {  
            gameObject.SetActive(false);
            errorMessage.SetActive(false);
            Debug.Log("User registered: " + username);
        }
        else
        {
            errorMessage.SetActive(true);
        }
    }
}
