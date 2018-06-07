
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptDeckNetwork : NetworkBehaviour
{
    List<GameObject> cartes = new List<GameObject>();
    [SyncVar] private GameObject PrefabCarte;
    private ScriptCarte PrefabScriptCarte;
    [SyncVar] public int NbreCartes = 159; //nbres de cartes dans le deck
    //public Sprite[] paquet;
    public Sprite SpriteAttaque;//fait perdre 1PV 
    public Sprite SpriteAttaqueFurtive;//La cible ne peut utilsier ses pouvoirs de personnage
    public Sprite SpriteAttaqueSang;//récupère 1PV
    public Sprite SpriteEsquive;//esquive une attaque
    public Sprite SpriteParchemin;//echangeable contre n'importe quelle carte grise
    public Sprite SpriteSoin;//soigne 1 PV
    public Sprite SpritePoison; //Vos prochaines attaques feront +1 degats en échange d'1 PV jusqu'à la fin du tour
    public Sprite SpritePrendre;//Prends une carte
    public Sprite SpriteBrise;//Defausse une carte
    public Sprite SpriteMagasin;//Releve nbrJoueurs cartes du deck, chaque joueur en pioche une
    public Sprite SpritePioche;//pioche 2 cartes
    public Sprite SpriteDuel;//le joueur qui ne peut jouer de carte attaque perd 1PV
    public Sprite SpriteFlamme;//Zone de flamme
    public Sprite Sprite1000Fleche;//Zone de fleche
    public Sprite SpriteBombe;//Fait perdre 3PV a la cible si la détermination releve entre 2/Pique et 9/Pique
    public Sprite SpriteRepos;//Redonne 1PV a tous les joueurs
    public Sprite SpriteAntiMagie;//Annule l'effet d'une Magie
    public Sprite SpriteControle;//oblige un adversaire à attaquer quelqu'un sous la portée
    public Sprite SpritePrison;//Si la determination ne releve pas du coeur, le joueur pioche ses 2 cartes et passe son tour
    public Sprite SpriteBataille;//Bataille de signe, le joueur ayant le plus petit signe perds 1PV (si égale le lanceur perds le PV)
    public Sprite SpriteAlliee;//Lorsqu'un joueur allié perds/gagne des PV, tous les autres joueurs alli également
    public Sprite SpriteFamine;//Si la détermination ne releve pas du trefle, le joueur ne pioche pas ses deux cartes en début de tour
    public Sprite SpriteProvocation;//Si la détermination ne releve pas du Carreau, les attaques du joueur "Provoqué" sont dérivées au poseur de la provoc
    public Sprite SpriteEchange;//echange entre 2 joueur 1 contre 2 cartes
    public Sprite SpriteBouclierEsquive;//si adversaire vous attaque, faites détermination, si c'est rouge, vous esquivez l'attaque
    public Sprite SpriteChevalOffenssif;//Cheval -1
    public Sprite SpriteChevalDeffenssif;//Cheval +1
    public Sprite SpriteArmeInfinie;//Vous pouvez jouez plus d'une attaque par tour
    public Sprite SpriteArmeBriseCarte;//Si vous touchez, vous pouvez defaussez 2 cartes de la cible au lieu d'un degat
    public Sprite SpriteArmeContreBouclier;//Vous ignorez le bouclier de la cible
    public Sprite SpriteArmeContreEsquive;//si la cible esquive l'attaque, vous pouvez jouez une autre carte attaque
    public Sprite SpriteArmePrendreCarte;//Vous pouvez utilisez 2 cartes en main au lieu d'une attaque
    public Sprite SpriteArmeDegatSiEsquive;//Si la cible Esquive, vous pouvez utiliser 2 cartes (main ou equipement pour qu'elle subisse le degat)
    public Sprite SpriteArmeAttaqueZone;//si votre derniere carte en main est une attaque et que vous l'utilisez, elle peut toucher jusqu'à 3 joueurs
    public Sprite SpriteArmeBriseCheval;//Si vous touchez, vous pouvez defaussez 1 cheval de la cible au lieu d'un degat
    public Sprite SpriteBouclier2Degats;//Peut encaisser 2 degats
    public Sprite SpriteBouclierZone;//ignore les magie de zones
    public Sprite SpriteArmePoison;//Vos prochaines attaques feront +1 degats en échange d'1 PV jusqu'à la fin du tour
    public Sprite SpriteBouclierSac;//Vous pouvez gardez une carte suplémentaire à la fin du tour
    public Sprite SpriteArmeAttaqueSang;//Si la cible n'esquive pas, récupère 1PV
    SpriteRenderer CarteVisible;
    [SyncVar] private bool Init = true;//si c'est le premier click sur le deck
    [SyncVar] private GameObject TempCarte;//pour replacer la carte en dessous du paquet
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
            if (i <= 39)
            {
                if (i <= 13)
                {
                    nb = i;
                    sgn = "Coeur";
                }
                if (13 < i && i <= 26)
                {
                    nb = i - 13;
                    sgn = "Coeur";
                }
                if (26 < i && i <= 39)
                {
                    nb = i - 26;
                    sgn = "Coeur";
                }

            }
            else if (39 < i && i <= 78)
            {
                if (i <= 52)
                {
                    nb = i - 39;
                    sgn = "Carreau";
                }
                if (52 < i && i <= 65)
                {
                    nb = i - 52;
                    sgn = "Carreau";
                }
                if (65 < i && i <= 78)
                {
                    nb = i - 65;
                    sgn = "Carreau";
                }
            }
            else if (78 < i && i <= 117)
            {
                if (i <= 91)
                {
                    nb = i - 78;
                    sgn = "Trèfle";
                }
                if (91 < i && i <= 104)
                {
                    nb = i - 91;
                    sgn = "Trèfle";
                }
                if (104 < i && i <= 117)
                {
                    nb = i - 104;
                    sgn = "Trèfle";
                }

            }
            else
            {
                if (i <= 130)
                {
                    nb = i - 117;
                    sgn = "Pique";
                }
                if (130 < i && i <= 143)
                {
                    nb = i - 130;
                    sgn = "Pique";
                }
                if (143 < i && i <= 156)
                {
                    nb = i - 143;
                    sgn = "Pique";
                }
                if (156 < i && i <= 159)
                {
                    nb = i - 156;
                    sgn = "Pique";
                }

            }
            cartes.Add(Instantiate(PrefabCarte, this.transform.position, this.transform.rotation));
            NetworkServer.Spawn(cartes[i-1]);
            cartes[i - 1].GetComponent<ScriptCarte>().signe = sgn;
            cartes[i - 1].GetComponent<ScriptCarte>().numero = nb;
        }
        Randomise();

        for (int i = 1; i <= NbreCartes; i++)
        {
            //30 Cartes
            if (0 < i && i <= 30)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAttaque;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Attaque";
            }
            //22 Cartes
            if (30 < i && i <= 52)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteEsquive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Esquive";
            }
            //12 Cartes
            if (52 < i && i <= 64)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteSoin;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Soin";
            }
            //7 Cartes
            if (64 < i && i <= 71)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAttaqueFurtive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "AttaqueFurtive";
            }
            //2 Cartes
            if (71 < i && i <= 73)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteParchemin;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Parchemin";
            }
            //7 cartes
            if (73 < i && i <= 80)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAttaqueSang;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "AttaqueSang";
            }
            //4 Cartes
            if (80 < i && i <= 84)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpritePoison;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Poison";
            }
            //5 Cartes
            if (84 < i && i <= 89)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpritePrendre;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Prendre";
            }
            //6 Cartes
            if (89 < i && i <= 95)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBrise;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Brise";
            }
            //2 Cartes
            if (95 < i && i <= 97)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteMagasin;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Magasin";
            }
            //4 Cartes
            if (97 < i && i <= 101)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpritePioche;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Pioche";
            }
            //3 Cartes
            if (101 < i && i <= 104)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteDuel;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Duel";
            }
            //3 Cartes
            if (104 < i && i <= 107)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteFlamme;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Flamme";
            }
            //1 Carte
            if (107 < i && i <= 108)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = Sprite1000Fleche;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "1000Fleche";
            }
            //2 Cartes
            if (108 < i && i <= 110)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBombe;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Bombe";
            }
            //1 Carte
            if (110 < i && i <= 111)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteRepos;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Repos";
            }
            //6 Cartes
            if (111 < i && i <= 117)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAntiMagie;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "AntiMagie";
            }
            //2 Cartes
            if (117 < i && i <= 119)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteControle;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Controle";
            }
            //3 Cartes
            if (119 < i && i <= 122)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpritePrison;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Prison";
            }
            //2 Cartes
            if (122 < i && i <= 124)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBataille;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Bataille";
            }
            //4 Cartes
            if (124 < i && i <= 128)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteAlliee;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Alliee";
            }
            //2 Cartes
            if (128 < i && i <= 130)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteFamine;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Famine";
            }
            //3 Cartes
            if (130 < i && i <= 133)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteProvocation;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Provocation";
            }
            //3 Cartes
            if (133 < i && i <= 136)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteEchange;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Echange";
            }
            //2 Cartes
            if (136 < i && i <= 138)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBouclierEsquive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "BouclierEsquive";
            }
            //3 Cartes
            if (138 < i && i <= 141)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteChevalOffenssif;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ChevalOffenssif";
            }
            //3 Cartes
            if (141 < i && i <= 144)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteChevalDeffenssif;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ChevalDeffenssif";
            }
            //2 Cartes
            if (144 < i && i <= 146)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeInfinie;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeInfinie";
            }
            //1 Carte
            if (146 < i && i <= 147)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeBriseCarte;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeBriseCarte";
            }
            //1 Carte
            if (147 < i && i <= 148)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeContreBouclier;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeContreBouclier";
            }
            //1 Carte
            if (148 < i && i <= 149)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeContreEsquive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeContreEsquive";
            }
            //1 Carte
            if (149 < i && i <= 150)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmePrendreCarte;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmePrendreCarte";
            }
            //1 Carte
            if (150 < i && i <= 151)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeDegatSiEsquive;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeDegatSiEsquive";
            }
            //1 Carte
            if (151 < i && i <= 152)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeAttaqueZone;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "AttaqueZone";
            }
            //1 Carte
            if (152 < i && i <= 153)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeBriseCheval;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeBriseCheval";
            }
            //1 Carte
            if (153 < i && i <= 154)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBouclier2Degats;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "Bouclier2Degats";
            }
            //1 Carte
            if (154 < i && i <= 155)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBouclierZone;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "BouclierZone";
            }
            //1 Carte
            if (155 < i && i <= 156)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmePoison;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmePoison";
            }
            //2 Cartes
            if (156 < i && i <= 158)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteBouclierSac;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "BouclierSac";
            }
            //1 Carte
            if (158 < i && i <= 159)
            {
                cartes[i - 1].GetComponent<ScriptCarte>().image = SpriteArmeAttaqueSang;
                cartes[i - 1].GetComponent<ScriptCarte>().TypeCarte = "ArmeAttaqueSang";
            }
            cartes[i - 1].GetComponent<SpriteRenderer>().sortingLayerName = "Default";//pour éviter que la carte s'affiche devant le reste
            //cartes[i - 1].transform.localScale=new Vector3(0.5f,0.5f,1)//pour afficher a la bonne taille
        }
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
    /*
    [Command]
    public GameObject CmdPrendreCarte()
    {
        TempCarte = cartes[NbreCartes - 1];
        cartes.RemoveAt(NbreCartes - 1);
        NbreCartes -= 1;
        //this.transform.localScale = new Vector3(1, 1, 1);//pour afficher a la bonne taille
        //CarteVisible.sprite = cartes[NbreCartes - 1].GetComponent<ScriptCarte>().image;//reactu de l'affichage
        return TempCarte;
    }
    */
    public void Randomise()
    {
        for (int i = 0; i < NbreCartes; i++)
        {
            int R = Random.Range(0, i + 1);
            GameObject Temp = cartes[R];
            cartes[R] = cartes[i];
            cartes[i] = Temp;
        }
    }
}