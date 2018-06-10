using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hand : NetworkBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    public static int NbreJoueur = 0;
    public GameObject Deck; 
    [SyncVar] public int TailleMainMax = 5;
    [SyncVar] public int TailleMainActuel = 0;//
    [SyncVar] public bool RecupCarte = false;
    [SyncVar] public GameObject CarteARecup;
    [SyncVar] public int NumeroDuJoueur = 0;
    [SyncVar] public float cos;
    [SyncVar] public float sin;
    [SyncVar] public double timer;

    // Use this for initialization
    void Start()
    {
        NbreJoueur += 1;
        NumeroDuJoueur = NbreJoueur;
        if (NumeroDuJoueur == 1)
        {
            this.tag = "TagJoueurN1";
        }
        else if (NumeroDuJoueur == 2)
        {
            this.tag = "TagJoueurN2";
        }
        else if (NumeroDuJoueur == 3)
        {
            this.tag = "TagJoueurN3";
        }
        else if (NumeroDuJoueur == 4)
        {
            this.tag = "TagJoueurN4";
        }
        else if (NumeroDuJoueur == 5)
        {
            this.tag = "TagJoueurN5";
        }
        else if (NumeroDuJoueur == 6)
        {
            this.tag = "TagJoueurN6";
        }
        else if (NumeroDuJoueur == 7)
        {
            this.tag = "TagJoueurN7";
        }
        else if (NumeroDuJoueur == 8)
        {
            this.tag = "TagJoueurN8";
        }
        else if (NumeroDuJoueur == 9)
        {
            this.tag = "TagJoueurN9";
        }
        Deck = GameObject.FindGameObjectWithTag("TagDeckNetwork");
    }

    // Update is called once per frame
    void Update()
    {
        cos = 4f * Mathf.Cos(NumeroDuJoueur * 2f * Mathf.PI / NbreJoueur);//problème avec le client si mis dans le rpc
        sin = 2f * Mathf.Sin(NumeroDuJoueur * 2f * Mathf.PI / NbreJoueur);
        this.transform.position = new Vector3(cos, sin, 0);
        if (isServer)
        {
            RpcUpdate();
        }
    }

    [ClientRpc]
    private void RpcUpdate()
    {

        if (RecupCarte)//pour le placement après ajout d'une carte
        {
            if (true)
            {
                //Debug.Log("testTESTtest");
                Deck.GetComponent<Deck>().CmdPrendreCarte();
                cartes.Add(Deck.GetComponent<Deck>().TempCarte);
                TailleMainActuel++;// ça gène pas ici :)
                                   //Vector3 PositionCarte = new Vector3(this.transform.position.x + 1f * TailleMainActuel, this.transform.position.y, this.transform.position.z);
                for (int i = 0; i < TailleMainActuel; i++)//sert a mettre les cartes comme il faut
                {
                    Vector3 PositionCarte;
                    PositionCarte = new Vector3(this.transform.position.x + i - 0.5f * TailleMainActuel + 0.5f, this.transform.position.y - 1, this.transform.position.z);
                    cartes[i].transform.position = PositionCarte;//surrement plus simple qui existe mais je connais pas
                }
                cartes[TailleMainActuel - 1].GetComponent<Card>().Face.sprite = cartes[TailleMainActuel - 1].GetComponent<Card>().image;//afficher la face de la carte
                cartes[TailleMainActuel - 1].GetComponent<SpriteRenderer>().sortingLayerName = "Carte";
                cartes[TailleMainActuel - 1].GetComponent<Card>().EnMain = true;
                RecupCarte = false;
            }
        }
        SelectCarte();
    }

    private void OnMouseUpAsButton()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdTravel();
    }

    [Command]
    public void CmdTravel()//parce qu'il faut bien un nom sur cette méthode
    {
        if (TailleMainActuel < TailleMainMax)
        {
            RecupCarte = true;
            //Deck.GetComponent<Deck>().CmdPrendreCarte();
            //cartes.Add(Deck.GetComponent<Deck>().TempCarte);
            //TailleMainActuel++;// ça gène ici
        }
    }

    private void SelectCarte()//vas  de pair avec le if(enmain) du scriptcarte pour avoir juste une carte par main zoomé
    {
        for (int i = 0; i < TailleMainActuel; i++)
        {
            if (cartes[i].GetComponent<Card>().Select == true)
            {
                for (int j = 0; j < i; j++)//les deux for servent a remmetre tte les autres cartes a une taille normal
                {
                    cartes[j].GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
                }
                if (i != TailleMainActuel - 1)//éviter le oob
                {
                    for (int j = i + 1; j < TailleMainActuel; j++)
                    {
                        cartes[j].GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                cartes[i].GetComponent<Card>().Select = false;
            }
        }
    }
}
