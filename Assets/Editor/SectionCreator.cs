using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SectionCreator : EditorWindow
{

    public static List<GameObject> objetos;
    public static string prefabName;

    public static int numFilas = 20;

    public static int[] columna1;
    public static int[] columna2;
    public static int[] columna3;
    public static int[] columna4;
    public static int[] columna5;

    public static bool initialized = false;

    private static string prefabScenePath = "Assets/Editor/PrefabScene.unity";
    private static string prefabSavedPath = "Assets/Prefabs/Secciones";

    private static Vector3 columna1Position = new Vector3(-2, 0, 0);
    private static Vector3 columna2Position = new Vector3(-1, 0, 0);
    private static Vector3 columna3Position = new Vector3(0, 0, 0);
    private static Vector3 columna4Position = new Vector3(1, 0, 0);
    private static Vector3 columna5Position = new Vector3(2, 0, 0);

    [MenuItem("Component/Section creator")]
    public static void Init()
    {
        Debug.Log("INIT!");

        Initialize();

        EditorWindow.GetWindow(typeof(SectionCreator));
    }


    public static void Initialize()
    {
        prefabName = "";
        objetos = new List<GameObject>();
        columna1 = new int[numFilas];
        columna2 = new int[numFilas];
        columna3 = new int[numFilas];
        columna4 = new int[numFilas];
        columna5 = new int[numFilas];
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
        int newCount = Mathf.Max(2, EditorGUILayout.IntField("Objetos", objetos.Count));
        while (newCount < objetos.Count)
            objetos.RemoveAt(objetos.Count - 1);
        while (newCount > objetos.Count)
            objetos.Add(null);
        for (int i = 0; i < objetos.Count; i++)
        {
            objetos[i] = (GameObject)EditorGUILayout.ObjectField(objetos[i], typeof(GameObject), true);
        }

        /**
         * Construimos las 5 columnas y las 20 filas
         */
        GUILayout.Label("\n\nConfiguración de la sección");

        for (int i = numFilas - 1; i >= 0; i--)
        {
            Rect r = EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Bloque " + (i + 1));
            columna1[i] = EditorGUILayout.IntField(columna1[i] != 0 ? columna1[i] : 0);
            columna2[i] = EditorGUILayout.IntField(columna2[i] != 0 ? columna2[i] : 0);
            columna3[i] = EditorGUILayout.IntField(columna3[i] != 0 ? columna3[i] : 0);
            columna4[i] = EditorGUILayout.IntField(columna4[i] != 0 ? columna4[i] : 0);
            columna5[i] = EditorGUILayout.IntField(columna5[i] != 0 ? columna5[i] : 0);
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Label("\nOpciones del prefab");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Nombre de la sección:");
        prefabName = EditorGUILayout.TextField(prefabName);
        EditorGUILayout.EndHorizontal();


        // Botón para crear!
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Crear prefab"))
        {
            CrearPrefabButton();
        }
        GUILayout.Button("Cargar prefab");
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
        GameObject root = new GameObject("root");
        root.name = "root";

        for (int i = 0; i < numFilas; i++)
        {
            InstanciarObjetosColumna(i, columna1, objetos, columna1Position, root);
            InstanciarObjetosColumna(i, columna2, objetos, columna2Position, root);
            InstanciarObjetosColumna(i, columna3, objetos, columna3Position, root);
            InstanciarObjetosColumna(i, columna4, objetos, columna4Position, root);
            InstanciarObjetosColumna(i, columna5, objetos, columna5Position, root);
        }

//         Instantiate(objetos[1], root.transform);

        // Guardamos el prefab
        bool saved = false;
        PrefabUtility.SaveAsPrefabAsset(root, prefabSavedPath + "/" + prefabName + ".prefab", out saved);
        if(saved)
        {
            Debug.Log("PREFAB " + prefabName + " GUARDADO CORRECTAMENTE");
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
