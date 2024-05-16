using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRegisterMenu : MonoBehaviour
{
    public GameObject LoginButton;
    public GameObject RegisterButton;
    public GameObject LogoutButton;
    void Start()
    {
        
    }

    private void OnEnable() {
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            LogoutButton.SetActive(true);
            RegisterButton.SetActive(false);
            LoginButton.SetActive(false);
        }
        else{
            LogoutButton.SetActive(false);
            RegisterButton.SetActive(true);
            LoginButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("PlayerID");
    }
}
