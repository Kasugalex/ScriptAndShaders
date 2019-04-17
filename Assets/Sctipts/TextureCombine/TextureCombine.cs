using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using System;

public class TextureCombine : MonoBehaviour
{
    private delegate Texture2D WatcherEventHandler();

    public RawImage output;

    public Texture2D input1;

    public Texture2D input2;

    string webAdd = "127.0.0.1/Textures";

    void Start()
    {
        //Watcher(CombienTwoTextureWithGetsetPixel);
        Watcher(CombienWithSetPixels);
    }
    
    private void Watcher(WatcherEventHandler combienEvent)
    {
        
        Stopwatch watcher = new Stopwatch();
        watcher.Start();
        output.texture = combienEvent();
        watcher.Stop();

        print(string.Format("{0}用了：{1}毫秒",combienEvent.Method.Name, watcher.ElapsedMilliseconds));
    }

    /// <summary>
    /// 140-150毫秒
    /// </summary>
    /// <returns></returns>
    private Texture2D CombienTwoTextureWithGetsetPixel()
    {
        int w = input1.width ;
        int h = input1.height + input2.height;
        Texture2D t = new Texture2D(w, h);
       
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < input1.height; j++)
            {
                Color c = input1.GetPixel(i, j);
                t.SetPixel(i, j, c);           
            }
        }

        for (int i = 0; i < w; i++)
        {
            for (int j = input1.height; j < h; j++)
            {
                Color c = input2.GetPixel(i, j);
                t.SetPixel(i, j, c);
            }
        }

        t.Apply();

        return t;
    }

    /// <summary>
    /// 18毫秒左右
    /// </summary>
    /// <returns></returns>
    private Texture2D CombienWithSetPixels()
    {
        int w = input1.width;
        int h = input1.height + input2.height;
        int halfH = h / 2;
        Texture2D t = new Texture2D(w, h);

        Color[] c1 = input1.GetPixels();

        t.SetPixels(0, 0, w, halfH, c1);

        Color[] c2 = input2.GetPixels();

        t.SetPixels(0, halfH, w, halfH, c2);

        t.Apply();

        return t;
    }

       


}
