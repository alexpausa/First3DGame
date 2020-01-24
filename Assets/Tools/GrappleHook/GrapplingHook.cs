using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    RaycastHit hit;

    public float grappleRange = 10;
    public float playerTravelSpeed = 20;
    bool held = false;

    LineRenderer LR = new LineRenderer();

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject player;
    private PlayerMovement PM;

    private float initialDistance;
    private Rigidbody rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        PM = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        Inputs();

    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            held = true;
            shoot();
        }
        if (held && PM.grappled)
        {
            float magn;

            Vector3 direction = hit.transform.position - cam.transform.position;
            Vector3 pos = cam.transform.position;
            pos.y = 0;

            float angle = Vector3.Angle(direction, Vector3.up);
            

            if (angle <= 90 && angle >= 0)
            {
                if (angle > 1) {
                    magn = (100 / (Mathf.Abs(angle)) + 10);
                } else
                {
                    magn = 100;
                }
            }
            else
            {
                magn = 0;
            }

            rb.AddForce(((direction)/initialDistance) * playerTravelSpeed * Time.deltaTime * 80);
            rb.AddForce(Vector3.up * magn * 100 * Time.deltaTime);

        }
        if (Input.GetButtonUp("Fire1"))
        {
            PM.grappled = false;
            held = false;
        }

    }

    public void shoot()
    {

        PM.grappled = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grappleRange, mask);
        initialDistance = hit.distance;
        if (PM.grappled)
        {
            // add line renderer and change the position of the start of the line
            // also look up how to animate the line
        }
    }
}
