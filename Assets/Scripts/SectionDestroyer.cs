using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDestroyer: MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Section")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
