using UnityEngine;

public abstract class PortalLogic
{
    public Transform destination;
    public bool isLocation;
    public float distance = 0.2f;
    bool transitionEffect = false;



    public PortalLogic(bool isLocation, float distance) {
        this.isLocation = isLocation;
        this.distance = distance;
        
    
    }

    public void toTeleport(string toTag, string nextTag) {

        if (isLocation == false)
        {
            destination = GameObject.FindGameObjectWithTag(toTag).transform;
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag(nextTag).transform;
        }

        transitionEffect = false;

    }

    public void teleportTransition() {
        GameManager.instance.animator.SetTrigger("START");

    }



}
