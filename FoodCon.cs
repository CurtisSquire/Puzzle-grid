using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCon : MonoBehaviour
{
    protected Vector2 firstClickPos;
    protected Vector2 LastClickPos;
    [SerializeField] float Angle;
 
    private void OnMouseDown()
    {
        firstClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    private void OnMouseUp()
    {
        Angle = Mathf.Atan2(LastClickPos.y - firstClickPos.y, LastClickPos.x - firstClickPos.x) * 180/Mathf.PI; 
        LastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    
}
