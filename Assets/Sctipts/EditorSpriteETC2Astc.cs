using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

//public class EditorSpriteETC2Ast : AssetPostprocessor
//{
//    public void OnPreprocessTexture()
//    {
//        TextureImporter textureImporter = (TextureImporter)assetImporter;
//        if (textureImporter == null) return;
//        UnityEngine.Debug.Log("change image import setting");
//        TextureImporterPlatformSettings settings = new TextureImporterPlatformSettings();
//        settings.name = BuildTarget.Android.ToString();
//        settings.overridden = true;
//        settings.maxTextureSize = 1024;
//        settings.format = TextureImporterFormat.ASTC_6x6;
//        textureImporter.SetPlatformTextureSettings(settings);
//        textureImporter.SaveAndReimport();
//    }
//}

public class EditorSpriteETC2Astc
{
    [MenuItem("Assets/UI/Sprite选择ASTC", false, 1)]
    public static void OnReimportSprite()
    {
        //AssetImporter.GetAtPath
        List<string> fileList = new List<string>();
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj);

            getFiles(path, ".png", ref fileList, true);

            var androidName = BuildTarget.Android.ToString();
            foreach (var item in fileList)
            {
                Debug.Log("文件路径:" + item);
                var import = AssetImporter.GetAtPath(item);
                if(import)
                {
                    TextureImporter textureImporter = (TextureImporter)import;
                    var saveImport = false;
                    // 关闭 mapmip
                    if(textureImporter.textureType == TextureImporterType.Default && textureImporter.mipmapEnabled)
                    {
                        textureImporter.streamingMipmaps = false;
                        saveImport = true;
                    }
                    var curFormat = textureImporter.GetPlatformTextureSettings(androidName);


                    if (curFormat.format != TextureImporterFormat.ASTC_6x6)
                    {
                        TextureImporterPlatformSettings settings = new TextureImporterPlatformSettings();
                        settings.name = androidName;
                        settings.overridden = true;
                        settings.maxTextureSize = 1024;
                        settings.format = TextureImporterFormat.ASTC_6x6;
                        textureImporter.SetPlatformTextureSettings(settings);
                        saveImport = true;
                    }

                    if(saveImport)
                    {
                        textureImporter.SaveAndReimport();
                    }
                }
            }
        }
    }

    private static void getFiles(string path, string suffix, ref List<string> fileList, bool isSubcatalog)
    {
        string filename;
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] file = dir.GetFiles();
        foreach (FileInfo f in file)
        {
            filename = f.FullName;//拿到了文件的完整路径
            if (filename.EndsWith(suffix))//判断文件后缀，并获取指定格式的文件全路径增添至fileList  
            {
                filename = filename.Replace('\\', '/');
                filename = filename.Replace(Application.dataPath, "Assets");
                fileList.Add(filename);
            }
        }
        //获取子文件夹内的文件列表，递归遍历
        if (isSubcatalog)
        {
            DirectoryInfo[] dii = dir.GetDirectories();//如需遍历子文件夹时需要使用  
            foreach (DirectoryInfo d in dii)
            {
                getFiles(d.FullName, "", ref fileList, false);
            }
        }

        return;
    }

}