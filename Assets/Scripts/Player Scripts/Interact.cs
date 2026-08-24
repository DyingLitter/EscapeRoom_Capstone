using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject selection;
    [SerializeField] GameObject TouchZone;
    [SerializeField] Material highlightMaterial;
    private Material previousMaterial;
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
                var selectionRenderer = selection.GetComponent<Renderer>();

                if (selectionRenderer != null)
                {
                    if (_selection != null && _selection != selection.transform)
                    {
                        var previousRenderer = _selection.GetComponent<Renderer>();
                        if (previousRenderer != null)
                        {
                            previousRenderer.material = previousMaterial;
                        }
                    }
                }

                if (_selection != selection.transform)
                {
                    Vector3 selectionpos = selection.transform.position;

                    interactionText.transform.position = new Vector3(selectionpos.x, selectionpos.y + 0.6f, selectionpos.z); 
                    interactionText.SetActive(true);
                    previousMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection.transform;
            }
            return;
        }
        if (_selection != null)
        {
            var previousRenderer = _selection.GetComponent<Renderer>();
            if (previousRenderer != null)
            {
                previousRenderer.material = previousMaterial;
                interactionText.SetActive(false);
            }
            _selection = null;
            selection = null;
        }
        else if (_selection == null)
        {
            interactionText.SetActive(false);
           
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (_selection != null && selection != null)
        {
            var selectionRenderer = selection.GetComponent<Renderer>();

            selectionRenderer.material = previousMaterial;
            interactionText.SetActive(false);
            _selection = null;
            selection = null;
        }
       
    }
}
