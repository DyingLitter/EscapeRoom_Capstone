using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public GameObject selection; 
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    [SerializeField] private Material highlightMaterial;
    private Material _previousMaterial;
    [SerializeField] private List<string> selectableTags = new List<string> { "Interactable" };
    [SerializeField] public GameObject interactionText; // Reference to the UI text element 

    private Transform _selection;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, 3)) //Distance
        {
            collision = hit.point;
            selection = hit.transform.gameObject; 

            // Check if the object is selectable
            if (selectableTags.Contains(selection.tag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();

                if (selectionRenderer != null)
                {
                    
                    if (_selection != null && _selection != selection.transform)
                    {
                        var previousRenderer = _selection.GetComponent<Renderer>();
                        if (previousRenderer != null)
                        {
                            previousRenderer.material = _previousMaterial;
                            
                        }
                    }

                  
                    if (_selection != selection.transform)
                    {
                        interactionText.SetActive(true);
                        _previousMaterial = selectionRenderer.material;
                        selectionRenderer.material = highlightMaterial;
                    }

                    _selection = selection.transform;
                }
                return; 
            }
        }

        // Reset the material if no object is hit or the object is not selectable
        if (_selection != null)
        {
            var previousRenderer = _selection.GetComponent<Renderer>();
            if (previousRenderer != null)
            {
                previousRenderer.material = _previousMaterial;
                interactionText.SetActive(false);
            }
            _selection = null;
            selection = null; 
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}

