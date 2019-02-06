using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class collidersGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject prefab;

    public TextMeshPro text;

    public meshGenerator meshGen;

    
    void Start()
    {
        int percentage = 0;
        foreach (var item in meshGen.items)//Loop through the list of items
        {
            percentage += item.chance;
            Vector3 colPos = meshGen.CirclePosition(meshGen.radius, percentage / 100f);//Get the position for the collider according to percentage
            Vector3 txtPos = meshGen.CirclePosition(meshGen.radius / 1.2f, (percentage - (item.chance / 2f)) / 100f);//Get position for textBoxes according to percentage
            var collider = Instantiate(prefab, colPos, Quaternion.identity);//Instantiate the collider
            collider.transform.parent = gameObject.transform;//Set the instantiated objects as children of current gameObject(to make them spin with the wheel)
            text.text = item.value.ToString();//populate the text with the item's value
            var textBox = Instantiate(text, txtPos, Quaternion.identity);//Instantiate the textbox
            textBox.transform.SetParent(gameObject.transform, false);//Set it's parent the current gameObject

        }
    }


}
   
