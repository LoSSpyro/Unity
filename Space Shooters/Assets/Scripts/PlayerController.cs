﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbody;
    private AudioSource audio;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}

    void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rbody.velocity = movement * speed;

        rbody.position = new Vector3
        (
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(rbody.position.z, boundary.zMin, boundary.zMax)
        );

        rbody.rotation = Quaternion.Euler(0.0f, 0.0f, rbody.velocity.x * -tilt);
    }
}
