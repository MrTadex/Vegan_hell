using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerShootSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerShootSound = Resources.Load<AudioClip> ("Bullet_shoot");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "Bullet_shoot":
                audioSrc.PlayOneShot (playerShootSound);
                break;
            default:
                break;
        }
    }
}
