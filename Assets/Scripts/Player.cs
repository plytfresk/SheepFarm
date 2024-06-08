using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System;

public class Player : MonoBehaviour
{
    public int movementSpeed;
    Vector2 tempSpeed = Vector2.zero;
    public Rigidbody2D playerBody;
    void Start()
    {
        Debug.Log("Start");
        gameObject.name = "Phteven";
    }

    void Update()
    {

        //Movement
        playerBody.velocity = Vector2.zero;
        tempSpeed = Vector2.zero;
        if (Input.GetKey(KeyCode.W) == true){
            tempSpeed += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) == true){
            tempSpeed += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) == true){
            tempSpeed += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S) == true){
            tempSpeed += Vector2.down;
        }
        if (tempSpeed != Vector2.zero)
        {
            playerBody.velocity = tempSpeed.normalized;
            playerBody.velocity *= movementSpeed;
        }

        //Inventory

    }
}
