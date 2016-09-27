using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public int speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private static int levelNumber = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            VerifyIfWon();
        } else if (other.gameObject.CompareTag("Lava"))
        {
            RestartLevel();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void VerifyIfWon()
    {
        if (count >= 8)
        {
            //winText.text = "You Win!";
            StartNewLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("level" + levelNumber.ToString());
    }

    void StartNewLevel()
    {
        levelNumber++;
        count = 0;
        SetCountText();
        RestartLevel();
    }
}
