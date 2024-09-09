using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoxColour : MonoBehaviour
{
    [SerializeField] private Material boxFinishColour;

    [SerializeField] private Material GreenFinishBox;
    [SerializeField] private Material RedFinishBox;

    private Renderer boxRenderer;
    
    //Put in naother script later
    public BoxCollider boxCollider;
    void Start()
    {
        boxRenderer = GetComponent<Renderer>();  
        boxFinishColour = RedFinishBox;          
        boxRenderer.material = boxFinishColour;

        //boxCollider.enabled = false;
    }

    public void UpdateBoxColour()
    {
        if (PlayerCollision.playerHasKey)
        {
            boxFinishColour = GreenFinishBox;
            //boxCollider.enabled = true;
        }
        else
        {
            boxFinishColour = RedFinishBox;
            //boxCollider.enabled = false;

        }

        boxRenderer.material = boxFinishColour;  
    }
}