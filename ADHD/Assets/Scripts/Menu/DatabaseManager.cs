using UnityEngine;
using System;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    private string _connectionString;
    private MySqlConnection _connection;

    private void Awake()
    {
        _connectionString = "server=localhost;database=unity;user=root;password=root";
        _connection = new MySqlConnection(_connectionString);
    }

    private void OpenConnection()
    {
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
    }

    private void CloseConnection()
    {
        if (_connection.State == ConnectionState.Open)
            _connection.Close();
    }

    private string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool RegisterUser(string username, string password, int age)
    {
        try
        {
            OpenConnection();

            // Hash the password
            string hashedPassword = HashPassword(password);

            using MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "INSERT INTO players (username, hash, age) VALUES (@username, @password, @age)";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);
            cmd.Parameters.AddWithValue("@age", age);
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

            using MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText =
                "SELECT id FROM players WHERE username = @username AND hash = @password";
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

    public bool AddPoints(int userId, List<int> scores, List<string> scenes, List<int> errors, List<int> points)
    {
        GameMetrics gameMetrics = new GameMetrics();

        for (int i = 0; i < points.Count; i++)
        {
            string scene = scenes[i];

            switch (scene)
            {
                case "Concentration":
                gameMetrics.ConcentrationErrors = errors[i];
                gameMetrics.ConcentrationPoints = points[i];
                gameMetrics.ConcentrationScore = scores[i];
                break;
                case "Destination":
                gameMetrics.DestinationErrors = errors[i];
                gameMetrics.DestinationPoints = points[i];
                gameMetrics.DestinationScore = scores[i];
                break;
                case "Dual-N-back":
                gameMetrics.DualnBackErrors = errors[i];
                gameMetrics.DualnBackPoints = points[i];
                gameMetrics.DualnBackScore = scores[i];
                break;
                case "Feeding":
                gameMetrics.FeedingErrors = errors[i];
                gameMetrics.FeedingPoints = points[i];
                gameMetrics.FeedingScore = scores[i];
                break;
                case "FlyingSwipe":
                gameMetrics.SwipeErrors = errors[i];
                gameMetrics.SwipePoints = points[i];
                gameMetrics.SwipeScore = scores[i];
                break;
                case "MemoryMatrix":
                gameMetrics.MatrixErrors = errors[i];
                gameMetrics.MatrixPoints = points[i];
                gameMetrics.MatrixScore = scores[i];
                break;
                case "SequenceMemory":
                gameMetrics.SequenceErrors = errors[i];
                gameMetrics.SequencePoints = points[i];
                gameMetrics.SequenceScore = scores[i];
                break;
                case "ShadowGame":
                gameMetrics.ShadowErrors = errors[i];
                gameMetrics.ShadowPoints = points[i];
                gameMetrics.ShadowScore = scores[i];
                break;
                case "TextColor":
                gameMetrics.TextColorErrors = errors[i];
                gameMetrics.TextColorPoints = points[i];
                gameMetrics.TextColorScore = scores[i];
                break;
            }
        }

        try
        {
            OpenConnection();

            // Get the current date
            DateTime currentDate = DateTime.Now;

            using MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText =
                @"INSERT INTO games (user_ID,
                                    date, 
                                    concentrationPoints, 
                                    concentrationErrors, 
                                    concentrationScore, 
                                    destinationPoints, 
                                    destinationErrors, 
                                    destinationScore, 
                                    dualnBackPoints, 
                                    dualnBackErrors, 
                                    dualnBackScore, 
                                    feedingPoints, 
                                    feedingErrors, 
                                    feedingScore, 
                                    swipePoints, 
                                    swipeErrors, 
                                    swipeScore, 
                                    matrixPoints, 
                                    matrixErrors, 
                                    matrixScore, 
                                    sequencePoints, 
                                    sequenceErrors, 
                                    sequenceScore, 
                                    shadowPoints, 
                                    shadowErrors, 
                                    shadowScore, 
                                    textColorPoints, 
                                    textColorErrors, 
                                    textColorScore)
                VALUES (@userId,
                        @date, 
                        @ConcentrationPoints, 
                        @ConcentrationErrors, 
                        @ConcentrationScore, 
                        @DestinationPoints, 
                        @DestinationErrors, 
                        @DestinationScore, 
                        @DualnBackPoints, 
                        @DualnBackErrors, 
                        @DualnBackScore, 
                        @FeedingPoints, 
                        @FeedingErrors, 
                        @FeedingScore, 
                        @SwipePoints, 
                        @SwipeErrors, 
                        @SwipeScore, 
                        @MatrixPoints, 
                        @MatrixErrors, 
                        @MatrixScore, 
                        @SequencePoints, 
                        @SequenceErrors, 
                        @SequenceScore, 
                        @ShadowPoints, 
                        @ShadowErrors, 
                        @ShadowScore, 
                        @TextColorPoints, 
                        @TextColorErrors, 
                        @TextColorScore)";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@date", currentDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@ConcentrationErrors", (object)gameMetrics.ConcentrationErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ConcentrationPoints", (object)gameMetrics.ConcentrationPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ConcentrationScore", (object)gameMetrics.ConcentrationScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DestinationPoints", (object)gameMetrics.DestinationPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DestinationErrors", (object)gameMetrics.DestinationErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DestinationScore", (object)gameMetrics.DestinationScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DualnBackPoints", (object)gameMetrics.DualnBackPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DualnBackErrors", (object)gameMetrics.DualnBackErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DualnBackScore", (object)gameMetrics.DualnBackScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FeedingPoints", (object)gameMetrics.FeedingPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FeedingErrors", (object)gameMetrics.FeedingErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FeedingScore", (object)gameMetrics.FeedingScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SwipePoints", (object)gameMetrics.SwipePoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SwipeErrors", (object)gameMetrics.SwipeErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SwipeScore", (object)gameMetrics.SwipeScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MatrixPoints", (object)gameMetrics.MatrixPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MatrixErrors", (object)gameMetrics.MatrixErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MatrixScore", (object)gameMetrics.MatrixScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SequencePoints", (object)gameMetrics.SequencePoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SequenceErrors", (object)gameMetrics.SequenceErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SequenceScore", (object)gameMetrics.SequenceScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShadowPoints", (object)gameMetrics.ShadowPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShadowErrors", (object)gameMetrics.ShadowErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShadowScore", (object)gameMetrics.ShadowScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TextColorPoints", (object)gameMetrics.TextColorPoints ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TextColorErrors", (object)gameMetrics.TextColorErrors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TextColorScore", (object)gameMetrics.TextColorScore ?? DBNull.Value);

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

    public DataTable GetPlayerScores(int playerId)
    {
        try
        {
            OpenConnection();

            using MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText =
                "SELECT players.username, games.points FROM games INNER JOIN players ON games.user_ID = players.id WHERE games.user_ID = @userId ORDER BY games.date DESC";
            cmd.Parameters.AddWithValue("@userId", playerId);

            using MySqlDataReader reader = cmd.ExecuteReader();
            DataTable scores = new DataTable();
            scores.Load(reader);
            return scores;
        }
        catch (Exception ex)
        {
            Debug.Log("Error getting player scores: " + ex.Message);
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }

    public string GetPlayerUsername(int playerId)
    {
        try
        {
            OpenConnection();

            using MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT username FROM players WHERE id = @userId";
            cmd.Parameters.AddWithValue("@userId", playerId);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                Debug.Log("Error getting player username: No user found with ID " + playerId);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error getting player username: " + ex.Message);
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }

}

public class GameMetrics
{
    public int? ConcentrationPoints { get; set; }
    public int? ConcentrationErrors { get; set; }
    public int? ConcentrationScore { get; set; }
    public int? DestinationPoints { get; set; }
    public int? DestinationErrors { get; set; }
    public int? DestinationScore { get; set; }
    public int? DualnBackPoints { get; set; }
    public int? DualnBackErrors { get; set; }
    public int? DualnBackScore { get; set; }
    public int? FeedingPoints { get; set; }
    public int? FeedingErrors { get; set; }
    public int? FeedingScore { get; set; }
    public int? SwipePoints { get; set; }
    public int? SwipeErrors { get; set; }
    public int? SwipeScore { get; set; }
    public int? MatrixPoints { get; set; }
    public int? MatrixErrors { get; set; }
    public int? MatrixScore { get; set; }
    public int? SequencePoints { get; set; }
    public int? SequenceErrors { get; set; }
    public int? SequenceScore { get; set; }
    public int? ShadowPoints { get; set; }
    public int? ShadowErrors { get; set; }
    public int? ShadowScore { get; set; }
    public int? TextColorPoints { get; set; }
    public int? TextColorErrors { get; set; }
    public int? TextColorScore { get; set; }
}
