using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform playerMesh;
    public Transform lookDir;
    public Animator anim;

    private float translationX, translationZ;
    private float straffeX, straffeZ;
    private Quaternion _lookRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
