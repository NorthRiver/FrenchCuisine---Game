﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSnail : MonoBehaviour {
    
	public float movementSpeed = 3f;
    public float rotationSpeed = 150f;
    public float rollingDuration = 1f;
    public float slowCoefficient = 0.5f;

    public Animator animator;
    private bool available = true;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {
        if (available)
        {
            if (Input.GetButtonDown("Jump"))
            {
                available = false;
                animator.SetInteger("state", 2);
                StartCoroutine(rollingMovement());
            }
            else if ((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0))
            {
                animator.SetInteger("state", 1);
                normalMovement();
            }
            else
            {
                animator.SetInteger("state", 0);
            }
        }
      
    }

    void normalMovement()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, x, 0);
        //rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + z));
        transform.Translate(0, 0, z);
    }
    IEnumerator rollingMovement()
    {
        float elapsedTime = 0f;
        while(elapsedTime < rollingDuration)
        {
            elapsedTime += Time.deltaTime;
            var z = Time.deltaTime * movementSpeed * 2;
            transform.Translate(0, 0, z);
            yield return null;
        }
        available = true;
        
    }
    void slowedMovement()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed * slowCoefficient;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
