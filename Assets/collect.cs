using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    public Gem[] collectibles;
    public Transform handLocation;

    // Update is called once per frame
    void Update()
    {
        foreach (Gem collectible in collectibles)
        {
            float dist = Vector3.Distance(handLocation.position, collectible.transform.position);
            if  (dist < 1)
            {
                collectible.collectionEvent.Invoke();
            }
            
        }
    }
}
