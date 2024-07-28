using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CreateSphereMesh : MonoBehaviour
{
    public float radius = 1.0f;
    public int segments = 24;
    public int rings = 12;

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateSphere();
    }

    Mesh CreateSphere()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(segments + 1) * (rings + 1)];
        int[] triangles = new int[segments * rings * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int i = 0; i <= rings; i++)
        {
            float phi = i * Mathf.PI / rings;
            for (int j = 0; j <= segments; j++)
            {
                float theta = j * 2 * Mathf.PI / segments;
                float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float y = radius * Mathf.Cos(phi);
                float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);

                vertices[vertexIndex++] = new Vector3(x, y, z);

                if (i < rings && j < segments)
                {
                    int a = i * (segments + 1) + j;
                    int b = a + segments + 1;
                    int c = a + 1;
                    int d = b + 1;

                    triangles[triangleIndex++] = a;
                    triangles[triangleIndex++] = b;
                    triangles[triangleIndex++] = c;
                    triangles[triangleIndex++] = c;
                    triangles[triangleIndex++] = b;
                    triangles[triangleIndex++] = d;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
