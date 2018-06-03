using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptPlateauNetwork : NetworkBehaviour
{
    private GameObject PrefabDeck;
    private GameObject Deck;//le deck instancier
    private Transform PosDeck;//coord du deck
    public int NombreJoueur = 0;
    //public List<GameObject> ListeJoueur;
    //private Transform PosJoueur;

    // Use this for initialization
    void Awake()
    {
        //================instanciation deck
        PrefabDeck = GameObject.FindGameObjectWithTag("TagDeck");
        PosDeck = this.transform;
        Deck = Instantiate(PrefabDeck, new Vector3(0, 0, 0), PosDeck.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        string tag = "TagJoueurN";
        //GameObject joueur = GameObject.FindGameObjectWithTag("TagJoueurN1");
        //NombreJoueur = joueur.GetComponents<ScriptMainJoueurNetwork>().NbreJoueur;//mettre un if pour éviter les null
        NombreJoueur = 9;//la ligne du dessus marche pas jsp pq
        for (int i = 0; i < NombreJoueur; i++)
        {
            //if (ListeJoueur[i].GetComponent<ScriptMainJoueur>().RecupCarte == true)
            if (GameObject.FindGameObjectWithTag(string.Concat(tag, (1 + i).ToString())) == null)
            {
                return;
            }
            GameObject joueurI = GameObject.FindGameObjectWithTag(string.Concat(tag,(1+i).ToString()));
            if (joueurI.GetComponent<ScriptMainJoueurNetwork>().RecupCarte == true) 
            {
                joueurI.GetComponent<ScriptMainJoueurNetwork>().CarteARecup = Deck.GetComponent<ScriptDeck>().PrendreCarte();
                joueurI.GetComponent<ScriptMainJoueurNetwork>().RecupCarte = false;
            }
        }

    }

    private void OnMouseUpAsButton()
    {

    }
}
