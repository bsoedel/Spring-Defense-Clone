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
    public bool[ , ] isOccupied = new bool[6, 7];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                GameObject clone = Instantiate(targetObject.transform.gameObject);
                clone.transform.localScale = new Vector3(0.9f, 0.9f);
                clone.GetComponent<Collider2D>().enabled = false;
                draggedObject = clone;
            }
        }
        if (Input.GetMouseButtonUp(0) && draggedObject)
        {
            Vector3Int cellPosition = tilemap.LocalToCell(mousePosition);
            Vector3 cellCenter = tilemap.GetCellCenterLocal(cellPosition);
            int row = (int)(cellCenter.x + 8.5);
            int col = (int)(cellCenter.y + 2.5);
            if (cellCenter.x >= xLowBound && cellCenter.x <= xHighBound && cellCenter.y >= yLowBound && cellCenter.y <= yHighBound && !isOccupied[row, col])
            {
                draggedObject.transform.localPosition = cellCenter + Vector3.forward;
                isOccupied[row, col] = true;
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
    }
}
