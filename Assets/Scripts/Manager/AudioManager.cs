using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public GameObject Audio;

    [Header("Audio Source")]
    public AudioSource BGM;
    public AudioSource Walking;
    public AudioSource Damage;
    public AudioSource Drinking;
    public AudioSource Eating;

    private void Awake()
    {
        AudioManager.instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Audio);
    }

    private void Update()
    {
        if (BGM.isPlaying == false)
        {
            BGM.PlayOneShot(BGM.clip);
        }

        if (PlayerController.instance.isMove == true && Walking.isPlaying == false)
            Walking.Play();
        else if (PlayerController.instance.isMove == false)
            Walking.Stop();
    }

    public void DamageSound()
    {
        if (Damage.isPlaying == false)
            Damage.Play();
    }

    public void DrinkingSound()
    {
        if (Drinking.isPlaying == false)
            Drinking.Play();
    }

    public void EatingSound()
    {
        if (Eating.isPlaying == false)
            Eating.Play();
    }
}
