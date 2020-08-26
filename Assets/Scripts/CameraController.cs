using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    private float originalX;
    private float originalY;
    private float originalZ;

    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;
        originalZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float cameraSpeed = player.MoveSpeed * Time.deltaTime;

        transform.position = new Vector3(originalX, player.transform.position.y, originalZ);
    }
}
