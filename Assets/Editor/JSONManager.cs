using UnityEngine;
using UnityEditor;

public class SectionJSONManager
{
    
    public Section CargarSection(string path)
    {
        return JsonUtility.FromJson<Section>(System.IO.File.ReadAllText(path));
    }

    public void GuardarSection(string path, Section section)
    {
        string sectionJSON = JsonUtility.ToJson(section, true);
        System.IO.File.WriteAllText(path, sectionJSON);
    }

}