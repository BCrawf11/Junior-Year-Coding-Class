﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

    [SerializeField]
    GameObject ballPrefab;
    float speed = 1.5f;

    Vector3 velocity = new Vector3();

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 200 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 200 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity += LookAtDirection(transform.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= LookAtDirection(transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.MyCharacter = this;
        }

        velocity.Normalize();

        ShootBalls();
    }

    void MoveWASD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < 1)
            {
                velocity += Vector3.up;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > -1)
            {
                velocity += Vector3.down;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -1.75)
            {
                velocity += Vector3.left;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 1.75)
            {
                velocity += Vector3.right;
            }
        }
    }

    public Vector3 LookAtDirection(float eulerAnglesZ)
    {
        return new Vector3 (Mathf.Cos(eulerAnglesZ * Mathf.Deg2Rad), Mathf.Sin(eulerAnglesZ * Mathf.Deg2Rad), 0);
    }

    void ShootBalls()
    {
        transform.position += velocity * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBall = Instantiate(ballPrefab);
            newBall.transform.position = transform.position;
            newBall.GetComponent<Ball>().velocity = LookAtDirection(transform.eulerAngles.z);
        }
    }
}
