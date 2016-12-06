using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{

    public int sizeX = 25;
    public int sizeZ = 25;
    public int sizeY = 5;

    public float tileSize = 1.0f;


    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    // Use this for initialization
    void Start()
    {
        BuildMesh();
    }

   public  void BuildMesh()
    {
        int numTiles = sizeX * sizeZ;
        int numTriangles = numTiles * 2;

        int vertSizeX = sizeX + 1;
        int vertSizeZ = sizeZ + 1;
        int numVerts = vertSizeX * vertSizeZ;
        //create mesh data
        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTriangles * 3];

        int x, z;
        for (z = 0; z < vertSizeZ; z++)
        {
            for (x = 0; x < vertSizeX; x++)
            {
                vertices[z * vertSizeX + x] = new Vector3(x * tileSize, 0, z * tileSize);
                normals[z * vertSizeX + x] = Vector3.up;
                uv[z * vertSizeX + x] = new Vector2((float)x / vertSizeX, (float)z / vertSizeZ);
            }

        }
        for (z = 0; z < sizeZ; z++)
        {
            for (x = 0; x < sizeX; x++)
            {
                int tileIndex = z * sizeX + x;
                int triangleOffset = tileIndex * 6;
                int tileOffset = z * vertSizeX + x;

                triangles[triangleOffset + 0] = tileOffset + 0;
                triangles[triangleOffset + 1] = tileOffset + vertSizeX + 0;
                triangles[triangleOffset + 2] = tileOffset + vertSizeX + 1;

                triangles[triangleOffset + 3] = tileOffset + 0;
                triangles[triangleOffset + 4] = tileOffset + vertSizeX + 1;
                triangles[triangleOffset + 5] = tileOffset + 1;

            }
        }


        //create mesh & populate
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        //assign mesh to filter/renderer/collider

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}
