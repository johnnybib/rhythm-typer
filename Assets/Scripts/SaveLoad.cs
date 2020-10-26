using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
 
public static class SaveLoad 
{
    public static event Action SettingsChanged = delegate { };
    public static Settings settings;
    public static void SaveSettings() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/settings.gd");
        bf.Serialize(file, settings);
        file.Close();
        SettingsChanged();
    } 
    public static void Load() 
    {
        if(File.Exists(Application.persistentDataPath + "/settings.gd")) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.gd", FileMode.Open);
            settings = (Settings)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            settings = new Settings();
        }
    }
}