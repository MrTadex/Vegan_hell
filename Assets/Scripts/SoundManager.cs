using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Bullet_shoot, Enemy_attack, Enemy_hit_bullet_attack, Enemy_hit_root_attack, Fire_hit_player, Root_attack;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        Bullet_shoot = Resources.Load<AudioClip> ("Bullet_shoot");
        Enemy_attack = Resources.Load<AudioClip> ("Enemy_attack");
        Enemy_hit_bullet_attack = Resources.Load<AudioClip> ("Enemy_hit_bullet_attack");
        Enemy_hit_root_attack = Resources.Load<AudioClip> ("Enemy_hit_root_attack");
        Fire_hit_player = Resources.Load<AudioClip> ("Fire_hit_player");
        Root_attack = Resources.Load<AudioClip> ("Root_attack");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "Bullet_shoot":
                audioSrc.PlayOneShot (Bullet_shoot);
                break;
            case "Enemy_attack":
                audioSrc.PlayOneShot (Enemy_attack);
                break;
            case "Enemy_hit_bullet_attack":
                audioSrc.PlayOneShot (Enemy_hit_bullet_attack);
                break;
            case "Enemy_hit_root_attack":
                audioSrc.PlayOneShot (Enemy_hit_root_attack);
                break;
            case "Fire_hit_player":
                audioSrc.PlayOneShot (Fire_hit_player);
                break;
            case "Root_attack":
                audioSrc.PlayOneShot (Root_attack);
                break;
            default:
                break;
        }
    }
}
