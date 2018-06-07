using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : NetworkBehaviour
{
    public bool awake = false;
    public bool start = false;
    public static int NbreServer = 0;
    public bool update = false;
    public bool random = false;

    private void Awake()
    {
        awake = true;
        NbreServer += 1;
        this.transform.position = new Vector3(NbreServer, 1, 0);
    }

    private void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);//parce que sinon il est déplacé
        start = true;
    }

    private void Update()
    {
        update = true;
    }

    private void OnMouseUpAsButton()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        random = true;
    }
}