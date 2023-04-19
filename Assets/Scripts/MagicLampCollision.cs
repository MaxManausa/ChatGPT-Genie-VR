using System.Collections.Generic;
using UnityEngine;

public class MagicLampCollision : MonoBehaviour
{
    [SerializeField] private List<GameObject> wallsToAddRigidbody;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == leftHand || collision.gameObject == rightHand)
        {
            Debug.Log("Collision detected");
            
            foreach (GameObject wall in wallsToAddRigidbody)
            {
                Rigidbody wallRigidbody = wall.AddComponent<Rigidbody>();
                wallRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                wallRigidbody.AddForce(transform.forward * 500f);
                Destroy(wall, 3f);
            }
        }
    }
}
