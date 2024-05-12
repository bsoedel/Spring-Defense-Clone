using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectDragging : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject draggedObject;
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
            draggedObject.transform.localPosition = tilemap.GetCellCenterLocal(cellPosition) + Vector3.forward;
            draggedObject = null;

        }
        if (draggedObject)
        {
            draggedObject.transform.position = mousePosition + Vector3.forward;
        }
    }
}
