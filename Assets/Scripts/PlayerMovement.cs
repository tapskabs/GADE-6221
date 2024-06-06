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
    [SerializeField] GameObject SparkEffect;
    bool isJumpBoosted = false;
    bool isSpeedBoosted = false;
    int jumpCount = 0;
    int maxJumpCount = 2; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdatePickupStatusText();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        rb.velocity = movement;

        // Check if there is any movement input
        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Play the running sound
            FindAnyObjectByType<AudioManager>().Play("RunningGround");
        }
        //rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        //FindAnyObjectByType<AudioManager>().Play("RunningGround");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            if (!isJumpBoosted)
                Jump();
            else
                StartCoroutine(BoostedJump());
        }

        if (isGrounded())
        {
            jumpCount = 0; 
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpCount++;
    }

    IEnumerator BoostedJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce + 5f, rb.velocity.z);
        jumpCount++;
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
            InstantiateAndDestroySparkEffect(collision.transform.position);
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

    void InstantiateAndDestroySparkEffect(Vector3 position)
    {
        GameObject spark = Instantiate(SparkEffect, position, Quaternion.identity);
        Destroy(spark, 2f); 
    }
}