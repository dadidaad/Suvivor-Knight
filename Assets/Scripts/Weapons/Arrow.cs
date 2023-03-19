using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10;
    public int damage = 5;
    public float maxDistance = 10;

    private Vector2 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //conquaredDistance = Vector2.Distance(transform.position, startPosition);
        //if(conquaredDistance >= maxDistance)
        //{
        //    DisableObject();
        //}
    }

    private void DisableObject()
    {
        rigidbody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(Vector2 pointerPosition, Vector2 circlePosition)
    {
        startPosition = circlePosition;
        rigidbody2D.velocity = (pointerPosition - circlePosition).normalized * speed;
    }

}
