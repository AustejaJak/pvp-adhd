using UnityEngine;
using System;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Data;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString;
    private MySqlConnection connection;

    private void Awake()
    {
        connectionString = "server=localhost;database=unity;user=root;password=root";
        connection = new MySqlConnection(connectionString);
    }

    private void OpenConnection()
    {
        if (connection.State == ConnectionState.Closed)
            connection.Open();
    }

    private void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
            connection.Close();
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool RegisterUser(string username, string password)
    {
        try
        {
            OpenConnection();

            // Hash the password
            string hashedPassword = HashPassword(password);

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO players (username, hash) VALUES (@username, @password)";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Registration successful
                    return true;
                }
                else
                {
                    // No rows affected, registration failed
                    Debug.Log("Error registering user: No rows affected");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error registering user: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool LoginUser(string username, string password)
    {
        try
        {
            OpenConnection();

            // Hash the password
            string hashedPassword = HashPassword(password);

            using (MySqlCommand cmd = connection.CreateCommand())
        {
            cmd.CommandText = "SELECT id FROM players WHERE username = @username AND hash = @password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int playerId = Convert.ToInt32(result);

                // Save player ID to PlayerPrefs
                PlayerPrefs.SetInt("PlayerID", playerId);
                PlayerPrefs.Save();

                Debug.Log("Login successful. Player ID: " + playerId);
                return true;
            }
            else
            {
                Debug.Log("Login failed: Invalid username or password");
                return false;
            }
        }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error logging in: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool AddPoints(int userId, int points)
    {
        try
        {
            OpenConnection();

            // Get the current date
            DateTime currentDate = DateTime.Now;

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO games (user_ID, points, date) VALUES (@userId, @points, @date)";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@points", points);
                cmd.Parameters.AddWithValue("@date", currentDate.ToString("yyyy-MM-dd")); // Format the date as "YYYY-MM-DD"

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Insert successful
                    return true;
                }
                else
                {
                    // No rows affected, insertion failed
                    Debug.Log("Error adding points: No rows affected");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error adding points: " + ex.Message);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

}
