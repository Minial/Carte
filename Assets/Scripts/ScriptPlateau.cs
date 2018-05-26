using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlateau : MonoBehaviour {
    private GameObject PrefabDeck;
    private GameObject Deck;//le deck instancier
    private Transform PosDeck;//coord du deck
    private GameObject PrefabJoueur;
    public int NombreJoueur = 1;
    public List<GameObject> ListeJoueur;
    private Transform PosJoueur;

	// Use this for initialization
	void Awake () {
        //================instanciation deck
        PrefabDeck = GameObject.FindGameObjectWithTag("TagDeck");
        PosDeck = this.transform;
        Deck = Instantiate(PrefabDeck, new Vector3(0, 0, 0), PosDeck.rotation);

        //=====================instanciation joueur
        ListeJoueur = new List<GameObject>();
        PrefabJoueur = GameObject.FindGameObjectWithTag("TagJoueur");
        PosJoueur=this.transform;
        //ListeJoueur.Add(Instantiate(PrefabJoueur, new Vector3(4, 0, 0), PosJoueur.rotation));
        //ListeJoueur.Add(Instantiate(PrefabJoueur, new Vector3(-4, 0, 0), PosJoueur.rotation));
        //ListeJoueur.Add(Instantiate(PrefabJoueur, new Vector3(0, 3, 0), PosJoueur.rotation));
        //ListeJoueur.Add(Instantiate(PrefabJoueur, new Vector3(0, -3, 0), PosJoueur.rotation));
        for (int i = 0; i < NombreJoueur; i++)
        {
            float Cos = 8f * Mathf.Cos(i*2f*Mathf.PI/NombreJoueur);
            float Sin = 4f * Mathf.Sin(i * 2f * Mathf.PI / NombreJoueur);
            ListeJoueur.Add(Instantiate(PrefabJoueur, new Vector3(Cos, Sin, 0), PosJoueur.rotation));
        }
    }
	
	// Update is called once per frame
	void Update () {
        for(int i=0;i<NombreJoueur;i++)
        {
            if (ListeJoueur[i].GetComponent<ScriptMainJoueur>().RecupCarte == true)
            {
                ListeJoueur[i].GetComponent<ScriptMainJoueur>().CarteARecup = Deck.GetComponent<ScriptDeck>().PrendreCarte();
                ListeJoueur[i].GetComponent<ScriptMainJoueur>().RecupCarte = false;
            }
        }
		
	}

    private void OnMouseUpAsButton()
    {
        
    }
}
