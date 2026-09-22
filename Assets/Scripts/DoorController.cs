using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class DoorController : MonoBehaviour
{
    [SerializeField] private Camera CurrentCam;
    [SerializeField] private Camera DisabledCam1;
    [SerializeField] private Camera DisabledCam2;

    private PlayerController Player;
    private Collider doorCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindAnyObjectByType<PlayerController>();
        doorCollider = GetComponent<Collider>();
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CurrentCam.gameObject.SetActive(true);
            DisabledCam1.gameObject.SetActive(false);
            DisabledCam2.gameObject.SetActive(false);
        }
    }
}
