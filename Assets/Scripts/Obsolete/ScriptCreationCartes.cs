
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCreationCartes : MonoBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    private GameObject PrefabCarte;
    private ScriptCarte PrefabScriptCarte;
    public int NbreCartes = 52; //nbres de cartes dans le deck
    public Sprite[] paquet;
    SpriteRenderer CarteVisible;
    private bool Init=true;//si c'est le premier click sur le deck
    private GameObject TempCarte;//pour replacer la carte en dessous du paquet
    private Sprite CarteAPrendre;//pour prendre la carte du dessus du paquet

    // Use this for initialization
    void Awake()
    {
        CarteVisible = GetComponent<SpriteRenderer>();
        //CarteVisible.sprite=
        //créateur sur deck de base WARNING
        PrefabCarte = GameObject.FindGameObjectWithTag("TagCarte");
        int nb = 0;
        string sgn = "N/A";
        for (int i = 1; i <= NbreCartes; i++)
        {
            if (Mathf.Round(i / 13) == 0)
            {
                nb = i;
                sgn = "Coeur";
            }
            else if (Mathf.Round(i / 13) == 1)
            {
                nb = i - 13;
                sgn = "Carreau";
            }
            else if (Mathf.Round(i / 13) == 2)
            {
                nb = i - 26;
                sgn = "Trèfle";
            }
            else if (Mathf.Round(i / 13) == 3)
            {
                nb = i - 39;
                sgn = "Pique";
            }
            else//en cas de bug
            {
                nb = 0;
                sgn = "N/A";
            }
            //cartes.Add(new ScriptCarte(nb, sgn, paquet[i - 1]));
            cartes.Add(Instantiate(PrefabCarte, this.transform.position, this.transform.rotation));
            cartes[i - 1].GetComponent<ScriptCarte>().signe = sgn;
            cartes[i - 1].GetComponent<ScriptCarte>().numero = nb;
            cartes[i - 1].GetComponent<ScriptCarte>().image = paquet[i - 1];
            cartes[i - 1].GetComponent<SpriteRenderer>().sortingLayerName="Default";
        }
        //test.sprite = cartes[12].image;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

void OnMouseUpAsButton()
    {
        if (Init == false)
        {//avec init on affiche juste la première carte
            TempCarte = cartes[NbreCartes - 1];
            for(int i = NbreCartes - 1; i > 0; i--)
            {
                cartes[i] = cartes[i - 1];
            }
            cartes[0] = TempCarte;
        }
        else
        {
            Init = false;
        }
        CarteVisible.sprite = cartes[NbreCartes-1].GetComponent<ScriptCarte>().image;//juste l'affichage
    }

public GameObject PrendreCarte()
    {
        TempCarte = cartes[NbreCartes - 1];
        cartes.RemoveAt(NbreCartes-1);
        NbreCartes -= 1;
        CarteVisible.sprite = cartes[NbreCartes - 1].GetComponent<ScriptCarte>().image;//reactu de l'affichage
        return TempCarte;
    }
}