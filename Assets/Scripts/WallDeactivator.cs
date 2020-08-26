using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeactivator : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LeftWall" || other.gameObject.tag == "RightWall")
        {
        other.gameObject.SetActive(false);
        }
    }
}
