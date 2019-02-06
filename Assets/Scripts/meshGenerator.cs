using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Item
{
    public string name;
    public int chance;
    public int value;
}

[RequireComponent(typeof(MeshFilter))]
public class meshGenerator : MonoBehaviour
{
    [Header("Circle Settings")]
    public float radius = 5f;

    [Header("Values Settings")]
    [Tooltip("Use random values, if not then populate the list below with your own values")]
    public bool useRandomValues;

    public int minValue;
    public int maxValue;

    public List<Item> items = new List<Item>();

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    private Vector2Int textureSize = new Vector2Int(4, 4);

    int segments = 100;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        //Populate the list with random values.
        if (useRandomValues)
        {

            for (int i = 1; i < items.Count; i++)
            {
                Item item = items[i];
                
                item.value = Random.Range(minValue, maxValue);
                item.chance = 95 / (items.Count-1);
                item.name ="ItemNum" + i;
            }
            //Reserved amount for the jackpot in case of use of random values.
            items[0].name = "jackpot";
            items[0].chance = 5;
            items[0].value = 10000;

        }
        //Create the mesh according to the data. 
        MakeMeshData();
        CreateMesh();
    }

    
    //Generate the vertex positions and the triangles. 
    //Give specific color according to chance percentage by UV mapping.
    void MakeMeshData()
    {
        int i = 0;
        vertices = new Vector3[3 * segments];
        triangles = new int[vertices.Length];
        uvs = new Vector2[vertices.Length];

        for (int x = 0; x < 100; x++)
        {
            triangles[i] = i;
            uvs[i] = GetUVCoords(GetIndexOfPercentage(x));
            vertices[i++] = Vector3.zero;

            triangles[i] = i;
            uvs[i] = GetUVCoords(GetIndexOfPercentage(x));
            vertices[i++] = CirclePosition(radius, ((float)x / (float)segments));

            triangles[i] = i;
            uvs[i] = GetUVCoords(GetIndexOfPercentage(x));
            vertices[i++] = CirclePosition(radius, ((float)(x+1) / (float)segments));
        }
        
    }

    //Get the position for each vertex that will be used to create a triangle
    public Vector3 CirclePosition(float radius, float percentage)
    {
        float ang = 360f * percentage;
        Vector3 pos;
        pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);//calculate the vertex position on the X
        pos.y = radius * Mathf.Cos(ang * Mathf.Deg2Rad);//calculate the vertex position on the y
        pos.z = 0;
        return pos;
    }

    //Pass the newly acquired data into the mesh
    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }

    //This method takes as a parameter the percentage of chance per triangle.
    //It then returns the index of the last triangle where the circle needs to be "sliced"
    int GetIndexOfPercentage(int percentage)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];

            if (percentage < item.chance)
            {

                return i;
            }
            percentage -= item.chance;
        }

        //Debug.LogError("Percentage is higher than total chance");

        return 0;
    }
    
    //In this method we choose the centre point of a pixel in the 4x4 texture for the color of the slices.
    Vector2 GetUVCoords(int index)
    {
        // Find the X & Y
        int x = index % textureSize.x;
        int y = (index - x) / textureSize.x;

        // Find the size of the pixels in 0..1
        float uvStepX = 1f / (float)textureSize.x;
        float uvStepY = 1f / (float)textureSize.y;

        // Calc uv X & Y
        float uvX = (float)x / (float)textureSize.x;
        float uvY = (float)y / (float)textureSize.y;

        uvX = (uvStepX / 2f) + uvX;
        uvY = (uvStepY / 2f) + uvY;

        return new Vector2(uvX, 1f - uvY);
    }

    
    //Return the index of the triangle located at 12 o'clock. 
    public Item GetReward()
    {
        float rotationZ = transform.rotation.eulerAngles.z % 360f;
        int percentage = Mathf.RoundToInt((100f / 360f) * rotationZ);
        int index = GetIndexOfPercentage(percentage);

        return items[index];
    }

    //[ContextMenu("Test Item")]
    //public void TestItem()
    //{
    //    Debug.Log(GetReward().name);
    //}
}
