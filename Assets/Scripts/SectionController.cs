using Shapes2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    public int level = 0;
    public List<GameObject> easySections;
    public List<GameObject> normalSections;
    public List<GameObject> hardSections;

    
    public List<GameObject> InitializeSectionsPool(List<GameObject> sections)
    {
        List<GameObject> sectionPool = new List<GameObject>();
        for (int i = 0; i < sections.Count; i++)
        {
            GameObject section = Instantiate<GameObject>(sections[i], this.transform);
            section.SetActive(false);
            
            Transform spawner = section.transform.Find("Spawner");
            spawner.GetComponent<SpawnerCollider>().sectionController = this;
            sectionPool.Add(section);
        }
        return sectionPool;
    }

    public void SpawnNewSection (GameObject player)
    {
        switch (level)
        {
            case 0: 
                SpawnSection(easySections, player);
                break;
            case 1:
                SpawnSection(normalSections, player);
                break;
            case 2:
                SpawnSection(hardSections, player);
                break;
            default:
                break;
        }
    }

    private void SpawnSection(List<GameObject> sections, GameObject player) 
    {
        int sectionIndex = Random.Range(0, sections.Count - 1);

        Vector3 playerPosition = player.transform.position;
        Vector3 newSectionPosition = new Vector3(0, playerPosition.y + 10f, 0);

        GameObject section = Instantiate<GameObject>(sections[sectionIndex], this.transform);
        Transform spawner = section.transform.Find("Spawner");
        spawner.GetComponent<SpawnerCollider>().sectionController = this;

        section.SetActive(true);
        section.transform.position = newSectionPosition;

    }

}
