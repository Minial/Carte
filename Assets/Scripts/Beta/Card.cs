using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Card : NetworkBehaviour
{

    // Use this for initialization
    [SyncVar] public int numero = 0;//            1 -> 13(roi)
    [SyncVar] public string signe = "N/A";//      Carreau / Coeur / Pique / Trèfle
    [SyncVar] public string TypeCarte = "N/A";
    [SyncVar] public Sprite image;
    [SyncVar] public SpriteRenderer Face;//la face de la carte
    [SyncVar] public bool EnMain = false;
    [SyncVar] public bool Select = false;//si on a cliqué sur la carte

    public Card(int numero_, string signe_, Sprite img)
    {
        numero = numero_;
        signe = signe_;
        image = img;
    }

    void Awake()
    {
        Face = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Face.sprite = image;
    }

    private void OnMouseUpAsButton()
    {
        if (EnMain)
        {
            this.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1.1f, 1.1f, 1);//vas de pair avec le selectcarte de scriptmainjoueur
            Select = true;
            //a finir + tard
        }
    }
}
