using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Transform playerTransform;
    

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       
    }
    private void Update()
    {
        if (playerTransform.transform.position.y <= 0.1)
        {
            Invoke(nameof(ReloadLevel), 1f);
            
        }
        else
        {
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            Die();
        }
    }
        void Die()
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            Invoke(nameof(ReloadLevel), 1.3f);
        }

        void ReloadLevel()
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
     
       public void FallDie()
    {

    }
   
}
