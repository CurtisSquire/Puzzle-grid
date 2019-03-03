using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCell : MonoBehaviour
{
    public SpriteRenderer foodSprite;  
    public Vector2 gridPos;
    public FoodType typeOfFood = FoodType.Undefined; 
    //here I use a enum to define the different foods that can be in a FruiCell. 
    public enum FoodType
    {
        Undefined = -1, 
        Apple = 0, 
        Bacon = 1, 
        Brownie = 2, 
        Cheese = 3, 
        Chicken = 4, 
        Eggs = 5, 
        Honey = 6, 
        Jam =7
    } 
    //here I create a bool that takes a vector 2(the grid position) and returns true or false depending on weather or not a food is in the same column or row.
    public bool IsNeiborCell(Vector2 _gridPos)
    {
        if (gridPos.x == _gridPos.x || gridPos.y == _gridPos.y)
        {
            return true; 
        }
        return false; 
    }


}
