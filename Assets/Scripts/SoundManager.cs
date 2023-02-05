using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Bullet_shoot, Enemy_attack, Enemy_hit_bullet_attack, Enemy_hit_root_attack, Fire_hit_player, Root_attack, Munching_sound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //sound effects
        Bullet_shoot = Resources.Load<AudioClip> ("Bullet_shoot");
        Enemy_attack = Resources.Load<AudioClip> ("Enemy_attack");
        Enemy_hit_bullet_attack = Resources.Load<AudioClip> ("Enemy_hit_bullet_attack");
        Enemy_hit_root_attack = Resources.Load<AudioClip> ("Enemy_hit_root_attack");
        Fire_hit_player = Resources.Load<AudioClip> ("Fire_hit_player");
        Root_attack = Resources.Load<AudioClip> ("Root_attack");
        Munching_sound = Resources.Load<AudioClip> ("Munching_sound");

        //background music
        // battle_music = Resources.Load<AudioClip> ("battle_music");
        // bad_end_music = Resources.Load<AudioClip> ("bad_end_music");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // public static void PlayBackgroundMusic(string clip) {
    //     if (clip == "battle_music") {
    //         audioSrc.Stop ();
    //         audioSrc.loop = true;
    //         audioSrc.clip = battle_music;
    //         audioSrc.volume = 1f;
    //         audioSrc.Play();
    //     } else if (clip == "bad_end_music") {
    //         audioSrc.Stop ();
    //         audioSrc.loop = true;
    //         audioSrc.clip = bad_end_music;
    //         audioSrc.volume = 1f;
    //         audioSrc.Play();
    //     }
    // }

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
            case "Munching_sound":
                audioSrc.PlayOneShot (Munching_sound);
                break;
            default:
                break;
        }
    }
}
