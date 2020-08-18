using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nexPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTranform;
    [SerializeField]
    private Transform transformB;
    void Start()
    {
        posA = childTranform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        childTranform.localPosition = Vector3.MoveTowards(childTranform.localPosition, nexPos, speed * Time.deltaTime);
        if (Vector3.Distance(childTranform.localPosition, nexPos) <= 0.1)
        {
            ChangeDestination();
        }
    }
    private void ChangeDestination()
    {
        nexPos = nexPos != posA ? posA : posB;
    }
}
