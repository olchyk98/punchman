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
            
            Vector2 Position = new Vector2(0, y);
            GameObject gameObject = Instantiate(CloudResult);
            gameObject.transform.localPosition = Position;
            var anim = gameObject.GetComponent<Animation>();
            anim["Cloud Move"].speed = Random.Range(MinSpeed, MaxSpeed); 
        }
    }
}
