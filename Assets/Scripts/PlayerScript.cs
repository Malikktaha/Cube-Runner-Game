using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Presets;

//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody;
    public float speed = 10f;
    public float jump = 7f;
    bool isGrounded = true;
    public float force = 1000f;
    public float maxX;
    public float minX;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        transform.position = playerPos;

        //if (playerPos.x < minX) 
        //{
        //    playerPos.x = minX;        
        //}                                                    mannual way to clamp a body
        //transform.position = playerPos;
        //if (playerPos.x > maxX) 
        //{
        //    playerPos.x = maxX;
        //}
        //transform.position = playerPos;

        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D)) 
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0,0);
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded == true)
        {
            rigidbody.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isGrounded = false;
        }

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        //{                                                                                                    to add jump in game
        //    transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        //}
    }
    private void FixedUpdate()
    {
        //transform.position = transform.position + new Vector3(0, 0, 10f*Time.deltaTime);
        rigidbody.AddForce(0, 0, force * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
