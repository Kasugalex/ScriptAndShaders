using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVToRGB : MonoBehaviour
{
    public static Color HSV2RGB(float H, float S, float V)
    {
        float newR = 0;
        float newG = 0;
        float newB = 0;
        float p, f, a, b, c;
        Color co = Color.white;
        S = S / 255;
        V = V / 255;

        if (S == 0f)
        {
            H = S = V = Mathf.Round(255 * V / 100);
        }
        else
        {
            p = Mathf.Floor(H / 60);
            f = H / 60 - p;
            a = V * (1 - S);
            b = V * (1 - S * f);
            c = V * (1 - S * (1 - f));

            switch ((int)p)
            {
                case 0:
                    newR = V; newG = c; newB = a;
                    break;
                case 1:
                    newR = b; newG = V; newB = a;
                    break;
                case 2:
                    newR = a; newG = V; newB = c;
                    break;
                case 3:
                    newR = a; newG = b; newB = V;
                    break;
                case 4:
                    newR = c; newG = a; newB = V;
                    break;
                case 5:
                    newR = V; newG = a; newB = b;
                    break;
            }
        }

        co.r = newR;
        co.g = newG;
        co.b = newB;

        return co;
    }
}
