using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

            //DataTable scores = DatabaseManager.GetPlayerScores(playerId);

            // Clear the table
            foreach (Transform child in TableContainer)
            {
                Destroy(child.gameObject);
            }

            // Add a new row for each score
            /*foreach (DataRow row in scores.Rows)
            {
                GameObject tableRow = Instantiate(TableRowPrefab, TableContainer);
                Text scoreText = tableRow.GetComponentInChildren<Text>();
                scoreText.text = "Points: " + row["points"];
            }*/
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