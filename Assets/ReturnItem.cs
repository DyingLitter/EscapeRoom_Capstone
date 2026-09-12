using UnityEngine;

public class ReturnItem : MonoBehaviour
{
    [SerializeField] private GameObject itemToReturn;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DispenseItem()
    {
        if (itemToReturn != null)
        {
            GameObject returnedItem = Instantiate(itemToReturn, transform.position, Quaternion.identity);
            returnedItem.name = itemToReturn.name;
        }
    }
}
