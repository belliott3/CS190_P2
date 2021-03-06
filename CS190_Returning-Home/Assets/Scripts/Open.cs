﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour {

    bool opened = false;
    public bool playerHere = false;
    public float speed = 10f;
    public float max;
    public float timer = 0;
    Quaternion original;

    public GameObject pickup;

    // Use this for initialization
    void Start () {
        original = transform.rotation;
        //Debug.Log(original[1] + max);
    }
	
	// Update is called once per frame
	void Update () {

        //Check if the door was opened, and where it is in relation to its original position.
        if (opened && playerHere)
        {
            transform.Rotate(new Vector3(0, 1 * speed * Time.deltaTime, 0));
            //Debug.Log(Quaternion.Angle(transform.rotation, original));
        }
        if (Quaternion.Angle(transform.rotation, original) >= original[1] + max)
        {
            opened = false;
        }

        //Check if player is in vicinity, and where the door is in relation to its original position.
        if (!playerHere && Quaternion.Angle(transform.rotation, original) > original[1])
        {
            transform.Rotate(new Vector3(0, -1 * speed * Time.deltaTime, 0));
            timer -= Time.deltaTime;
            //Debug.Log(Quaternion.Angle(transform.rotation, original));
        }
        if (!playerHere && Quaternion.Angle(transform.rotation, original) <= 1 || timer < 0)
        {
            transform.rotation = original;
            timer = 0;
        }

    }

    public void Interact()
    {
        if (playerHere)
            playerHere = false;
        // Only rotate if door is not at the maximum position.
        else if(Quaternion.Angle(transform.rotation, original) < original[1] + max)
        {
            opened = true;
            playerHere = true;
            timer = 5;
        }
        if (pickup != null)
            pickup.SetActive(true);
    }
}
