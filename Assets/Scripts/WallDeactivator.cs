using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeactivator : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LeftWall") || other.gameObject.CompareTag("RightWall"))
        {
        other.gameObject.SetActive(false);
        }
    }
}
