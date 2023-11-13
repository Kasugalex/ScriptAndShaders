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
    [MenuItem("Assets/UI/Spriteѡ��ASTC", false, 1)]
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
                Debug.Log("�ļ�·��:" + item);
                var import = AssetImporter.GetAtPath(item);
                if(import)
                {
                    TextureImporter textureImporter = (TextureImporter)import;
                    var saveImport = false;
                    // �ر� mapmip
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
            filename = f.FullName;//�õ����ļ�������·��
            if (filename.EndsWith(suffix))//�ж��ļ���׺������ȡָ����ʽ���ļ�ȫ·��������fileList  
            {
                filename = filename.Replace('\\', '/');
                filename = filename.Replace(Application.dataPath, "Assets");
                fileList.Add(filename);
            }
        }
        //��ȡ���ļ����ڵ��ļ��б��ݹ����
        if (isSubcatalog)
        {
            DirectoryInfo[] dii = dir.GetDirectories();//����������ļ���ʱ��Ҫʹ��  
            foreach (DirectoryInfo d in dii)
            {
                getFiles(d.FullName, "", ref fileList, false);
            }
        }

        return;
    }

}