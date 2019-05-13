using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProperty : MonoBehaviour {

    public Shader shader;
    public ComputeShader computeShader;
    private ComputeBuffer offsetBuffer;
    private ComputeBuffer outputBuffer;
    private ComputeBuffer constantBuffer;
    private ComputeBuffer colorBuffer;

    private Material material;
    private int kernel;
    public const int vertexCount = 65536; //64*64*4

    void Start () {
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
        material.SetBuffer("buf_Points", outputBuffer);
        material.SetBuffer("buf_Colors", colorBuffer);
        Graphics.DrawProcedural(MeshTopology.Points, vertexCount);
    }

    private void Dispatch()
    {
        constantBuffer.SetData(new[] { Time.time });

        computeShader.SetBuffer(kernel,"cBuffer",constantBuffer);
        computeShader.SetBuffer(kernel,"offsets",offsetBuffer);
        computeShader.SetBuffer(kernel,"output",outputBuffer);
        computeShader.SetBuffer(kernel,"color",colorBuffer);
        computeShader.Dispatch(kernel, 64, 64, 1);
        
    }

    private void CreateBuffers()
    {
        offsetBuffer = new ComputeBuffer(vertexCount, 4);
        float[] values = new float[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            values[i] = UnityEngine.Random.value * 2 * Mathf.PI;
        }
        offsetBuffer.SetData(values);

        constantBuffer = new ComputeBuffer(1, 4);
        colorBuffer = new ComputeBuffer(vertexCount, 12);
        outputBuffer = new ComputeBuffer(vertexCount,12);

    }

    private void ReleaseBuffer()
    {
        constantBuffer.Release();
        offsetBuffer.Release();
        outputBuffer.Release();

        DestroyImmediate(material);
    }



}
