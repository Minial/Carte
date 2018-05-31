using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptMainJoueurNetwork : NetworkBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    public int TailleMainMax = 5;
    public int TailleMainActuel = 0;
    public bool RecupCarte = false;// pou
    public GameObject CarteARecup;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    private void SelectCarte()//vas  de pair avec le if(enmain) du scriptcarte pour avoir juste une carte par main zoomé
    {
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
