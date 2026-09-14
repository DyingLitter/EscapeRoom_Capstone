using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class DoorController : MonoBehaviour
{
    [SerializeField] private Camera ToggledCam;
    [SerializeField] private Camera DisCam;

    [SerializeField] private GameObject Trig1;
    [SerializeField] private GameObject Trig2;

    private Camera CurrentCam;
    private float colliderDisableDuration = 1.5f;

    private PlayerController Player;
    private Collider doorCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindAnyObjectByType<PlayerController>();
        doorCollider = GetComponent<Collider>();
        CurrentCam = DisCam;
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisableColliderTemporarily(colliderDisableDuration));
        }
    }

    void ReverseControls()
    {
        if (CurrentCam.name == "LivingCam")
        {
            Player.SetControlsReversed(true);
        }
        else
        {
            Player.SetControlsReversed(false);
        }
    }

    void TriggersDisable()
    {
        if (Trig1.activeSelf == true)
        {
            Trig2.SetActive(false);
        }
        else
        {
            Trig1.SetActive(true);
        }
    }

    private IEnumerator DisableColliderTemporarily(float duration)
    {
    
        doorCollider.enabled = false;
        yield return new WaitForSeconds(duration);
        Player.enabled = false;

        if (ToggledCam.gameObject.activeSelf)
        {
            ToggledCam.gameObject.SetActive(false);
            DisCam.gameObject.SetActive(true);
            CurrentCam = DisCam;
        }
        else
        {
            ToggledCam.gameObject.SetActive(true);
            DisCam.gameObject.SetActive(false);
            CurrentCam = ToggledCam;
        }

        ReverseControls();
        TriggersDisable();
        Player.enabled = true;
    }
}
