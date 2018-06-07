using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptMainJoueurNetwork : NetworkBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    public static int NbreJoueur=0;
    [SyncVar] public int TailleMainMax = 5;
    [SyncVar] public int TailleMainActuel = 0;
    [SyncVar] public bool RecupCarte = false;// pou
    [SyncVar] public GameObject CarteARecup;
    [SyncVar] public int NumeroDuJoueur = 0;

    // Use this for initialization
    void Awake()
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
    }

    // Update is called once per frame
    void Update()
    {
        CmdUpdate();
    }

    [Command]//donné vont vers server
    void CmdUpdate()
    {
        float Cos = 8f * Mathf.Cos(NumeroDuJoueur * 2f * Mathf.PI / NbreJoueur);
        float Sin = 4f * Mathf.Sin(NumeroDuJoueur * 2f * Mathf.PI / NbreJoueur);
        this.transform.position = new Vector3(Cos, Sin, 0);
        if (!isLocalPlayer)
        {
            return;
        }
        if (CarteARecup != null)//pour récup les cartes
        {
            cartes.Add(CarteARecup);
            CarteARecup = null;
            //Vector3 PositionCarte = new Vector3(this.transform.position.x + 1f * TailleMainActuel, this.transform.position.y, this.transform.position.z);
            for (int i = 0; i < TailleMainActuel; i++)//sert a mettre les cartes comme il faut
            {
                Vector3 PositionCarte;
                PositionCarte = new Vector3(this.transform.position.x + i - 0.5f * TailleMainActuel + 0.5f, this.transform.position.y - 1, this.transform.position.z);
                cartes[i].transform.position = PositionCarte;//surrement plus simple qui existe mais je connais pas
            }
            cartes[TailleMainActuel - 1].GetComponent<ScriptCarte>().Face.sprite = cartes[TailleMainActuel - 1].GetComponent<ScriptCarte>().image;//afficher la face de la carte
            cartes[TailleMainActuel - 1].GetComponent<SpriteRenderer>().sortingLayerName = "Carte";
            cartes[TailleMainActuel - 1].GetComponent<ScriptCarte>().EnMain = true;
        }
        SelectCarte();
    }

    private void OnMouseUpAsButton()
    {
        CmdOnMouseUpAsButton();
    }

    [Command]
    private void CmdOnMouseUpAsButton()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (TailleMainActuel < TailleMainMax)
        {
            RecupCarte = true;
            TailleMainActuel++;// peut se placer aussi dans lupdate, mais ça ne gène pas pour le moment
        }
    }

    private void SelectCarte()
    {
        CmdSelectCarte();
    }

    [Command]
    private void CmdSelectCarte()//vas  de pair avec le if(enmain) du scriptcarte pour avoir juste une carte par main zoomé
    {
        if (!isLocalPlayer)
        {
            return;
        }
        for (int i = 0; i < TailleMainActuel; i++)
        {
            if (cartes[i].GetComponent<ScriptCarte>().Select == true)
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
                cartes[i].GetComponent<ScriptCarte>().Select = false;
            }
        }
    }
}
