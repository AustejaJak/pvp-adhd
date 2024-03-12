using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    int keyCount = 3;
    public GameObject key;
    public GameObject text;

    private bool isDragging = false;
    private Vector3 offset;

    public TMP_Text text2;

    private void Start()
    {
        text.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = GetComponent<Collider2D>();

            if (col.OverlapPoint(mousePos))
            {
                isDragging = true;
                offset = transform.position - mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, transform.position.z);
            HandleWallCollisions();
        }
    }

    void HandleWallCollisions()
    {
        Vector2 playerColliderSize = GetComponent<Collider2D>().bounds.size;
        Vector2 playerPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerPosition, playerColliderSize, 0);

        // Check each collider for the "Walls" tag
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Walls")
            {
                // Destroy the player object and print "Game Over"
                Destroy(gameObject);
                text.active = true;
                text2.color = Color.red;
                text2.SetText("Game Over!");
            }
            if (collider.gameObject.tag == "Finish")
            {
                GameObject objective;
                objective = GameObject.FindGameObjectWithTag("Finish");
                Destroy(objective);
                Destroy(gameObject);
                text.active = true;
                text2.color = Color.green;
                text2.SetText("You Won!");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Keys")
        {
            Destroy(collision.gameObject);
            keyCount--;
            GameObject finish;
            finish = GameObject.Find("Finish");
            if (keyCount == 0)
            {
                Destroy(finish);
            }
        }
    }
}