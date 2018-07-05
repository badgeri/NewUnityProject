using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public bool onHold = false;
    //private bool beenHit = false;
    //private bool zOffsetSet = false;
    private float zOffset = 0;
    private float liftHeight { get { return 5 + zOffset; } }
    private Vector3 oldPosition;
    public Playground grid;
    

	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
        grid = GameObject.FindWithTag("Playground").GetComponent<Playground>();
	}

	// Update is called once per frame
	void Update () {
        if (onHold)
        {
            if (!Input.GetMouseButtonUp(0))
            {
                PickupObject();
                //print("not Mouse up");
            }
            else
            {
                //print("mouse up");
                onHold = false;
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 100.0f);
                if (hit)
                    transform.SetPositionAndRotation(hitInfo.point + Vector3.up * zOffset, transform.rotation);
                //in case something went wrong
                else
                    transform.SetPositionAndRotation(transform.position - Vector3.up * liftHeight, transform.rotation);

                gameObject.isStatic = true;
                zOffset = 0;

                grid.UpdateGrid(gameObject);

                oldPosition = transform.position;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (IsPickable(out hitInfo))
            {
                int tmpMask = gameObject.layer;
                gameObject.layer = ExtendLayerMask.GhostMask;
                grid.UpdateGrid(gameObject);
                gameObject.layer = tmpMask;
                PickupObject();
            }
        }
    }

    private bool IsPickable(out RaycastHit hitInfo)
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hitInfo.transform.gameObject.layer == ExtendLayerMask.UI) return false;
        if (hitInfo.transform.gameObject == gameObject) return true;
        return false;
    }

    private void PickupObject()
    {
        gameObject.isStatic = false;

        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 100.0f);
        if (hit && !onHold)
        {
            zOffset = (transform.position - hitInfo.transform.position).y;
        }
        onHold = true;
        if (onHold)
        {
            //Have a hitpoint on ground so we lift the object
            //print("onHold " + liftHeight);
            transform.SetPositionAndRotation(hitInfo.point + Vector3.up * liftHeight, transform.rotation);
        }
    }
}
