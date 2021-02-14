using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SectionCreator : EditorWindow
{
    // Seccion
    public static Section section; 

    // JSONManager para gestionar el JSON de la sección
    public static SectionJSONManager sectionJSONManager;

    public static bool initialized = false;

    private static string prefabScenePath = "Assets/Editor/PrefabScene.unity";
    private static string prefabSavedPath = "Assets/Prefabs/Secciones";

    [MenuItem("Component/Section creator")]
    public static void Init()
    {
        Debug.Log("INIT!");

        Initialize();

        EditorWindow.GetWindow(typeof(SectionCreator));
    }


    public static void Initialize()
    {
        section = new Section();
        sectionJSONManager = new SectionJSONManager();
        initialized = true;
    }

    void OnGUI()
    {
        if (!initialized)
        {
            Initialize();
        }

        GUILayout.Label("Generador de secciones");

        /**
         * Creamos la seccion para definir los diferentes objetos 
         */
        int newCount = Mathf.Max(2, EditorGUILayout.IntField("Objetos", section.objetos.Count));
        
        // Botones añadir/quitar
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Añadir"))
        {
            newCount += 1;
        }
        if (GUILayout.Button("Quitar"))
        {
            newCount -= 1;
        }
        EditorGUILayout.EndHorizontal();
         
        while (newCount < section.objetos.Count)
            section.objetos.RemoveAt(section.objetos.Count - 1);
        while (newCount > section.objetos.Count)
            section.objetos.Add(null);
        for (int i = 0; i < section.objetos.Count; i++)
        {
            section.objetos[i] = (GameObject)EditorGUILayout.ObjectField(section.objetos[i], typeof(GameObject), true);
        }

        /**
         * Construimos las 5 columnas y las 20 filas
         */
        GUILayout.Label("\n\nConfiguración de la sección");

        for (int i = section.numFilas - 1; i >= 0; i--)
        {
            Rect r = EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Bloque " + (i + 1));
            section.columna1[i] = EditorGUILayout.IntField(section.columna1[i] != 0 ? section.columna1[i] : 0);
            section.columna2[i] = EditorGUILayout.IntField(section.columna2[i] != 0 ? section.columna2[i] : 0);
            section.columna3[i] = EditorGUILayout.IntField(section.columna3[i] != 0 ? section.columna3[i] : 0);
            section.columna4[i] = EditorGUILayout.IntField(section.columna4[i] != 0 ? section.columna4[i] : 0);
            section.columna5[i] = EditorGUILayout.IntField(section.columna5[i] != 0 ? section.columna5[i] : 0);
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Label("\nOpciones del prefab");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Nombre de la sección:");
        section.prefabName = EditorGUILayout.TextField(section.prefabName);
        EditorGUILayout.EndHorizontal();


        // Botón para crear!
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Crear prefab"))
        {
            CrearPrefabButton();
        }
        if(GUILayout.Button("Cargar prefab"))
        {
            CargarPrefabButton();
        }
        if (GUILayout.Button("Reset"))
        {
            ResetButton();
        }
        EditorGUILayout.EndHorizontal();
    }

    public static void CrearPrefabButton()
    {
        Debug.Log("Crear Prefab pulsado!");

        if (GuardarScene())
        {
            CrearPrefab();
        }
        else
        {
            Debug.Log("PREFAB NO GENERADO! HAY QUE GUARDAR LA SCENA PRIMERO");
        }
    }

    public static void CargarPrefabButton()
    {
        string path = EditorUtility.OpenFilePanel("", prefabSavedPath, "json");
        if (path.Length != 0)
        {
            section = sectionJSONManager.CargarSection(path);
        }
    }

    public static void ResetButton()
    {
        Initialize();
    }

    public static bool GuardarScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = path[path.Length - 1];
            bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
            Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
            return true;
        }
        else
        {
            Debug.Log("Don't save Scene(s)");
            return false;
        }
    }

    public static void CrearPrefab()
    {
        // Obtenemos la escena anterior
        Scene scenePrevia = EditorSceneManager.GetActiveScene();
        string scenePath = scenePrevia.path;
        Debug.Log("ESCENA ACTUAL: " + scenePrevia.name + " PATH: " + scenePath);

        // Cambiamos de escena para crear el prefab
        EditorSceneManager.OpenScene(prefabScenePath);

        // Creamos el objeto raiz del prefab
        GameObject root = new GameObject();
        root.name = "root";
        root.tag = "SectionRoot";

        // Buscamos el objeto Section
        GameObject sectionCollider = GameObject.Find("Section");
        sectionCollider.transform.SetParent(root.transform);

        // Buscamos el objeto spawner.
        // Este objeto es el que controla cuando generar una nueva sección
        GameObject spawner = GameObject.Find("Spawner");
        spawner.transform.SetParent(root.transform);

        for (int i = 0; i < section.numFilas; i++)
        {
            InstanciarObjetosColumna(i, section.columna1, section.objetos, section.columna1Position, root);
            InstanciarObjetosColumna(i, section.columna2, section.objetos, section.columna2Position, root);
            InstanciarObjetosColumna(i, section.columna3, section.objetos, section.columna3Position, root);
            InstanciarObjetosColumna(i, section.columna4, section.objetos, section.columna4Position, root);
            InstanciarObjetosColumna(i, section.columna5, section.objetos, section.columna5Position, root);
        }

//         Instantiate(objetos[1], root.transform);

        // Guardamos el prefab
        bool saved = false;
        PrefabUtility.SaveAsPrefabAsset(root, prefabSavedPath + "/" + section.prefabName + ".prefab", out saved);
        if(saved)
        {
            sectionJSONManager.GuardarSection(prefabSavedPath + "/" + section.prefabName + ".json", section);
            Debug.Log("PREFAB " + section.prefabName + " GUARDADO CORRECTAMENTE");
            DestroyImmediate(root);
            EditorSceneManager.OpenScene(scenePath);
        }
    }

    private static void InstanciarObjetosColumna(int i, int[] columna, List<GameObject> objetos, Vector3 posicion, GameObject root)
    {
        int numGoColumna = columna[i];
        if (numGoColumna != 0)
        {
            Instantiate<GameObject>(objetos[numGoColumna], new Vector3(posicion.x, i, 0), Quaternion.identity, root.transform);
        }
    }
}
