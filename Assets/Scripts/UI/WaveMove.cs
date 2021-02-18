using System;
using UnityEngine;

public class WaveMove : MonoBehaviour
{
    [SerializeField] private double mySpeed;
    [SerializeField] private Vector2 myTranslationVector;
    [SerializeField] private int layer;

    private const double myMax = Math.PI * 2;
    private double myCurrentAngle = 0;
    private Vector2 myStartPosition;

    private void Start()
    {
        myStartPosition = transform.position;
    }

    private void Update()
    {
        myCurrentAngle += mySpeed * Time.deltaTime;

        if (myCurrentAngle >= myMax)
            myCurrentAngle = 0;

        var x = Mathf.Cos((float)myCurrentAngle);
        var y = Mathf.Sin((float)myCurrentAngle);

        transform.position = myStartPosition + new Vector3(x, y, layer) * myTranslationVector;
    }


}
