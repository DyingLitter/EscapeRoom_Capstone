using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] GameObject TouchZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Interactable"))
        {
            
        }
    }
}
