using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner : MonoBehaviour
{
    public int numObjects;
    public GameObject prefab;
    public List<Vector2> pointsOnCircle = new List<Vector2>();
    Vector2 p;
    void Start()
    {
        
        Vector3 center = transform.position;

        //for (int i = 0; i < numObjects; i++)
        //{
        //    int a = i * 2;
        //    Vector3 pos = RandomCircle(center, 3.0f, a);
        //    Instantiate(prefab, pos, Quaternion.identity);
        //}
        DrawCirclePoints(3, 4, new Vector2(0, 0)); 

        foreach (var point in pointsOnCircle)
        {
            Instantiate(prefab, point, Quaternion.identity);

            Debug.Log(pointsOnCircle[0]);
            Debug.Log("something");
        }


    }
    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        //Debug.Log(a);
        float ang = 360f;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    void DrawCirclePoints(int points, double radius, Vector2 center)
    {
        float slice = 2 * Mathf.PI / points;
        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            int newX = (int)(center.x + radius * Mathf.Cos(angle));
            int newY = (int)(center.y + radius * Mathf.Sin(angle));
            p = new Vector2(newX, newY);
            pointsOnCircle.Add(p);
        }
    }

   
}
