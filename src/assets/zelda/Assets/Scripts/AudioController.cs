using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void play_enemy_death()
    {
        AudioSource.PlayClipAtPoint(clips[0], Camera.main.transform.position);
    }
    public void play_heart_clip()
    {
        AudioSource.PlayClipAtPoint(clips[1], Camera.main.transform.position);
    }
    public void play_rupee_clip()
    {
        AudioSource.PlayClipAtPoint(clips[2], Camera.main.transform.position);
    }
    public void play_key_clip()
    {
        AudioSource.PlayClipAtPoint(clips[16], Camera.main.transform.position);
    }
    public void play_enemy_damage_clip()
    {
        AudioSource.PlayClipAtPoint(clips[3], Camera.main.transform.position);
    }
    public void play_player_death()
    {
        AudioSource.PlayClipAtPoint(clips[4], Camera.main.transform.position);
    }
    public void play_obtain_clip()
    {
        AudioSource.PlayClipAtPoint(clips[5], Camera.main.transform.position);
    }
    public void play_aquamentus_roar()
    {
        AudioSource.PlayClipAtPoint(clips[6], Camera.main.transform.position);
    }
    public void play_aquamentus_damaged()
    {
        AudioSource.PlayClipAtPoint(clips[7], Camera.main.transform.position);
    }
    public void play_door_open()
    {
        AudioSource.PlayClipAtPoint(clips[8], Camera.main.transform.position);
    }
    public void play_use_sword()
    {
        AudioSource.PlayClipAtPoint(clips[9], Camera.main.transform.position);
    }
    public void play_shoot_sword()
    {
        AudioSource.PlayClipAtPoint(clips[10], Camera.main.transform.position);
    }
    public void play_damage_taken()
    {
        AudioSource.PlayClipAtPoint(clips[11], Camera.main.transform.position);
    }
    public void play_bomb_placement() { 
        AudioSource.PlayClipAtPoint(clips[12], Camera.main.transform.position);
    }
    public void play_bomb_explosion() { 
        AudioSource.PlayClipAtPoint(clips[13], Camera.main.transform.position);
    }
    public void play_low_health() { 
        AudioSource.PlayClipAtPoint(clips[14], Camera.main.transform.position);
    }
    public void play_key_appear() { 
        AudioSource.PlayClipAtPoint(clips[15], Camera.main.transform.position);
    }
    public void play_secret() { 
        AudioSource.PlayClipAtPoint(clips[17], Camera.main.transform.position);
    }
    public void play_boomerang() { 
        AudioSource.PlayClipAtPoint(clips[18], Camera.main.transform.position);
    }
    public void play_arrow() { 
        AudioSource.PlayClipAtPoint(clips[19], Camera.main.transform.position);
    }
    public void play_enter_old()
    {
        AudioSource.PlayClipAtPoint(clips[20], Camera.main.transform.position);
    }
    public void play_clip(int n) { 
        AudioSource.PlayClipAtPoint(clips[n], Camera.main.transform.position);
    }
}
