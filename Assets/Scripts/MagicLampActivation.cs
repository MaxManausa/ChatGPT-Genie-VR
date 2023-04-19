using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicLampActivation : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable magicLampGrabInteractable;
    [SerializeField] private GameObject leftHandController;
    [SerializeField] private GameObject[] wallsToAddRigidbody;
    [SerializeField] private GameObject table;
    [SerializeField] private Animator genie;
    
    private bool hasBeenActivated = false;
    
    private void Update()
    {
        if (magicLampGrabInteractable.isSelected && !hasBeenActivated && leftHandController.activeInHierarchy)
        {
            XRBaseInteractor interactor = magicLampGrabInteractable.selectingInteractor;

            if (interactor != null && interactor.gameObject == leftHandController)
            {
                hasBeenActivated = true;

                foreach (GameObject wall in wallsToAddRigidbody)
                {
                    Rigidbody wallRigidbody = wall.AddComponent<Rigidbody>();
                    wallRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    wallRigidbody.AddForce(transform.forward * 500f);
                    Destroy(wall, 3f);
                }
                Destroy(table, 5f);
                genie.gameObject.SetActive(true);
            }
        }
    }
}