﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Destructible
{
    //To get access to scriptable objects information
    public Transform HUD;
    public Card cards;
    protected Text nameText;
    protected HealthBar cooldownSlider;
    protected Text secondaryAmmo;
    public float maxSpeed = 5f;
    public float enginePower = 1000f;
    Vector2 movement;
    Vector2 moveDir;
    public Collider2D area;
    //Values for rotation
    protected Camera cam;
    Vector2 mousePos;
    // Active weapons stuff
    public int activeSecondaryWeapon = 0;
    public int activeShellGroup = 0;
    public int numberOfSecondaryWeapons = 0;
    public int numberOfShellGroups = 1;

    int isDestroyed;

    //Add death animation

    int sx = 1;  //stop speed variable
    int sy = 1;

    bool left, right, up, down = false;
    // DEV MODE DEV MODE DEV MODE!!! - "V" key to activate
    public bool devMode = false;

    new void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (SceneManager.GetActiveScene().name != "Hangar")
        {
            //Debug.Log("Start assigning values for the player");
            // Initialize HUD components
            HUD = GameObject.Find("HUD").GetComponent<Transform>();
            cam = Camera.main;
            healthBar = FindTag("HealthBar").GetComponent<HealthBar>();
            defenseBar = FindTag("DefenseBar").GetComponent<HealthBar>();
            cooldownSlider = FindTag("Cooldown").GetComponent<HealthBar>();
            nameText = FindTag("NameText").GetComponent<Text>();
            secondaryAmmo = FindTag("Ammo").GetComponent<Text>();
            // Base start
            base.Start();
            // To display name on HealthDock
            nameText.text = cards.name;
            if (maxHealth > 0)
                health = maxHealth;
            else
                maxHealth = health;
            isDestroyed = 1;
            // Set cooldown slider 
        }
    }

    public void SetUp()
    {
        if (SceneManager.GetActiveScene().name != "Hangar")
            Start();
    }

    // Update is called once per frame
    // Input
    new void Update()
    {
          if (SceneManager.GetActiveScene().name != "Hangar")
          {
               base.Update();

            if (left == true && Input.GetAxisRaw("Horizontal") > 0) {
                sx = 1;
                left = false;
            }
            if (right == true && Input.GetAxisRaw("Horizontal") < 0) {
                sx = 1;
                right = false;
            }
            if (up == true && Input.GetAxisRaw("Vertical") < 0) {
                sy = 1;
                up = false;
            }
            if (down == true && Input.GetAxisRaw("Vertical") > 0) {
                sy = 1;
                down = false;
            }
            


               // Movement
               movement.x = Input.GetAxisRaw("Horizontal") *sx;
               movement.y = Input.GetAxisRaw("Vertical") * sy;

               // Get Mouse Position
               mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

               // Update active secondary weapon
               if (Input.GetKeyDown(KeyCode.LeftShift) && numberOfSecondaryWeapons > 0)
               {
                    activeSecondaryWeapon = (activeSecondaryWeapon + 1) % numberOfSecondaryWeapons;
               }
               else if (Input.GetKeyDown(KeyCode.LeftControl) && numberOfSecondaryWeapons > 0)
               {
                    activeSecondaryWeapon = (activeSecondaryWeapon + numberOfSecondaryWeapons - 1) % numberOfSecondaryWeapons;
               }

               // Update active shell group
               if (Input.mouseScrollDelta.y > 0 && numberOfSecondaryWeapons > 0)
               {
                    activeShellGroup = (activeShellGroup + 1) % (numberOfShellGroups + 1);
               }
               else if (Input.mouseScrollDelta.y < 0 && numberOfSecondaryWeapons > 0)
               {
                    activeShellGroup = (activeShellGroup + numberOfShellGroups - 1) % (numberOfShellGroups + 1);
               }
          }
    }

    //Movement
    void FixedUpdate()
    {
          if (SceneManager.GetActiveScene().name != "Hangar")
          {
               movement.Normalize();
               // Move in the direction specified, then force the speed back to max speed if it is already reached.
               // (Provided the max speed is due to moment and not knockback or external factor)
               rb.AddForce(movement * enginePower, ForceMode2D.Force);
               moveDir = rb.velocity / rb.velocity.magnitude;
               if (rb.velocity.magnitude > maxSpeed && movement.magnitude > 0)
               {
                    rb.velocity = maxSpeed * moveDir;
               }

               //Rotate the Player
               Vector2 lookDir = mousePos - rb.position;
               float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
               rb.rotation = angle;
          }
    }


    
 

    void OnTriggerEnter2D(Collider2D other)  //for edge collider.  OnTriggerExit for polygon and box collider
    {  //OnCollisionEnter2D  runs this code
        if (moveDir.x < 0)
        {
            sx = 0;
            left = true;
        }
        if (moveDir.x > 0)
        {
            sx = 0;
            right = true;
        }
        if (moveDir.y > 0)
        {
            sy = 0;
            up = true;
        }
        if (moveDir.y < 0)
        {
            sy = 0;
            down = true;
        }

        Debug.Log("exit");  //proof that the code ran.
    }

    //Player Death
    public new void Die()
    {
        if (isDestroyed == 1)
        {
            //Play death animation
            HUD.GetComponent<Narration>().ChangeLineSet(2); //TODO Change
            transform.gameObject.SetActive(false);
            isDestroyed = 0;
        }
    }

    // Getters and setters for non-Inspector-editable fields
    public HealthBar getCooldownSlider()
    {
        return cooldownSlider;
    }

    public Text getSecondaryAmmo()
    {
        return secondaryAmmo;
    }

    public int GetIsDestroyed()
    {
        return isDestroyed;
    }

    public void setCooldownSlider(HealthBar input)
    {
        cooldownSlider = input;
    }

    public void setNameText(Text input)
    {
        nameText = input;
    }

    public void setSecondaryAmmo(Text input)
    {
        secondaryAmmo = input;
    }

    // Other helper functions
    public Transform FindTag(string tag)
    {
        foreach (Transform child in HUD)
        {
            if (child.tag == tag)
            {
                return child;
            }
        }
        return null;
    }
}
