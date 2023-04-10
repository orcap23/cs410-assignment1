using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
  public float speed = 0;
  public TextMeshProUGUI countText;
  public GameObject winTextObject;

  private Rigidbody rb;
  private int count;
  private float movementX;
  private float movementY;
  private bool grounded = false;
  private bool jump = false;
  private int jumpHeight = 200;


  // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Set the count to zero 
		count = 0;
		SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
}

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 14) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
		}
	}

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if(Input.GetKeyDown(KeyCode.Space) && grounded) {
            jump = true;
            rb.AddForce(Vector3.up * jumpHeight);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            if (jump)
            {
                rb.AddForce(Vector3.up * jumpHeight / 2);
                jump = false;
            }
        }

    }

    void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    } 

}
