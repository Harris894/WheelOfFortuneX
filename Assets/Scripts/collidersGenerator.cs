using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidersGenerator : MonoBehaviour
{
    public int numOfColliders;
    public float radius;
    public GameObject prefab;
    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numOfColliders; i++)
        {
            int a = i * 40;
            Vector3 pos = RandomCircle(center, radius, a);
            var collider = Instantiate(prefab, pos, Quaternion.identity);
            collider.transform.parent = gameObject.transform;
        }
    }
    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
