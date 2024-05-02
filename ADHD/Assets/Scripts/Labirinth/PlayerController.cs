using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;
    public float speed = 5.0f;
    Rigidbody2D rb;
    GameObject canvas;
    GameObject endScreen;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        endScreen = GameObject.Find("END");
        canvas.SetActive(true);
        endScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movX = SimpleInput.GetAxis("Horizontal");
        float movY = SimpleInput.GetAxis("Vertical");

        rb.velocity = new Vector2(movX * speed, movY * speed);

        HandleWallCollisions();
    }
    void HandleWallCollisions()
    {
        Vector2 playerColliderSize = GetComponent<Collider2D>().bounds.size;
        Vector2 playerPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerPosition, playerColliderSize, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Destroy(collision.gameObject);
            Debug.Log("YOU WON!");
            canvas.SetActive(false);
            endScreen.SetActive(true);
        }
    }
}
