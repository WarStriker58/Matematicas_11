using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    public Button button;
    public float jumpForce = 2.5f;
    public float downwardForce = 2.5f;
    public float rotationSpeed = 5f;
    public Vector3 backwardForce = new Vector3(-5f, 0, 0);
    private Rigidbody rb;

    void Start()
    {
        button.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput > 0)
            {
                Jump();
            }
            else if (verticalInput < 0)
            {
                Down();
            }
            float angle = Mathf.Clamp(rb.velocity.y * rotationSpeed, -90f, 90f);
            transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Down();
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Down()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.down * downwardForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipeline"))
        {
            rb.AddForce(backwardForce, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Limit"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        button.gameObject.SetActive(true);
    }
}