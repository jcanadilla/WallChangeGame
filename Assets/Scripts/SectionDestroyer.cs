using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDestroyer: MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Section"))
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
