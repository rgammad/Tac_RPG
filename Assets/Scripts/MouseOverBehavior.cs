using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class MouseOverBehavior : MonoBehaviour {

    TileMap _tileMap;

    Vector3 currentTileCoord;

    public Transform highlight;

    void Start()
    {
        _tileMap = GetComponent<TileMap>();
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt(hitInfo.point.x / _tileMap.tileSize);
            int z = Mathf.FloorToInt(hitInfo.point.z / _tileMap.tileSize);


            currentTileCoord.x = x;
            currentTileCoord.z = z;

            highlight.transform.position = currentTileCoord * _tileMap.tileSize;

            if (Input.GetMouseButton(0))
            {
                Instantiate(highlight, new Vector3(x, 0, z), Quaternion.identity, transform);
            }
        }
        else { }
    }

}
