using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptCarteNetwork : NetworkBehaviour
{

    // Use this for initialization
    public int numero = 0;//            1 -> 13(roi)
    public string signe = "N/A";//      Carreau / Coeur / Pique / Trèfle
    public string TypeCarte = "N/A";
    public Sprite image;
    public SpriteRenderer Face;//la face de la carte
    public bool EnMain = false;
    public bool Select = false;//si on a cliqué sur la carte

    public ScriptCarteNetwork(int numero_, string signe_, Sprite img)
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
