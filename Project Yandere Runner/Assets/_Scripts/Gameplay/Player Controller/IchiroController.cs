﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Controller for Ichiro
public class IchiroController : InfRunnerController 
{

   

    //////////////////////////////////////////////
    // Class Functions
    //////////////////////////////////////////////

    // UnityEngine Functions //

    private void Awake()
    {
        // Gets the componenets of the player
        _Anim = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();

        // Player will start running after a set period seconds
        Invoke("startRunning", startDelay);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Wall climb logic
        //  If the player has collided with the wall climb object 
        if (collision.gameObject.tag == "Climb")
        {
            // Player is climbing the wall
            _isClimbingWall = true;
        }
        
        // If the player collides with a platform hazard 
        if (collision.gameObject.tag == "Hazard")
        {
            // TODO: When the player hits a hazard. delete the hazard and slow the player
            // down temporarily
            hitPlatformHazard(collision);
        }

        // If the player gets caught by Yumi...
        // End game
    }
    
    // If the player collides with a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Platform Spawn Logic
        // If player exceeds platform spawner, spawn in new platforms
        if (collision.gameObject.tag == "Platform Spawner")
        {
            for(int i = 0; i < platformSpawner.numOfPlatformsToSpawn; i++)
            {
                // Spawn in a new set of platforms into the scene
                platformSpawner.SpawnPlatform();
            }
        }

    } 

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wall Climb Logic
        //  If player isn't touching wall cliimb 
        if (collision.gameObject.tag == "Climb")
        {
            _isClimbingWall = false;
            _rb2d.velocity = new Vector2(0,0);

            // Set animation state to returning to running state
        }
    }

    // Destroys the collided gameobject
    // 
    void hitPlatformHazard(Collision2D collision)
    {
        Destroy(collision.gameObject);
        // Slows the player down temporarily
        //after a certain time, return player speed to normal
    }
    
    

    // Player Dash
    void playerDash()
    {  
        //Vector2 newDashDistance = new Vector2 (dashDistance, 0);
        // Dashes the plater forward
        //this.transform.Translate(dashDistance * Time.deltaTime, 0, 0);
        //rb2D.AddForce(Vector2.right * dashDistance);

        // TODO: Instead of forcing player forwards, greatly increase player speed temporarily
    }

    // When the player has been caught by Yumi
    void Caught()
    {

    }



}
