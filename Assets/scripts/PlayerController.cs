using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 60.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private bool grounded;



    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        this.SetCountText();
        this.winTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position,  new Vector3(0,-1000,0), out hit);
        if (hit.distance < 0.6f)
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }
        print(hit.distance);

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

    void OnJump()
    {

        if (this.grounded == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            this.SetCountText();
            if (count >= 12)
            {
                this.winTextObject.SetActive(true);
            }
        }

    }




}
