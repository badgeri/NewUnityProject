using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public bool onHold = false;
    private bool beenHit = false;
    private bool zOffsetSet = false;
    private float zOffset = 0;
    private float liftHeight { get { return 5 + zOffset; } }
    private Vector3 oldPosition;
    public Playground grid;
    

	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
	}
	
                GameObject ghostMesh;
	// Update is called once per frame
	void Update () {
        if (onHold)
        {
            if (!Input.GetMouseButtonUp(0))
            {
                pickupObject();
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
            createGostMesh(gameObject, out ghostMesh);
            grid.UpdateGrid(ghostMesh);
            Destroy(ghostMesh);
            pickupObject();
        }
    }

    //store gameObject reference

    void createGostMesh(GameObject original, out GameObject ghostMesh)
    {
        //spawn object
        ghostMesh = new GameObject("Cool GameObject made from Code");
        ghostMesh.AddComponent<MeshCollider>();
        ghostMesh.layer = ExtendLayerMask.GhostMask;
        ghostMesh.transform.localScale = gameObject.transform.localScale;
        ghostMesh.transform.position = gameObject.transform.position + Vector3.back * 2;
        ghostMesh.transform.rotation = gameObject.transform.rotation;
        Mesh m = ((MeshFilter) original.gameObject.GetComponent("MeshFilter")).mesh;
        ((MeshCollider)ghostMesh.GetComponent<MeshCollider>()).sharedMesh = m;
//        ((MeshCollider)ghostMesh.GetComponent<MeshCollider>()).convex = true;
    }
        private void pickupObject()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            if (hitInfo.transform.GetInstanceID() == transform.GetInstanceID())
            {
                gameObject.isStatic = false;

                hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 100.0f);
                if (hit && !onHold)
                {
                    //print("reset zOffset ");
                    //print(hitInfo.transform.position);
                    //print(transform.position);
                    zOffset = (transform.position - hitInfo.transform.position).y;
                }
                onHold = true;
            }
            if (onHold)
            {
                //Have a hitpoint on ground so we lift the object
                //print("onHold " + liftHeight);
                transform.SetPositionAndRotation(hitInfo.point + Vector3.up * liftHeight, transform.rotation);
            }
        }
    }
}
