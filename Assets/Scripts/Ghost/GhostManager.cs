using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public Material colourToKill;
    public Material defaultColour;

    public Transform spawnPointPrefab;
    
    public void InstantiateGhostGameObjects(List<GameObject> instantiatedGhosts, GameObject prefab, List<GhostData> ghostData, string tag, bool setColour)
    {
        for (int i = 0; i < ghostData.Count; i++)
        {
            GameObject ghost = CreateGhostSpawnLocation(prefab);
            if (setColour)
            { 
                SetGhostColour(ghost, i, ghostData.Count, tag);
            }
            
            instantiatedGhosts.Add(ghost);
        }
    }

    private GameObject CreateGhostSpawnLocation(GameObject prefab)
    {
        //SpawnPointPrefab an empty space in the game world. 
        GameObject ghost = Instantiate(prefab, spawnPointPrefab.transform.position, Quaternion.identity);

      //  Debug.Log($"Ghost spawned at: {ghost.transform.position}");
        
        return ghost;
    }

    private void SetGhostColour(GameObject ghost, int index, int totalCount, string tag)
    {
        Renderer cloneRenderer = ghost.GetComponentInChildren<Renderer>();

        //if (cloneRenderer != null) { Debug.LogWarning("cloneRender Not Found") ;return; }
        
        if (index == totalCount - 1)
        {
            cloneRenderer.material = colourToKill;
            ghost.tag = "GhostToKill";
        }
        else
        {
            cloneRenderer.material = defaultColour;
            //testing kill all ghosts 
            ghost.tag = "GhostToKill";

            //ghost.tag = tag;
        }
    }

    public void InstantiateGhostData(List<GameObject> InitialisedGhostData, List<GhostData> ghostDataList)
    {
        for (int i = 0; i < InitialisedGhostData.Count; i++)
        {
            GhostReplayData ghostMovementReplay = InitialisedGhostData[i].GetComponent<GhostReplayData>();
            
            if (ghostMovementReplay != null)
            {
                ghostMovementReplay.Initialize(ghostDataList[i]);
                ghostMovementReplay.isReplay = true;
            }
        }
    }
}
