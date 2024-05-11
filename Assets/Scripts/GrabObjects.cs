using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private Transform rayPoint;
    [SerializeField]
    private RandObjectCreator objGenerator;
    
    [SerializeField]
    private float rayDistance;
    private GameObject grabbedObject;
    private int layerIndex;
    public bool storagePoint;
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
        storagePoint = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        float maxDistance = 2f;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.forward);
        UnityEngine.Debug.DrawRay(ray.origin, ray.direction *maxDistance, Color.green);
        if(Physics.Raycast(ray, out hit, maxDistance) && hit.collider.CompareTag("Item")){
            // Grab an object
            if(grabbedObject==null  && storagePoint==false){
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position + new Vector3(0,0,-1);
                grabbedObject.transform.SetParent(transform);
            }
            // Release an object
            else if (storagePoint && grabbedObject != null)
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                Destroy(grabbedObject, 5.0f);
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
                objGenerator.repetitor();
            }
            
        }

    }

    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<Storage>(out Storage goal)){
            storagePoint = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.TryGetComponent<Storage>(out Storage goal)){
            
            storagePoint = false;
        }
    }
}