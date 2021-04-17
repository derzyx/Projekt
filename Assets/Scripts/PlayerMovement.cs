using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rig;
    private float jumpingValue = 9.0f;
    public Text playerScoreText;
    public int playerScore = 0;
    public int lives = 3;

    Vector3 spawnPoint;

    public float playerSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
                var HorizontalMove = Input.GetAxis("Horizontal");

        transform.position += new Vector3(HorizontalMove, 0, 0) * Time.deltaTime * playerSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rig.velocity.y) < 0.001f)
        {
            rig.AddForce(new Vector3(0, jumpingValue, 0), ForceMode.Impulse);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            playerScore++;
            playerScoreText.text = playerScore.ToString();
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("TPtoSpawn"))
        {
            lives--;
            transform.position = spawnPoint;
            if(lives <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                playerScoreText.text = "0";
            }
        }
    }
}