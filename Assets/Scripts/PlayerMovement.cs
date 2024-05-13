using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    bool isJumpBoosted = false;
    bool isSpeedBoosted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSpeedBoosted)
        {
            SpeedBoost();
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
    }

    void SpeedBoost()
    {
        movementSpeed *= 2; // Increase movement speed by doubling it
        isSpeedBoosted = true;
        StartCoroutine(ResetSpeedBoost());
    }

    IEnumerator ResetSpeedBoost()
    {
        yield return new WaitForSeconds(5f);
        movementSpeed /= 2; // Reset movement speed to normal
        isSpeedBoosted = false;
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
}