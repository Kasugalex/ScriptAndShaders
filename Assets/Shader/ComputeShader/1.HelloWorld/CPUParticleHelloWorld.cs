using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUParticleHelloWorld : MonoBehaviour
{
    public Shader shader;
    public ComputeShader computeShader;

    private ComputeBuffer outputBuffer;
    private ComputeBuffer constantBuffer;
    private ComputeBuffer colorBuffer;

    private Material material;
    private int kernel;
    public const int vertexCount = 65536;
    void Start()
    {
        CreateBuffers();

        material = new Material(shader);
        kernel = computeShader.FindKernel("CSMain");
    }

    private void OnDisable()
    {
        ReleaseBuffer();
    }


    private void OnPostRender()
    {
        Dispatch();

        material.SetPass(0);
        material.SetBuffer("pointsBuffer", outputBuffer);
        material.SetBuffer("colorBuffer", colorBuffer);
        Graphics.DrawProcedural(MeshTopology.Points, vertexCount);
    }

    private void Dispatch()
    {
        constantBuffer.SetData(new[] { Time.time });

        computeShader.SetBuffer(kernel, "cBuffer", constantBuffer);
        computeShader.SetBuffer(kernel, "output", outputBuffer);
        computeShader.SetBuffer(kernel, "color", colorBuffer);
        computeShader.Dispatch(kernel, 64, 64, 1);

    }

    private void CreateBuffers()
    {
        constantBuffer = new ComputeBuffer(1, 4);
        outputBuffer = new ComputeBuffer(vertexCount, 12);
        colorBuffer = new ComputeBuffer(vertexCount, 12);
    }

    private void ReleaseBuffer()
    {
        constantBuffer.Release();
        outputBuffer.Release();

        DestroyImmediate(material);
    }
}
