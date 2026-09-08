using UnityEngine;

public class worldtoscreenpointui : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    private RectTransform myRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myRect.anchoredPosition = Camera.main.WorldToScreenPoint(targetPos.position);
    }
}
