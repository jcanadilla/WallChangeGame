using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    [SerializeField] GameObject wallPrefab;
    [SerializeField] private int numberOfWalls = 10;
    // [SerializeField] private PlayerController player;

    private List<GameObject> wallsPool;
    private GameObject lastWallPositioned;

    private float originalX;
    private float originalY;
    private float originalZ;

    // Start is called before the first frame update
    private void Start()
    {
        originalX = this.transform.position.x;
        originalY = this.transform.position.y;
        originalZ = this.transform.position.z;
    }

    void Awake()
    {
        // Inicializamos la lista de bloques de la pared
        wallsPool = new List<GameObject>();

        // Generamos las paredes de la derecha
        for (int i = 0; i < numberOfWalls; i += 1)
        {
            GameObject obj = (GameObject)Instantiate(wallPrefab, this.transform);
            obj.SetActive(true);
            obj.name = "Wall " + i;
            obj.transform.Translate(0, obj.transform.position.y + (i * obj.transform.localScale.y), 0);
            wallsPool.Add(obj);
        }
        lastWallPositioned = this.wallsPool[wallsPool.Count - 1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject inactiveWall = GetWall();
        if (inactiveWall != null)
        {
            inactiveWall.SetActive(true);
            inactiveWall.transform.Translate(0, (lastWallPositioned.transform.position.y - inactiveWall.transform.position.y) + inactiveWall.transform.localScale.y, 0);
            lastWallPositioned = inactiveWall;
        }
    }

    // Obtiene un GameObject inactivo
    private GameObject GetWall()
    {
        for (int i = 0; i < numberOfWalls; i += 1)
        {
            if (!wallsPool[i].activeInHierarchy)
            {
                return wallsPool[i];
            }
        }
        return null;
    }

}
