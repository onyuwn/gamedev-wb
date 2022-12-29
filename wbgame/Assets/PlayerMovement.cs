using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform playerMesh;
    public Transform lookDir;
    public Animator anim;

    private bool isSneaking = false;
    private int jumpCount = 0;
    private bool canJump = false;
    private bool isGrounded = false;
    private float translationX, translationZ;
    private float straffeX, straffeZ;
    private Quaternion _lookRotation;
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit groundHit;
        Debug.DrawRay(transform.position, -transform.up * .25f, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out groundHit, .25f))
        {
            Debug.Log("Hit");
            if (groundHit.transform.gameObject.tag == "ground")
            {
                jumpCount = 0;
                Debug.Log("Grounded");
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

        }
        else
        {
            isGrounded = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = speed/2;
            isSneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(jumpCount < 1)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * 300);
                jumpCount++;
            }

        }
        translationX = Input.GetAxis("Vertical") * speed * Mathf.Sin(Mathf.Deg2Rad * _lookRotation.eulerAngles.y);
        translationZ = Input.GetAxis("Vertical") * speed * Mathf.Cos(Mathf.Deg2Rad * _lookRotation.eulerAngles.y);
        straffeX = Input.GetAxis("Horizontal") * speed * Mathf.Cos(Mathf.Deg2Rad * _lookRotation.eulerAngles.y);
        straffeZ = Input.GetAxis("Horizontal") * speed * Mathf.Sin(Mathf.Deg2Rad * _lookRotation.eulerAngles.y);
        _lookRotation = lookDir.rotation;
        // Debug.Log($"rot: {_lookRotation.eulerAngles.y} translation: {translation} straffe: {straffe}");

        if(translationX != 0 || straffeX != 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, _lookRotation.eulerAngles.y, 0);
            anim.SetBool("walking", true);
            transform.Translate((translationX + straffeX) * Time.deltaTime, 0, (translationZ - straffeZ) * Time.deltaTime);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
}
