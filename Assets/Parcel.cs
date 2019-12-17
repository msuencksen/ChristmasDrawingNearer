using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip cheers;

    // Start is called before the first frame update
    void Awake()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Chimney")
        {

            GetComponent<Rigidbody>().isKinematic = true;  

            transform.position = other.transform.parent.position; // should be base of house

            audioSource.Play();

            //AudioSource.PlayClipAtPoint(cheers, other.transform.parent.position, 2); // no spatializing ?
        
        }
    }
}
