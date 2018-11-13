using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextLoad : MonoBehaviour {

    public string fileName;
    private string dataPath;
    private string printData;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, fileName);

        printData = LoadCharacter(dataPath);
    }

    void Update()
    {
       //printData = LoadCharacter(dataPath);
    }

    /*
    static void SaveCharacter(CharacterData data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }
    */

    static string LoadCharacter(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();

            Debug.Log(jsonString);

            return JsonUtility.FromJson<string>(jsonString);
        }
    }
}
