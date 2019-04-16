using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawMeshAndSkybox : MonoBehaviour
{

    public Transform cubeTrans;

    public Mesh mesh;

    public Material cubeMaterial;

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
        Graphics.Blit(rt, cam.targetTexture);
    }

}
