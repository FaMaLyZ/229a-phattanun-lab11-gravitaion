using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;
    public static List<Gravity> otherObjectList;

    [SerializeField] bool planet =false;
    [SerializeField] int orbitSpeed = 0;    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(otherObjectList == null )
        {
            otherObjectList = new List<Gravity>();
        }

        otherObjectList.Add(this);
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach(Gravity gra in otherObjectList)
        {
            if(gra != this)
            {
                Attract(gra);
            }
        }
        
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - other.rb.position;

        float distance = direction.magnitude;
        
        if(distance <= 0f)
        {
            return;
        }

        float forceMagnitude = G* (rb.mass * otherRb.mass)/Mathf.Pow(distance,2);

        Vector3 garvityForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(garvityForce);
    }
}
