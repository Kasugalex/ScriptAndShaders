using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawMeshAndSkybox : MonoBehaviour
{
    
    public Transform cubeTrans;

    public Mesh mesh;

    public Material cubeMaterial;

    public DrawSkybox skybox;
    private RenderTexture rt;

    private Camera cam;

    [Range(-120, 120)]
    public float rotateSpeed = 60;

    void Start()
    {
        rt = new RenderTexture(Screen.width, Screen.height, 24);
        cam = GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        Graphics.SetRenderTarget(rt);
        GL.Clear(true, true, Color.grey);
        cubeMaterial.color = Color.blue;
        cubeMaterial.SetPass(0);
        Graphics.DrawMeshNow(mesh, cubeTrans.localToWorldMatrix);
        cubeTrans.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        
        skybox.DrawMySkybox(cam);

        Graphics.Blit(rt, cam.targetTexture);
    }

}

[System.Serializable]
public struct DrawSkybox
{
    public Material skyBoxMaterial;
    private static Mesh m_mesh;
    private static Vector4[] corners = new Vector4[4];
    public static Mesh farSkyMesh
    {
        get
        {
            if (m_mesh != null) return m_mesh;
            m_mesh = new Mesh();
            m_mesh.vertices = new Vector3[]{
                new Vector4(-1,-1,0,1),
                new Vector4(-1,1,0,1),
                new Vector4(1,1,0,1),
                new Vector4(1,-1,0,1)
            };
            m_mesh.uv = new Vector2[]{
                new Vector2Int(0,1),
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(1,1),
            };

            m_mesh.SetIndices(new int[] { 0, 1, 2, 3 }, MeshTopology.Quads, 0);
            return m_mesh;
        }
    }

    public void DrawMySkybox(Camera cam)
    {
        corners[0] = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.farClipPlane));
        corners[1] = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.farClipPlane));
        corners[2] = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.farClipPlane));
        corners[3] = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane));
        skyBoxMaterial.SetVectorArray("_Corner",corners);
        skyBoxMaterial.SetPass(0);
        Graphics.DrawMeshNow(farSkyMesh,Matrix4x4.identity);
    }
}
