using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject parcelPrefab;
    public GameObject parcelSpawPointAnchor;
    public Transform currentTarget;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public void ThrowNextParcel()
    {
        if (currentTarget !=null)
        {

            if (GetWaitingParcelCount() == 0)
            {
                Debug.Log("No parcel!");
            }
            else
            {
                Debug.Log("ThrowNextParcel!");

                Transform parcel = parcelSpawPointAnchor.transform.GetChild(0);
                parcel.SetParent(null, worldPositionStays: true);

                Launch(parcel, currentTarget);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateParcels();
    }

    public void UpdateParcels()
    {
        if (GetWaitingParcelCount() == 0)
        {
            AddNewParcel();
        }
    }
        

    public int GetWaitingParcelCount()
    {
        return parcelSpawPointAnchor.transform.childCount;
    }

    public void AddNewParcel()
    {
        GameObject parcel = Instantiate(parcelPrefab);

        parcel.SetActive(false);
        parcel.transform.SetParent(parcelSpawPointAnchor.transform, false);
        parcel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        StartCoroutine(ShowNewParcel(parcel));
    }

    IEnumerator ShowNewParcel(GameObject parcel)
    {
        yield return new WaitForSeconds(1.0f);

        parcel.SetActive(true);

    }

    /// <summary>
    /// Source: https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/
    /// </summary>
    /// <param name="_bullseye"></param>

    public void Launch(Transform parcel, Transform _bullseye, float _angle = 60.0f)
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(parcel.position.x, parcel.position.y, parcel.position.z);
        Vector3 targetXZPos = new Vector3(_bullseye.position.x, parcel.position.y, _bullseye.position.z);

        // rotate the object to face the target
        parcel.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(_angle * Mathf.Deg2Rad);
        float H = _bullseye.position.y - parcel.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(Mathf.Abs(G * R * R / (2.0f * (H - R * tanAlpha))));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = parcel.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        parcel.gameObject.GetComponent<Rigidbody>().velocity = globalVelocity;
        //bTargetReady = false;
    }
}
