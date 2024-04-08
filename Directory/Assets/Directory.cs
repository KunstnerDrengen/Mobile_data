using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Xml.Serialization;
using Unity.VisualScripting;
using System.Text;


public class DataManager : MonoBehaviour
{
    private string _dataPath;
    private string _xmlMembers;
    private string _jsonMembers;


    private List<People> memberList = new List<People>
    {
        new People("Clara",1998,"Black"),
        new People("Astrid",2003,"Orange"),
        new People("Benjamin",1998,"Blue"),
        new People("Oliver", 2001,"Purple"),
        new People("Christoffer",2002,"Yellow")
    };
    
    void Awake()
    {
        _dataPath = "AndroidManifest.xml";
        Debug.Log(_dataPath);

        _xmlMembers = _dataPath + "MembersData.xml";
        _jsonMembers = _dataPath + "MembersData.json";
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        NewDirectory();
        CreateXML();
        ConvertToXML();
        TOJSON();
    }
    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }
        //New directory is created at _datapath
        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created!");
    }
    public void CreateXML()
    {
        // Using xmlSerializer. An instance is created that passes the type of data, which is going to be translated.
        var xmlSerializer = new XmlSerializer(typeof(List<People>));

        // Here a filestream is used. This wraps it in a code block and closes it
        using (FileStream stream = File.Create(_xmlMembers))
        {
            xmlSerializer.Serialize(stream, memberList);
        }
    }
    public void ConvertToXML()
    {
        // We find/check the file if it exist. 
        if (File.Exists(_xmlMembers))
        {
            // Here we do the same as above
            var xmlSerializer = new XmlSerializer(typeof(List<People>));

            //The same here, but here we openread instead of creating a file. 
            using (FileStream stream = File.OpenRead(_xmlMembers))
            {
                // Here a variable is created to hold the deserialized values.  
                var members = (List<People>)xmlSerializer.Deserialize(stream);

                //Here we write to the debuglog. to print which values it finds. 
                foreach (var Members in members)
                {
                    Debug.LogFormat(Members.name, Members.year, Members.color);
                }
            }
        }
    }
    public void TOJSON()
    {
        //Here we create a new list to work with, from what we already have. 
        People.MemberList group = new People.MemberList();
        group.list = memberList;
        
        // Here a variable is declared to hold the translated Json data when its formatted
        string jsonString = JsonUtility.ToJson(group, true);

        //Here we pass the Json/list filename
        using (StreamWriter stream = File.CreateText(_jsonMembers))
        {
            // The WriteLine pass the Json value, and writes the file. 
            stream.WriteLine(jsonString);
        }
    }
    //Hello Olga. If you read this i have a question. 
    //I get both files on my computer (the json and XML), but the JSON is created from the list, not the XML.
    //Im not sure how you achive it. The book dosent tell either. 
}