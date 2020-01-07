using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip eatAppleSound, gameOverSound;
    static AudioSource audioSrc; 

    // Start is called before the first frame update
    void Start()
    {
        
        eatAppleSound = Resources.Load<AudioClip> ("apple");
        gameOverSound = Resources.Load<AudioClip> ("gameOver");

        audioSrc = GetComponent<AudioSource> ();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "apple":
            audioSrc.PlayOneShot (eatAppleSound);
            break;

            case "gameOver":
            audioSrc.PlayOneShot (gameOverSound);
            break;
        }
    }

}
