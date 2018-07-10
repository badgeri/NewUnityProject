using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public bool onHold = false;
    private float zOffset = 0;
    private float liftHeight { get { return 5 + zOffset; } }
    public Playground grid;

    private RaycastHit hitInfo = new RaycastHit();

	// Use this for initialization
	void Start () {
        grid = GameObject.FindWithTag("Playground").GetComponent<Playground>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (onHold && !Input.GetMouseButtonUp(0)))
        {
            if (IsPickable())
            {
                if (!onHold) // first lift, clean the grid at objects location
                {
                    int tmpMask = gameObject.layer;
                    gameObject.layer = ExtendLayerMask.GhostMask();
                    grid.UpdateGrid(gameObject);
                    gameObject.layer = tmpMask;
                    zOffset = (transform.position - hitInfo.transform.position).y;
                    onHold = true;
                    gameObject.isStatic = false;
                }
                PickupObject(Vector3.up * liftHeight);
            }
        }
        else if (onHold) // Object is no longer under control, put it down
        {
            if (IsPickable())
            {
                PickupObject(Vector3.up * zOffset);
                onHold = false;

                gameObject.isStatic = true;
                zOffset = 0;

                grid.UpdateGrid(gameObject);
            }
        }
    }

    private bool IsPickable()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            if (hitInfo.transform.gameObject.layer == ExtendLayerMask.UI()) return false;
            if (hitInfo.transform.gameObject == gameObject)
            {
                return Physics.Raycast(hitInfo.point, Vector3.down, out hitInfo, 100.0f);
            }
            return onHold;
        }
        return false;
    }

    private void PickupObject(Vector3 lift)
    {
        transform.SetPositionAndRotation(hitInfo.point + lift, transform.rotation);
    }
}
