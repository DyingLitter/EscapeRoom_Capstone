using UnityEngine;
using System.Collections;

public class ColllisionSounds : MonoBehaviour
{
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "Player")
        {
            if (source != null)
                source.Play();
        }
        else
            source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
