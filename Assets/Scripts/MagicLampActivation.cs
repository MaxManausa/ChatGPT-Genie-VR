using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicLampActivation : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable magicLampGrabInteractable;
    [SerializeField] private GameObject magicLamp;
    [SerializeField] private Transform newLampPosition;
    [SerializeField] private Transform lastLampPosition;
    
    [SerializeField] private GameObject leftHandController;
    [SerializeField] private GameObject rightHandController;
    
    [SerializeField] private GameObject[] wallsToAddRigidbody;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject table;
    
    [SerializeField] private Animator genie;
    [SerializeField] private float delay = 5f;
    
    //[SerializeField] private XRRayInteractor leftRayInteractor;
    //[SerializeField] private XRRayInteractor rightRayInteractor;
    
    
    private bool hasBeenActivated = false;

    private void Start()
    {
        rightHandController.GetComponent<XRInteractorLineVisual>().enabled = false;
        leftHandController.GetComponent<XRInteractorLineVisual>().enabled = false;
    }


    private void Update()
    {
        if (magicLampGrabInteractable.isSelected && !hasBeenActivated)
        {
            XRBaseInteractor interactor = magicLampGrabInteractable.selectingInteractor;

            if (interactor != null && interactor.gameObject == leftHandController)
            {
                hasBeenActivated = true;
                
                Destroy(ceiling);
                
                foreach (GameObject wall in wallsToAddRigidbody)
                {
                    Rigidbody wallRigidbody = wall.AddComponent<Rigidbody>();
                    wallRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    wallRigidbody.AddForce(transform.forward * 300f);
                    Destroy(wall, 3f);
                }
                
                Invoke("LampRelease",2f);
                Invoke("FloorFall", 2f);
                Invoke("ActivateGenie", delay);
            }
        }
    }
    private void FloorFall()
    {
        Rigidbody floorRigidbody = floor.AddComponent<Rigidbody>();
        floorRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        Destroy(floor, 3f);
        Rigidbody tableRigidbody = table.AddComponent<Rigidbody>();
        tableRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        Destroy(table, 3f);
    }
    
    private void ActivateGenie()
    {
        genie.gameObject.SetActive(true);
        magicLamp.GetComponentInChildren<ParticleSystem>().Stop();
        Invoke("FinalLampSpot",3f);
    }

    private void LampRelease()
    {
        magicLamp.transform.localScale = magicLamp.transform.localScale * 2.0f;
        magicLampGrabInteractable.enabled = false;
        magicLamp.GetComponent<Rigidbody>().isKinematic = true;
        magicLamp.GetComponent<Rigidbody>().useGravity = false;
        magicLamp.transform.position = newLampPosition.position;
        magicLamp.transform.rotation = newLampPosition.rotation;
        magicLamp.GetComponentInChildren<ParticleSystem>().Play();
    }

    private void FinalLampSpot()
    {
        magicLamp.transform.position = lastLampPosition.position;
        magicLamp.transform.rotation = lastLampPosition.rotation;
        
        // turns hand raycasts on
        rightHandController.GetComponent<XRInteractorLineVisual>().enabled = true;
        leftHandController.GetComponent<XRInteractorLineVisual>().enabled = true;
    }

}