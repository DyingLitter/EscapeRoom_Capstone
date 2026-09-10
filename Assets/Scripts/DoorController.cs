using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Camera BedroomCam;
    public Camera LivingCam;
    public Camera KitchenCam;

    public bool bedroom;
    public bool living;
    public bool kitchen;

    [SerializeField] GameObject[] DoorTriggers;

    private PlayerController Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BedroomCam.enabled = true;
        LivingCam.enabled = false;
        KitchenCam.enabled = false;
        living = false;
        kitchen = false;
    }

    private void Update()
    {
       if (living == true)
        {
            BedroomCam.enabled = false;
            LivingCam.enabled = true;
        }

       if (kitchen == true)
        {
            LivingCam.enabled = false;
            KitchenCam.enabled = true;
        }
    }


    public void OnTriggerEnter(Collider Player)
    {
       if (DoorTriggers[0])
        {
            living = !true;
            bedroom = false;
        }

       if (DoorTriggers[1])
        {
            kitchen = !true;
        }
    }
}
