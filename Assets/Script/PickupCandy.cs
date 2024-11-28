using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCandy : MonoBehaviour
{
    public GameObject CandyOnPlayer;
    void Start()
    {
        CandyOnPlayer.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                CandyOnPlayer.SetActive(true);
            }
        }

    }


}
