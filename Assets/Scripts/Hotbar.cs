using UnityEngine;
using Unity.UI;
using System.Collections;
using UnityEngine.UI;
public class Hotbar : MonoBehaviour

{
    [SerializeField] GUI[] HotbarSlots;
    private Interactables Interactables;
    private Interact Interact;
    private GameManager Key;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem()
    {
        if (Key.StickGet == true)
        {
            
            var sticksprite = Resources.Load<GameObject>("UI/Stick");
            //Instantiate(sticksprite, HotbarSlots[0].TransformPoint);
        }
       
    }
}
