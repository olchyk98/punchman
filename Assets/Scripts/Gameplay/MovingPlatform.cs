using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float mySpeed;
    [SerializeField] private float myFrequency;
 
    private Dictionary<Transform, Transform> myPrevParents = new Dictionary<Transform, Transform>();
    private float myOffsetY;
    private Transform myTransform;
    private float myCurrentAmount = 0;

    private void Start()
    {
        myOffsetY = transform.position.y;
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        myCurrentAmount += Time.deltaTime * mySpeed;
        var yAmount = Mathf.Sin(myCurrentAmount);
        var y = yAmount * myFrequency * 0.5f;
        myTransform.position = new Vector3(myTransform.position.x, y + myOffsetY);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        myPrevParents.Add(other.transform, other.transform.parent);
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(myPrevParents[other.transform]);
        myPrevParents.Remove(other.transform);
    }
}
