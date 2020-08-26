using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[System.Serializable]
public class Section
{
    public string prefabName = "D";
    public int numFilas = 20;

    public List<GameObject> objetos;

    public int[] columna1;
    public int[] columna2;
    public int[] columna3;
    public int[] columna4;
    public int[] columna5;

    public Vector3 columna1Position;
    public Vector3 columna2Position;
    public Vector3 columna3Position;
    public Vector3 columna4Position;
    public Vector3 columna5Position;

    public Section()
    {
        objetos = new List<GameObject>();

        columna1 = new int[numFilas];
        columna2 = new int[numFilas];
        columna3 = new int[numFilas];
        columna4 = new int[numFilas];
        columna5 = new int[numFilas];

        columna1Position = new Vector3(-2, 0, 0);
        columna2Position = new Vector3(-1, 0, 0);
        columna3Position = new Vector3(0, 0, 0);
        columna4Position = new Vector3(1, 0, 0);
        columna5Position = new Vector3(2, 0, 0);
    }
}

