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
    private int qntForWin;
    private static int levelNumber = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        qntForWin = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        SetCountText();
    }

	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = Input.GetKeyDown(KeyCode.Space) && transform.position.y == 0.5 ? 20 : 0;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            moveHorizontal = Input.acceleration.x;
            moveVertical = Input.acceleration.y;
            jump = 0;
        }

        Vector3 movement = new Vector3(moveHorizontal, jump, moveVertical);
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
        countText.text = "Count: " + count + "/" + qntForWin;
    }

    void VerifyIfWon()
    {
        if (count >= qntForWin)
        {
            //winText.text = "You Win!";
            StartNewLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(levelNumber);
    }

    void StartNewLevel()
    {
        levelNumber++;
        count = 0;
        SetCountText();
        RestartLevel();
    }
}
