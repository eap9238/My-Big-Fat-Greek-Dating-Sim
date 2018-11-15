using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextLoad : MonoBehaviour
{

    public string fileName;
    private string dataPath;
    private string printData;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, fileName);

		//printData = LoadSaveData(dataPath);
		//printData = LoadJson(dataPath);
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
        using (StreamReader streamReader = new StreamReader(path))
        {
            string jsonString = streamReader.ReadToEnd();

			var formattedString = JsonUtility.FromJson<string>(jsonString);
			foreach (var item in formattedString)
			{
				Debug.Log (item);
			}

            Debug.Log(jsonString);
            //Debug.Log(formattedString);
            //Debug.Log(formattedString.Length);

            return formattedString;
        }
    }

	/*
	static string LoadSaveData(string path) {
		if (true) //save point exists
		{ 
			var savePointData = savePoints [savePoints.Count - 1];
			SavePointData.Decode (savePointData);

			return "Save File Found";
		} 
		else 
		{
			return "Empty Save";
		}
	}

	public class SaveData
	{
		public string version { get; set; }
		public string savePoints { get; set; }
	}
	*/
}
