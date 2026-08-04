using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject selection;
    [SerializeField] GameObject TouchZone;
    [SerializeField] private Material highlightMaterial;
    private Material _previousMaterial;
    [SerializeField] public List<string> selectableTags = new List<string> { "Interactable" };
    [SerializeField] public GameObject interactionText;
    private Transform _selection;
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
        if (col.gameObject.GetComponent<Interactables>() != null)
        {
            selection = col.transform.gameObject;

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

        if (_selection == null)
        {
            interactionText.SetActive(false);
        }
    }
}
