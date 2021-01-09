using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip ShootSound;
    static AudioSource audioSrc;

    void Start()
    {
        ShootSound = Resources.Load<AudioClip>("Laser-Gun-Editied");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void playShootSound()
    {
        audioSrc.PlayOneShot(ShootSound);
    }
}
