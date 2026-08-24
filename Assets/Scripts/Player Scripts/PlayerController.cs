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

     
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(rawX, 0f, rawY);

        if (moveDir.sqrMagnitude > 1f)
            moveDir.Normalize();

        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);


        if (moveDir == Vector3.zero)
        {
            anim_player.Play("player_idle");
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
                if (moveDir.z < 0f) anim_player.Play("playerwalkdown");
                else anim_player.Play("playerwalkup");
            }
        }
    }

    void InteractWObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interacted.selection != null)
            {
                Interacted.selection.GetComponent<Interactables>().Interact();
                Debug.Log("Item has Itemed");
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
