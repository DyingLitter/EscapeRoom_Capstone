using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact : MonoBehaviour
{
    public GameObject selection;
    [SerializeField] Collider TouchZone;
    [SerializeField] Material highlightMaterial;
    private Material previousMaterial;
    [SerializeField] public GameObject interactionText;
    [HideInInspector] public Transform _selection;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

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
                    Vector3 screenPos = mainCamera.WorldToScreenPoint(selectionpos);
                    screenPos.y += 70f; 

                    interactionText.GetComponent<RectTransform>().position = screenPos;
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
