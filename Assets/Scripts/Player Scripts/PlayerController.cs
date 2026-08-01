using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask TerrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    [SerializeField] Interact Interacted;
    [SerializeField] GameObject Inventory;

    public bool KeyGet;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        KeyGet = false;
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

        if (x! < 0f)
        {
            sr.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x! > 0f)
        {
            sr.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void InteractWObject()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Interacted.selection != null)
            {
                Interacted.selection.GetComponent<Interactables>().Interact();
            }
        }
    }

    void OpenInventory()
    {
      
    }

    
}
