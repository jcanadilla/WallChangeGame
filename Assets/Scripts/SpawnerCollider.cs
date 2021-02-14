using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCollider : MonoBehaviour
{

    public SectionController sectionController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sectionController.SpawnNewSection(other.gameObject);
        }
    }
}
