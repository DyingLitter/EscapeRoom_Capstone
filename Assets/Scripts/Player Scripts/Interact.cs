using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject selection;
    [SerializeField] GameObject TouchZone;
    private Material _previousMaterial;
    [SerializeField] public GameObject interactionText;
    private Transform _selection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider col)
    {
        var newTargetObject = col.gameObject.GetComponent<Interactables>();

        if (newTargetObject != null)
        {
            selection = col.transform.gameObject;

            if (selection != null)
            {
                if (_selection != selection.transform)
                {
                    interactionText.SetActive(true);
                }
                _selection = selection.transform;
            }
            return;
        }
        if (_selection != null)
        {
            interactionText.SetActive(false);
            _selection = null;
            selection = null;
        }
        else if (_selection == null)
        {
            interactionText.SetActive(false);
           
        }
    }
}
