using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            //detect collision with player.
            ItemControl.instance.getQuality(other.gameObject);
            ItemControl.instance.addItem();
            Destroy(gameObject);
        }
    }
}
