using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = Unity.Mathematics.Random;

public class CloudManager : MonoBehaviour
{
    public List<GameObject> Cloud = new List<GameObject>();

    [SerializeField] public int CloudAmount = 3;

    
    
    [SerializeField]
    public float MaxY,MinY;
    [SerializeField]
    public float MaxSpeed,MinSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < CloudAmount; i++)
        {
            float y = Random.Range(MinY, MaxY);

            var CloudResult = Cloud[Random.Range(0, Cloud.Count)];
            var speed = Random.Range(MinSpeed, MaxSpeed);

            Vector3 Position = new Vector3(0, y, -speed);
            GameObject cloudInstance = Instantiate(CloudResult);
            cloudInstance.transform.position = Position;
            var anim = cloudInstance.GetComponent<Animation>();
            anim["Cloud Move"].speed = speed;
            anim["Cloud Move"].time = Random.Range(0, anim["Cloud Move"].length);
        }
    }
}
