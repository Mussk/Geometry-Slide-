using System;
using UnityEngine;

public class RailController : MonoBehaviour
{
    [SerializeField]
    private GameObject railEnd;
    
    [SerializeField]
    private int destroyThreshold = -4;
    
    [SerializeField]
    private float speed;
    
    public GameObject railContentPatterns;
    
    public static event Action<float, GameObject> OnRailDestroyed;
    private void Update()
    {
        
        DestroyRail();
    }

    private void FixedUpdate()
    {
        MoveRail();
    }

    private void MoveRail()
    {
        transform.position += new Vector3(0, 0, -Time.deltaTime * speed);
    }


    private void DestroyRail()
    {
        if (railEnd.transform.position.z <= destroyThreshold)
        {
            OnRailDestroyed?.Invoke(transform.position.x, gameObject);
            
        }
            
    }
}




