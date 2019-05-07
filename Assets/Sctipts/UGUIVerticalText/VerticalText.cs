using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class VerticalText : MonoBehaviour
{

    [Tooltip("字和字之间的距离")]
    public int spacing = 5;

    [Tooltip("一列最多字数")]
    public int VerticalMaxCount = 5;

    private RectTransform rectTransform;

    private Text currentText;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        currentText = rectTransform.GetComponent<Text>();
    }

    private void Start()
    {
        SetVerticalText("《醉花阴》（李清照）_来测试_莫道不消魂，帘卷西风，人比黄花瘦。");
    }

    public void SetVerticalText(string inputText)
    {
        StringBuilder space = new StringBuilder();

        for (int i = 0; i < spacing; i++)
        {
            space.Append(" ");
        }

        string spaceStr = space.ToString();


        StringBuilder finalString = new StringBuilder();

        float lineSpacing = currentText.fontSize * currentText.lineSpacing;

        List<List<char>> charsList = new List<List<char>>();
        for (int i = 0; i < VerticalMaxCount; i++)
        {
            charsList.Add(new List<char>());
        }

        char[] chars = inputText.ToCharArray();
        int currentRow = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            if(currentRow >= VerticalMaxCount)
            {
                currentRow = 0;
            }
            chars[i] = VerticalContent.Replace(chars[i]);

            charsList[currentRow].Add(chars[i]);
            charsList[currentRow].AddRange(spaceStr);

            currentRow++;
        }

        foreach(var l in charsList)
        {
            foreach (var c in l)
            {
                finalString.Append(c);
            }
            finalString.Append("\n");
        }


        currentText.text = finalString.ToString();
    }
}

public static class VerticalContent
{
    /// <summary>
    /// 分隔符,替换符号
    /// </summary>
    private static Dictionary<char, char> SplitSymbol = new Dictionary<char, char>()
    {
        {   ','    ,   ' '   },
        {   '。'   ,   ' '   },
        {   '.'    ,   ' '   },
        {   '《'   ,   '︽'  },
        {   '》'   ,   '︾'  },
        {   '('    ,   '︵'  },
        {   ')'    ,   '︶'  },
        {   '（'    ,   '︵'  },
        {   '）'    ,   '︶'  },
        {   '_'    ,   '︳'   }

    };

    public static char Replace(char input)
    {
        foreach (var item in SplitSymbol.Keys)
        {
            char value = SplitSymbol[item];
            if (Equals(item, input) && !char.IsWhiteSpace(value))
            {
                input = value;
            }
        }

        return input;
    }

}
