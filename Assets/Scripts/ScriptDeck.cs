
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDeck : MonoBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    private GameObject PrefabCarte;
    private ScriptCarte PrefabScriptCarte;
    public int NbreCartes = 52; //nbres de cartes dans le deck
    //public Sprite[] paquet;
    public Sprite SpriteAttaque;
    public Sprite SpriteEsquive;
    public Sprite SpriteSoin;
    SpriteRenderer CarteVisible;
    private bool Init = true;//si c'est le premier click sur le deck
    private GameObject TempCarte;//pour replacer la carte en dessous du paquet
    private Sprite CarteAPrendre;//pour prendre la carte du dessus du paquet

    // Use this for initialization
    void Awake()
    {
        CarteVisible = GetComponent<SpriteRenderer>();
        PrefabScriptCarte = GetComponent<ScriptCarte>();
        PrefabCarte = GameObject.FindGameObjectWithTag("TagCarte");
        int nb = 0;
        string sgn = "N/A";
        for (int i = 1; i <= NbreCartes; i++)
        {
            if (i<=NbreCartes*0.25)
            {
                nb = i;
                sgn = "Coeur";
            }
            else if (i <= NbreCartes * 0.5)
            {
                nb = i - 13;
                sgn = "Carreau";
            }
            else if (i <= NbreCartes * 0.75)
            {
                nb = i - 26;
                sgn = "Trèfle";
            }
            else
            {
                nb = i - 39;
                sgn = "Pique";
            }
            cartes.Add(Instantiate(PrefabCarte, this.transform.position, this.transform.rotation));
            cartes[i - 1].GetComponent<ScriptCarte>().signe = sgn;
            cartes[i - 1].GetComponent<ScriptCarte>().numero = nb;
        }
        Randomise();
        for (int i = 1; i <= NbreCartes; i++)
        {
            if (i <= NbreCartes * 0.4)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAttaque;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Attaque";
            }
            else if (i <= NbreCartes * 0.7)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteEsquive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Esquive";
            }
            else
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteSoin;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Soin";
            }
            cartes[i - 1].GetComponent<SpriteRenderer>().sortingLayerName = "Default";//pour éviter que la carte s'affiche devant le reste
            //cartes[i - 1].transform.localScale=new Vector3(0.5f,0.5f,1)//pour afficher a la bonne taille
;        }
        Randomise();//on le met en deux fois pour que les signes ne corresponde pas aux cartes.
        this.transform.localScale = new Vector3(2.02222222f, 1.9841269f, 1);//pour afficher a la bonne taille au départ
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void OnMouseUpAsButton()
    {
        if (Init == false)
        {//avec init on affiche juste la première carte
            TempCarte = cartes[NbreCartes - 1];
            for (int i = NbreCartes - 1; i > 0; i--)
            {
                cartes[i] = cartes[i - 1];
            }
            cartes[0] = TempCarte;
        }
        else
        {
            Init = false;
            this.transform.localScale = new Vector3(1,1,1);//pour afficher a la bonne taille
        }
        CarteVisible.sprite = cartes[NbreCartes - 1].GetComponent<ScriptCarte>().image;//juste l'affichage
    }*/

    public GameObject PrendreCarte()
    {
        TempCarte = cartes[NbreCartes - 1];
        cartes.RemoveAt(NbreCartes - 1);
        NbreCartes -= 1;
        //this.transform.localScale = new Vector3(1, 1, 1);//pour afficher a la bonne taille
        //CarteVisible.sprite = cartes[NbreCartes - 1].GetComponent<ScriptCarte>().image;//reactu de l'affichage
        return TempCarte;
    }

    public void Randomise()
    {
        for(int i = 0; i < NbreCartes; i++)
        {
            int R = Random.Range(0, i + 1);
            GameObject Temp = cartes[R];
            cartes[R] = cartes[i];
            cartes[i] = Temp;
        }
    }
}