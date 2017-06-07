using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour {

    public int width = 100;
    public int height = 50;

    [ContextMenu("Setup")]
    void Setup()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        List<Vector3> verts = new List<Vector3>();
        List<Vector3> norms = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();

        int index = 0;
        for (int x = 0; x <width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                verts.AddRange(new Vector3[]
                {
                    pos,
                    pos + Vector3.up,
                    pos + new Vector3(1,1,0),
                    pos + Vector3.right
                });

                norms.AddRange(new Vector3[]
                {
                    Vector3.back,
                    Vector3.back,
                    Vector3.back,
                    Vector3.back
                });

                uvs.AddRange(new Vector2[]
                {
                    Vector2.zero,
                    Vector2.up,
                    Vector2.one,
                    Vector2.right
                });

                colors.AddRange(new Color[]
                {
                    Color.white,
                    Color.white,
                    Color.white,
                    Color.white
                });

                tris.AddRange(new int[]
                {
                    index, index + 1, index + 2,
                    index, index + 2, index + 3
                });
                index += 4;
            }
        }
        Mesh mesh = new Mesh();
        mesh.name = "ProcMesh";
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = tris.ToArray();
        meshFilter.sharedMesh = mesh;
    }
}
