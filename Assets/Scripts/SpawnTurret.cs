using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectDragging : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject draggedObject;
    public float xLowBound = -8.5f;
    public float xHighBound = -3.5f;
    public float yLowBound = -2.5f;
    public float yHighBound = 3.5f;
    public bool[ , ] isOccupied = new bool[4, 5];
    public int moneyAmount;
    public TMPro.TMP_Text moneyText;

    private enum TowerCost
    {
        Tower1 = 20,
        Tower2 = 40,
        Tower3 = 80
    }
    // Start is called before the first frame update
    void Start()
    {
        int.TryParse(moneyText.text, out moneyAmount);
        InvokeRepeating("PassiveGoldIncome", 1.0f, 1.0f);
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && targetObject.GetComponent<TowerController>())
            {
                if (targetObject.GetComponent<TowerController>().enabled == false) { 
                    GameObject clone = Instantiate(targetObject.transform.gameObject);
                    clone.transform.localScale = new Vector3(0.9f, 0.9f);
                    clone.GetComponent<Collider2D>().enabled = false;
                    draggedObject = clone;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && draggedObject)
        {
            Vector3Int cellPosition = tilemap.LocalToCell(mousePosition);
            Vector3 cellCenter = tilemap.GetCellCenterLocal(cellPosition);
            int row = (int)(cellCenter.x + 5);
            int col = (int)(cellCenter.y + 1);

            TowerCost cost;
            String strippedName = draggedObject.name.Substring(0, 6);
            Enum.TryParse(strippedName, out cost);
            if (cellCenter.x >= xLowBound && cellCenter.x <= xHighBound && cellCenter.y >= yLowBound && cellCenter.y <= yHighBound 
                && !isOccupied[row, col]
                && moneyAmount >= (int)cost)
            {
                draggedObject.transform.localPosition = cellCenter + Vector3.forward * 0;
                isOccupied[row, col] = true;
                moneyAmount -= (int)cost;
                draggedObject.GetComponent<TowerController>().enabled = true;
                draggedObject.GetComponent<PolygonCollider2D>().enabled = true;    
            }
            else
            {
                Destroy(draggedObject);
            }

            draggedObject = null;
        }
        if (draggedObject)
        {
            draggedObject.transform.position = mousePosition + Vector3.forward;
        }
        moneyText.text = moneyAmount.ToString();
    }

    void PassiveGoldIncome()
    {
        moneyAmount += 10;
    }
}
