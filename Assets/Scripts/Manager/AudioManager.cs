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
}
