using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Camera RCam;
    private Collider RoomCol;

    void Start()
    {
        RoomCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateRoomCamera();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeactivateRoomCamera();
        }
    }

    void ActivateRoomCamera()
    {
        if (RCam != null)
        {
            RCam.gameObject.SetActive(true);
            Debug.Log($"Entered room: {gameObject.name}");
        }
    }

    void DeactivateRoomCamera()
    {
        if (RCam != null)
        {
            RCam.gameObject.SetActive(false);
            Debug.Log($"Left room: {gameObject.name}");
        }
    }
}
