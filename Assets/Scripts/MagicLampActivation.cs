using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicLampActivation : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable magicLampGrabInteractable;
    [SerializeField] private GameObject magicLamp;
    
    [SerializeField] private GameObject leftHandController;
    [SerializeField] private GameObject rightHandController;
    
    [SerializeField] private GameObject[] wallsToAddRigidbody;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject table;
    
    [SerializeField] private Animator genie;
    [SerializeField] private float delay = 6f;
    
    //[SerializeField] private XRRayInteractor leftRayInteractor;
    //[SerializeField] private XRRayInteractor rightRayInteractor;
    
    
    private bool hasBeenActivated = false;
    
    
    private void Update()
    {
        if (magicLampGrabInteractable.isSelected && !hasBeenActivated)
        {
            XRBaseInteractor interactor = magicLampGrabInteractable.selectingInteractor;

            if (interactor != null && interactor.gameObject == leftHandController)
            {
                hasBeenActivated = true;
                
                foreach (GameObject wall in wallsToAddRigidbody)
                {
                    Rigidbody wallRigidbody = wall.AddComponent<Rigidbody>();
                    wallRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    wallRigidbody.AddForce(transform.forward * 300f);
                    Destroy(wall, 3f);
                }

                Invoke("FloorFall", 3f);
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
        Destroy(magicLamp);
    }
    
}