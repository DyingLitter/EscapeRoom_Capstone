using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;
    public Animator anim_player;
    private NPC npc;

    public LayerMask TerrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    [SerializeField] Interact Interacted;
    [SerializeField] GameObject Inventory;


  

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerRotateOnDir();
        //InteractWObject();
     
    }

    [SerializeField] private bool controlsReversed = false;

    public void SetControlsReversed(bool reversed)
    {
        if (controlsReversed != reversed)  
        {
            controlsReversed = reversed;
            sr.flipX = !sr.flipX;          
        }
    }

    void PlayerRotateOnDir() // Movement Controls
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, TerrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }


        float rawX = Input.GetAxisRaw("Horizontal");
        float rawY = Input.GetAxisRaw("Vertical");

        if (controlsReversed)
        {
            rawX = -rawX;
            rawY = -rawY;

        }

        Vector3 moveDir = new Vector3(rawX, 0f, rawY);

        if (moveDir.sqrMagnitude > 1f)
            moveDir.Normalize();

        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);


        if (moveDir == Vector3.zero)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            anim_player.Play("playeridle");
        }
        else
        {
            if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.z))
            {
                if (moveDir.x < 0f) anim_player.Play("playerwalk");
                else anim_player.Play("playerwalkright");
            }
            else
            {
                if (controlsReversed)
                {
                    if (moveDir.z > 0f) anim_player.Play("playerwalkdown");
                    else anim_player.Play("playerwalkup");
                }
                else
                {
                    if (moveDir.z < 0f) anim_player.Play("playerwalkdown");
                    else anim_player.Play("playerwalkup");
                }
            }
           

        }

    }

    //void InteractWObject()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (Interacted.selection != null)
    //        {
    //            Interacted.selection.GetComponent<Interactables>().Interact();
    //            Debug.Log("Item has Itemed");
    //        }
    //        else
    //        {
    //            Debug.Log("Item Not Found");
    //        }

    //    }
    //}


    void OpenInventory()
    {
      
    }

    
}
