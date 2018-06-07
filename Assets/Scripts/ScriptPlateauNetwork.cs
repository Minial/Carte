using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptPlateauNetwork : NetworkBehaviour
{
    private GameObject PrefabDeck;
    private GameObject Deck;//le deck instancier
    private Transform PosDeck;//coord du deck
    [SyncVar] public int NombreJoueur = 0;
    //public List<GameObject> ListeJoueur;
    //private Transform PosJoueur;

    public override void OnStartClient()//sert a la création du deck sur le serveur
    {
        ClientScene.RegisterPrefab(PrefabDeck);
    }

    [Server]
    public void ServerSpawnDeck(Vector3 pos, Quaternion rot)//sert a la création du deck sur le serveur
    {
        Deck = (GameObject)Instantiate(PrefabDeck, pos, rot);
        NetworkServer.Spawn(Deck);
    }
    // Use this for initialization
    void Awake()
    {
        //================instanciation deck
        PrefabDeck = GameObject.FindGameObjectWithTag("TagDeckNetwork");
        PosDeck = this.transform;
        ServerSpawnDeck(new Vector3(0, 0, 0), PosDeck.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        CmdUpdate();
    }

    [Command]
    void CmdUpdate()
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
            GameObject joueurI = GameObject.FindGameObjectWithTag(string.Concat(tag, (1 + i).ToString()));
            if (joueurI.GetComponent<ScriptMainJoueurNetwork>().RecupCarte == true)
            {
                //joueurI.GetComponent<ScriptMainJoueurNetwork>().CarteARecup = Deck.GetComponent<ScriptDeckNetwork>().CmdPrendreCarte();
                Debug.Log("testTESTtest");
                joueurI.GetComponent<ScriptMainJoueurNetwork>().RecupCarte = false;
            }
        }
    }

    private void OnMouseUpAsButton()
    {

    }
}
