using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioSource sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    sound = GetComponent<AudioSource>();
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySounds()
    {

       sound.Play();

    }

}
