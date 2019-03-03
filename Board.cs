using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int widthOfBoard;
    [SerializeField] int heightOfBoard;
    [SerializeField] float cellWidth = 1;
    [SerializeField] float cellHight = 1; 
    [SerializeField] GameObject myTile;
    [SerializeField] GameObject[] foods;
    private FruitCell fruitCellDown;
    private FruitCell fruitCellUp; 
    protected FruitCell[,] tiles;
    

    public void Start()
    {
        tiles = new FruitCell[widthOfBoard, heightOfBoard];
        LevelLoader(); 
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp();
        }
        if(Input.GetMouseButtonDown(0))
        {
            onMouseDown(); 
        }
        
    }

    private void onMouseUp()
    {
       fruitCellUp = GetFruiteCellFromMousePos(Input.mousePosition);
        if (fruitCellUp == null)
        {
            return; 
        }
        if (fruitCellUp.IsNeiborCell(fruitCellDown.gridPos))
        {
            if (fruitCellDown == null)
            {
                return;
            }
            //swaps the foodtype. 
            FruitCell.FoodType temp = fruitCellDown.typeOfFood; 
            fruitCellDown.typeOfFood = fruitCellUp.typeOfFood;
            fruitCellUp.typeOfFood = temp;
            //swaps the sprites. 
            Sprite tempSprite = fruitCellDown.foodSprite.sprite;
            fruitCellDown.foodSprite.sprite = fruitCellUp.foodSprite.sprite;
            fruitCellUp.foodSprite.sprite = tempSprite;
            //sets the picked up fruit sprite back into it's cell. 
            fruitCellDown.foodSprite.gameObject.transform.localPosition = Vector3.zero; 
        }
        else
        {
            fruitCellDown.foodSprite.gameObject.transform.localPosition = Vector3.zero; 
        }
    }
    //get's the fruit cell when the mouse is clicked down. 
    private void onMouseDown()
    {
        fruitCellDown = GetFruiteCellFromMousePos(Input.mousePosition); 

    }
    //this function loads the grid and randomly generates fruit in the grid. 
    private void LevelLoader()
    {
        for (int i = 0; i < widthOfBoard; i++)
        {
            for (int j = 0; j < heightOfBoard; j++)
            {
                //(Vector2)transform.position casts transform.position as a vector2 instead of vector3. 
                Vector2 position = (Vector2)transform.position + new Vector2(i * cellWidth, j * cellHight);
                GameObject tile = Instantiate(myTile, position, Quaternion.identity) as GameObject;
                tile.transform.parent = this.transform;
                FruitCell fruit = tile.AddComponent<FruitCell>();
                tiles[i, j] = fruit;
                fruit.gridPos = new Vector2(i, j); 
                tile.name = "(" + i + "," + j + ")";
                int whatTypeOfFood = Random.Range(0, foods.Length);
                tiles[i, j].typeOfFood = (FruitCell.FoodType)whatTypeOfFood; 
                GameObject foodSprite = Instantiate(foods[whatTypeOfFood], position, Quaternion.identity);
                fruit.foodSprite = foodSprite.GetComponent<SpriteRenderer>();
                foodSprite.transform.SetParent(tile.transform); 
                foodSprite.name = "(" + i + "," + j + ")";
            }
        }
    }
    //this function gets the FruitCell from the mouse position by converting the mouse position in unity to the position on my grid. 
    private FruitCell GetFruiteCellFromMousePos(Vector2 _mousePos)
    {
        Debug.Log("mousex - " + _mousePos.x + " mouse y - " + _mousePos.y);
        Vector2 startPos = transform.position;
        float cellXhalf = cellWidth / 2f;
        float CellYhalf = cellHight / 2f;
        //this converts my mouse from screen position to world position. 
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos); 
        Vector2 nomalizedMousePos = mouseWorldPos - startPos;
        //normalizes the mouse position in unity to my grid position. 
        int mouseXGridPos = (int)Mathf.Ceil((nomalizedMousePos.x - cellXhalf) / cellWidth);
        int mouseYGridPos = (int)Mathf.Ceil((nomalizedMousePos.y - CellYhalf) / cellHight);
       // Debug.Log("mouseGridPos" + " x " + mouseXGridPos + " y " + mouseYGridPos);
       // returns null if the MousePosition is out of bounds from the grid. 
        if (mouseXGridPos > 5 || mouseXGridPos < 0 || mouseYGridPos > 5 || mouseYGridPos < 0)
        {
            return null; 
        }
        // if it's not out of bounds it returns the grid position of the mouse. 
        return tiles[mouseXGridPos, mouseYGridPos]; 
    }
    
}
