using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int coinCount = 0;
    public float movementSpeed;
    
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(rigidbodyComponent.position.y < -13 || coinCount == 10)
        {
            coinCount = 0;
            SceneManager.LoadScene("FirstScene");
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0) * movementSpeed * Time.deltaTime;
        
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.06f, playerMask).Length == 0)
        {
            return;
        }

        rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            coinCount++;
            Debug.Log(coinCount);
        }
    }
}
