using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    public AudioClip cheers;

    public AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            audioSource[1].PlayOneShot(audioSource[1].clip);

        playPause = audioSource[1].isPlaying;

        Debug.Log(playPause);
    }

    private bool playPause;

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name + " " + other.gameObject.tag);

        if (other.gameObject.tag == "Sleigh")
        {
            if (transform.parent == null)
            {
                transform.parent = other.gameObject.transform;
            }
        }

        //if (other.gameObject.tag == "Untagged")
        //{
        //    if (!playPause)
        //    {
        //        audioSource[1].Play();
        //        Debug.Log("Play " + Time.time);
        //        playPause = true;
        //     }

        
        //}
    }


    private void OnTriggerEnter(Collider other)
    {

       
        if (other.tag == "Chimney")
        {

            //GetComponent<Rigidbody>().isKinematic = true;  

            transform.position = other.transform.parent.position; // should be base of house

            audioSource[0].Play();

            //AudioSource.PlayClipAtPoint(cheers, other.transform.parent.position, 2); // no spatializing ?      
        }

       
      

    }
}
