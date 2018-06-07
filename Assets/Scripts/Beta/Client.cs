using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : NetworkBehaviour
{
    public bool awake = false;
    public bool start = false;
    public static int NbreClient = 0;
    [SyncVar]
    public bool update = false;
    [SyncVar]
    public bool random = false;
    public Sprite another;
    public Sprite temp;

    private void Awake()
    {
        awake = true;
        NbreClient += 1;
        this.transform.position = new Vector3(NbreClient, 0, 0);
        if (NbreClient == 1)
        {
            this.tag = "TagJoueurN1";
        }
        else if (NbreClient == 2)
        {
            this.tag = "TagJoueurN2";
        }
        temp = this.GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        start = true;
    }

    private void Update()
    {
        if (update)
        {
            if (random)
            {
                this.GetComponent<SpriteRenderer>().sprite = another;
                this.transform.position = new Vector3(NbreClient, -1, 0);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = temp;
                this.transform.position = new Vector3(NbreClient, 1, 0);
            }
        }
    }
    
    public void OnMouseUpAsButton()
    {
        if (!isLocalPlayer)
        {
            //return;
        }
        //random = true;
        //this.GetComponent<SpriteRenderer>().sprite = another;
        CmdTravel();
    }
    [Command]
    public void CmdTravel()
    {
        random = !random;
        update = true;
    }
}