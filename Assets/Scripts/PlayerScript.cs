﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int HP = 100;
    public static int DamageAmount = 10;

    public static int DamagedCount = 0;
    public GameObject hitVignette;
    private Animator hitVignetteAnimator;

    public GameObject ARcamera;
    private Vector3 tempARcamera;
    public float waitTime = 0.5f;

    public GameObject Pistol;
    private Animator animator;

    private void Start()
    {
        StartCoroutine(getTempCameraLocation());
        animator = Pistol.GetComponent<Animator>();
        hitVignetteAnimator = hitVignette.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //decrease HP when collide with something
        HP -= DamageAmount;
        DamagedCount++;
        hitVignetteAnimator.Play("PlayerTakeHitVignette");
        
    }

    private void Update()
    {
        //kullanıcı yürüyor mu?
        if (tempARcamera != ARcamera.transform.position)
        {
            //play walking Animation
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private IEnumerator getTempCameraLocation()
    {
        while (true)
        {
            tempARcamera = ARcamera.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }

}
