using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LoginRegisterMenu : MonoBehaviour
{
    public GameObject LoginButton;
    public GameObject RegisterButton;
    public GameObject LogoutButton;
    public GameObject TableRowPrefab; // Prefab for a table row
    public Transform TableContainer; // Parent transform for the table rows
    public DatabaseManager DatabaseManager;
    public TextMeshProUGUI UsernameText; // UI Text component for displaying the username

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            LogoutButton.SetActive(true);
            RegisterButton.SetActive(false);
            LoginButton.SetActive(false);

            int playerId = PlayerPrefs.GetInt("PlayerID");
            UsernameText.text = DatabaseManager.GetPlayerUsername(playerId);

            List<ScoreSum> scoreSums = DatabaseManager.GetPlayerScoreSum(playerId);

            // Clear the table
            foreach (Transform child in TableContainer)
            {
                Destroy(child.gameObject);
            }

            // Add a new row for each score
            foreach (ScoreSum scoreSum in scoreSums)
            {
                GameObject tableRow = Instantiate(TableRowPrefab, TableContainer);
                TextMeshProUGUI scoreText = tableRow.transform.Find("Score").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI dateText = tableRow.transform.Find("Date").GetComponent<TextMeshProUGUI>();
                scoreText.text = "Score: " + scoreSum.TotalScore;
                dateText.text = "" + scoreSum.Date.ToString("yyyy-MM-dd");;
            }
        }
        else
        {
            LogoutButton.SetActive(false);
            RegisterButton.SetActive(true);
            LoginButton.SetActive(true);
        }
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("PlayerID");
    }
}