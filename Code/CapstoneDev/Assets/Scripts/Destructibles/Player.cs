﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
     //Values for rotation
     public Camera cam;
     Vector2 mousePos;

     // Active weapons stuff
     public int activeSecondaryWeapon = 0;
     public int activeShellGroup = 0;
     public int numberOfSecondaryWeapons = 0;
     public int numberOfShellGroups = 1;

     int isDestroyed;
     //Add death animation

     new void Start()
     {
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
          maxHealth = health;
          isDestroyed = 1;
          // Set cooldown slider 
     }

     public void SetUp()
     {
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
          maxHealth = health;
          isDestroyed = 1;
          // Set cooldown slider 
     }

     // Update is called once per frame
     // Input
     new void Update()
     {
          base.Update();

          // Movement
          movement.x = Input.GetAxisRaw("Horizontal");
          movement.y = Input.GetAxisRaw("Vertical");

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

     //Movement
     void FixedUpdate()
     {
          movement.Normalize();
          // Move in the direction specified, then force the speed back to max speed if it is already reached.
          // (Provided the max speed is due to moment and not knockback or external factor)
          rb.AddForce(movement * enginePower, ForceMode2D.Force);
          Vector2 moveDir = rb.velocity / rb.velocity.magnitude;
          if (rb.velocity.magnitude > maxSpeed && movement.magnitude > 0)
          {
               rb.velocity = maxSpeed * moveDir;
          }

          //Rotate the Player
          Vector2 lookDir = mousePos - rb.position;
          float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
          rb.rotation = angle;
     }

     //Player Death
     public new void Die()
     {
          if (isDestroyed == 1)
          {
               //Play death animation
               Destroy(gameObject);
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