using UnityEngine;
using System.Collections;

public class FightPlayerController : MonoBehaviour
{
    private Vector2 touchPos;
    private Vector2 controlVector, forceAtPos;
    private Transform bodyTransform;

    public Rigidbody2D Body;

    public Vector2 ForcePosOffset;

    public float ControlPower;

    // Use this for initialization
    void Start()
    {
        this.bodyTransform = Body.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            forceAtPos = bodyTransform.TransformPoint(ForcePosOffset);
            controlVector = (touchPos - forceAtPos);
            controlVector.Normalize();

            Body.AddForceAtPosition(controlVector * ControlPower * Time.deltaTime, forceAtPos, ForceMode2D.Impulse);
        }
    }
}
