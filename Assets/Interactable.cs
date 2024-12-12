using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool grabbed = false, canGrab = false;
    [SerializeField]
    Transform parentTransform, handTransform;

    Vector3 OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrab)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                transform.parent = handTransform;
            }
            else
            {
                transform.parent = parentTransform;
                transform.localPosition = OriginalPos;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.transform.name);
            MoveWhenGrabbed(other);
            //canGrab = true;
            //OriginalPos = other.transform.localPosition;
            //parentTransform = other.transform.parent;
            //handTransform = other.transform;


        }

    }


    void MoveWhenGrabbed(Collider other)
    {
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log(other.transform.name + "gRABBING");
            transform.position = other.transform.position;
        }
    }


}
