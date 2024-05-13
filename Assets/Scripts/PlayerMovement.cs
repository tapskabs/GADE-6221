using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] TextMeshProUGUI pickupStatusText; 

    bool isJumpBoosted = false;
    bool isSpeedBoosted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdatePickupStatusText();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            if (!isJumpBoosted)
                Jump();
            else
                StartCoroutine(BoostedJump());
        }

        
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    IEnumerator BoostedJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce + 5f, rb.velocity.z);
        yield return new WaitForSeconds(5f);
        isJumpBoosted = false;
        UpdatePickupStatusText(); 
    }

    void SpeedBoost()
    {
        movementSpeed *= 2; 
        isSpeedBoosted = true;
        StartCoroutine(ResetSpeedBoost());
        UpdatePickupStatusText(); 
    }

    IEnumerator ResetSpeedBoost()
    {
        yield return new WaitForSeconds(5f);
        movementSpeed /= 2; 
        isSpeedBoosted = false;
        UpdatePickupStatusText(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPU"))
        {
            isJumpBoosted = true;
            Destroy(other.gameObject);
            UpdatePickupStatusText(); 
        }

        if (other.CompareTag("SpeedPU"))
        {
            SpeedBoost();
            Destroy(other.gameObject);
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    void UpdatePickupStatusText()
    {
        if (pickupStatusText != null)
        {
            if (isJumpBoosted)
            {
                pickupStatusText.text = "Jump Boost: Active";
            }
            else if (isSpeedBoosted)
            {
                pickupStatusText.text = "Speed Boost: Active";
            }
            else
            {
                pickupStatusText.text = "No Boost Active";
            }
        }
    }
}