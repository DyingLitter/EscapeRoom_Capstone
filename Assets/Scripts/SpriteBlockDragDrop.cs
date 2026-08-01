using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SpriteBlockDragDrop : MonoBehaviour
{

    public LayerMask layerMask;
    private Camera mainCamera;
    private SpriteRenderer sprite;

    private Vector3 offset;

    private bool isDragging = false;
    private bool isHovering = false;
    public bool isOver = false;

    [Header("Sort Order")]
    [SerializeField] private int defaultSortOrder;
    [SerializeField] private int sortOverride = 100;

    [Header("Colours (Testing)")]
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color DragColor = Color.magenta;

    //For storing the starting/default colour
    private Color spriteColor;

    void Start()
    {
        mainCamera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      
    }

    private void OnMouseOver()
    {
        Debug.Log("Hovering on " + gameObject.name);
        isOver = true;
    }

    private void OnMouseExit()
    {
        isOver = false;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Drag()
    {
        if (isDragging)
        {

        }
    }
}
