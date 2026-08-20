using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public bool IfMoving;
    public bool IfUp;
    public bool IfDown;

    public Animator anim_player;

    public LayerMask TerrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    [SerializeField] Interact Interacted;
    [SerializeField] GameObject Inventory;

  

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim_player.SetBool("IfMoving", false);
    }

    private void Update()
    {
        PlayerRotateOnDir();
        InteractWObject();
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

     
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0f, y);
        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

      
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.D)))
        {
            anim_player.SetBool("IfMoving", true);
        }
        
        if (!Input.anyKey)
        {
            anim_player.SetBool("IfMoving", false);
            anim_player.SetBool("IfUp", false);
            anim_player.SetBool("IfDown", false);
        }

        if (x! < 0f)
        {
            sr.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim_player.SetBool("IfMoving", true);
            anim_player.SetBool("IfUp", false);
            anim_player.SetBool("IfDown", false);
        }
        else if (x! > 0f)
        {
            sr.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim_player.SetBool("IfMoving", true);
            anim_player.SetBool("IfDown", false);
            anim_player.SetBool("IfUp", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim_player.SetBool("IfUp", true);
            anim_player.SetBool("IfDown", false);
        }
     

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim_player.SetBool("IfDown", true);
            anim_player.SetBool("IfUp", false);
        }

    }

    void InteractWObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interacted.selection != null)
            {
                Interacted.selection.GetComponent<Interactables>().Interact();
                Debug.Log("Item Picked UP");
            }
            else
            {
                Debug.Log("Item Not Found");
            }
        }
    }

    void OpenInventory()
    {
      
    }

    
}
