using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour
{
    private Color colorDefault = new Color(0.2f, 1, 0.2f),
        colorActive = new Color(1, 0.2f, 0.2f);
    

    private Transform cachedTransform;
    private LineRenderer cachedLR;
    private SpriteRenderer cachedSprite;

    public Transform StartObject;

    // Use this for initialization
    void Start()
    {
        this.cachedTransform = this.GetComponent<Transform>();
        this.cachedSprite = this.GetComponent<SpriteRenderer>();
        this.cachedLR = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cachedTransform.position = v;

        if (Input.GetMouseButtonDown(0))
        {
            cachedSprite.color = colorActive;
            cachedLR.enabled = true;            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cachedLR.enabled = false;
            cachedSprite.color = colorDefault;
        }

        if (Input.GetMouseButton(0))
        {
            cachedLR.SetPosition(0, StartObject.transform.position);
            cachedLR.SetPosition(1, v);
        }
    }
}
