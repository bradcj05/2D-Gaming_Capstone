﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSwitching : MonoBehaviour
{
    public int selectedPlane = 0;
    float switchDelay = 2f;
    public float switchTimer; // Public for better testing
    int squadronSize;
    Transform[] squadArr;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        switchTimer = 0f;
        squadronSize = transform.childCount;

        //Assign numbers to the planes
        squadArr = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            squadArr[i] = transform.GetChild(i);
        }
        SelectPlane(squadArr[selectedPlane].position);
        startPos = squadArr[selectedPlane].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
            Destroy(transform.gameObject);

        if (switchTimer < switchDelay)
            switchTimer += Time.deltaTime;

        int previousPlane = selectedPlane;
        //Q and E based plane switching
        if (switchTimer >= switchDelay && Input.GetKeyDown(KeyCode.Q) && transform.childCount != 1)
        {
            int j = selectedPlane - 1;
            while (j != selectedPlane)
            {
                if (j < 0)
                    j = transform.childCount - 1;

                if (squadArr[j] != null)
                {
                    selectedPlane = j;
                    SelectPlane(squadArr[previousPlane].position);
                    break;
                }
                j--;
            }
            switchTimer = 0;
        }
        else if (switchTimer >= switchDelay && Input.GetKeyDown(KeyCode.E) && transform.childCount != 1)
        {
            int j = selectedPlane + 1;
            while (j != selectedPlane)
            {
                if (j == transform.childCount)
                    j = 0;

                if (squadArr[j] != null)
                {
                    selectedPlane = j;
                    SelectPlane(squadArr[previousPlane].position);
                    break;
                }
                j++;
            }
            switchTimer = 0;
        }

        //TODO swap to next plane if previous plane dies
        //Fix?
        if (squadronSize > transform.childCount)
        {
            for (int j = 0; j < squadArr.Length; j++)
            {
                if (squadArr[j] != null)
                {
                    selectedPlane = j;
                    SelectPlane(startPos);
                    switchTimer = 0f;
                    squadronSize = transform.childCount;
                    break;
                }
            }
        }
    }

    //Changes which of the planes in the squadron are active
    void SelectPlane(Vector3 prevPos)
    {
        //Add Animation
        int i = 0;
        foreach (Transform plane in squadArr)
        {
            //Loop through the squadron to determine which one is the one to set active
            if (i == selectedPlane)
            {
                plane.gameObject.SetActive(true);
                plane.gameObject.tag = "ActivePlayer";
                plane.position = prevPos;
                Player planeObj = plane.GetComponent<Player>();
                // Update health bars
                HealthBar hb = planeObj.healthBar;
                HealthBar db = planeObj.defenseBar;
                if (db != null)
                {
                    db.SetMax(plane.GetComponent<Destructible>().defense);
                }
                if (hb != null)
                {
                    hb.SetMax(plane.GetComponent<Destructible>().getMaxHealth());
                    hb.SetHealth(plane.GetComponent<Destructible>().health);
                }
                // Reset cooldown slider and secondary ammo text accordingly
                if (planeObj.numberOfSecondaryWeapons == 0)
                {
                    planeObj.getCooldownSlider().SetMax(0);
                    planeObj.getSecondaryAmmo().text = "";
                }
            }
            else if (plane != null)
            {
                plane.gameObject.SetActive(false);
                plane.gameObject.tag = "Player";
            }
            i++;
        }
    }
}
