using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    #region Variables
    [SerializeField]
    public List<CardInfo> Deck = null;
    [SerializeField]
    public List<CardInfo> OriginDeck = null;
    [SerializeField]
    public List<CardInfo> Hand = null;
    [SerializeField]
    public List<CardInfo> tmpDeck = null;
    public GameObject hand = null;
    public bool playerId = true;
    public GameObject plyPnl = null;
    public int ManaPool = 0;
    public int _Mana = 0;
    public int _Health = 30;
    public Text hlth = null;
    public Text manaTxt = null;
    public GameObject slctPnl = null;
    public PlayerInfo othrPlyr = null;
    public GameObject b = null;
    private GameObject c = null;
    public Text PlyrName = null;
    public Text turnTxt = null;
    public string _turnString=null;
    public string _plyrName = null;
    #region weapon
    public int _wpnDmg = 0;
    public int _wpnChrg = 0;
    public Text wpnDmgTxt = null;
    public Text wpnChrgTxt = null;
    public Button wpnBtn = null;
    public CardInfo wpnCrd = null;
    public bool wpnPlyd = true;
    public bool hasWpn = false;
    public bool hasGuard = false;
    //public int guardNom = 0;
    #endregion
    #endregion
    #region Properties
    public int Health
    {
        get
        {
            return _Health;
        }
        set
        {
            _Health = value;
            if (_Health <= 0)
            {
                GameLogic.Instanse.GameEnd();
            }
            hlth.text = _Health.ToString();
        }
    }
    public int Mana 
    {
        get
        {
            return _Mana;
        }
        set
        {
            _Mana = value;
            manaTxt.text = _Mana.ToString();
        }
    }
    public string plyrName
    {
        get
        {
            return _plyrName;
        }
        set
        {
            _plyrName = value;
            PlyrName.text = _plyrName;
        
        }
    }
    public string turnString
    {
        get
        {
            return _turnString;
        }
        set
        {
            _turnString = "Turn: " + value;
            turnTxt.text = _turnString;
        }
    }
    public int wpnDmg
    {
        get 
        {
            return _wpnDmg;
        }
        set 
        {
            if (value < 0) return;
            _wpnDmg = value;
            wpnDmgTxt.text = _wpnDmg.ToString();
        
        }
    
    }
    public int wpnChrg
    {
        get 
        {
            return _wpnChrg;
        }

        set 
        {
            
            _wpnChrg = value;
            if(wpnCrd)
            wpnCrd.health = _wpnChrg;
            if (value <= 0)
            {
                _wpnChrg = 0;
                wpnDmg = 0;
                wpnBtn.interactable = false;
            }
           
            wpnChrgTxt.text = _wpnChrg.ToString();
            
        }
    }
    public bool WpnPlyd
    {
        get
        {
            return wpnPlyd;
        }
        set 
        {
            wpnPlyd = value;
            wpnBtn.interactable = !value;
        }
    }



    #endregion
    #region Functions
    // Use this for initialization
	void Start () {
        EventManeger.Instance.AddListener(EVENT_TYPE.GAME_START,initialize);
        EventManeger.Instance.AddListener(EVENT_TYPE.TURN_END, trnChngd);
        EventManeger.Instance.AddListener(EVENT_TYPE.CARD_PLAY,crdPly);
        EventManeger.Instance.AddListener(EVENT_TYPE.CARD_SLCT,crdSlct);
        EventManeger.Instance.AddListener(EVENT_TYPE.CARD_ATTACKED,crdAttkcd);
        EventManeger.Instance.AddListener(EVENT_TYPE.Weapon_Equiped, wpnEqpd);
        //EventManeger.Instance.AddListener(EVENT_TYPE.ITEM_SLCT,itmSlct);
        EventManeger.Instance.AddListener(EVENT_TYPE.ITEM_UsdOnThis,ItmUsd);
        for (int i = 0; i < Deck.Count; i++)
        {
            OriginDeck.Add(Deck[i]);
        }
            tmpDeck = Deck;
        Mana = _Mana;
        Health = _Health;
        plyrName = _plyrName;
        wpnChrg = 0;
	}

    private void wpnEqpd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        /*if (playerId != GameLogic.turn)
            return;
        CardInfo a = Sender.gameObject.GetComponent<CardInfo>();

        if (a._manaCost <= Mana)
        {
            Mana -= a._manaCost;
            a.ownr = GameLogic.turn;
            wpnCrd = ( Instantiate(a.prefab)).GetComponent<CardInfo>();
            wpnChrg = wpnCrd.health;
            wpnDmg = wpnCrd.damage;
            hasWpn = true;
            removeCard(a);
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }*/
        if (playerId != GameLogic.turn)
            return;
        CardInfo a = Sender.gameObject.GetComponent<CardInfo>();

        if (a._manaCost <= Mana)
        {
            Mana -= a._manaCost;
            a.ownr = GameLogic.turn;
            //Instantiate(a.prefab, plyPnl.transform, false);
            wpnCrd =(CardInfo) Instantiate (a.prefab.GetComponent<CardInfo>());
            wpnChrg = wpnCrd.health;
            wpnDmg = wpnCrd.damage;
            hasWpn = true;
            removeCard(a);
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }
       
       
       


    }

    private void ItmUsd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        if (GameLogic.turn != playerId)
            return;
        if(slctPnl.transform.childCount==0)
            return;
        if (Mana < b.GetComponent<CardInfo>().manaCost)
            return;
        Mana -= b.GetComponent<CardInfo>().manaCost;
        CardInfo tmpCrdinfo = Sender.GetComponent<CardInfo>();
        tmpCrdinfo.health += b.GetComponent<CardInfo>().health;
        tmpCrdinfo.damage += b.GetComponent<CardInfo>().damage;
        DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        b.GetComponent<CardInfo>().health = -1;
    }

   /* private void itmSlct(EVENT_TYPE Event_Type, Component Sender, object param = null)
    { 
        if (playerId != GameLogic.turn)
            return;
    }*/

    private void crdAttkcd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        if (playerId != GameLogic.turn)
           hasGuard= isPlyrHasGuard();
        //EventManeger.Instance.PostNotification(EVENT_TYPE.CheckFor_Guards, this);


        #region frineldy Spell
        if (GameLogic.turn == playerId)
        {
            if (!b)
                return;
            if (b.GetComponent<CardInfo>().ID==CardId.Spell)
            {
                if (b.GetComponent<CardInfo>().manaCost < Mana)
                {

                    if (b.GetComponent<Spell>().doSpecial(b.GetComponent<Spell>().splTyp, b.GetComponent<CardInfo>(), Sender.gameObject.GetComponent<CardInfo>()))
                    {
                        Mana -= b.GetComponent<CardInfo>().manaCost;
                        b.GetComponent<CardInfo>().health = -1;
                        slctPnl.transform.GetChild(0).gameObject.GetComponent<CardInfo>().health = -1;
                    }
                }
                
            }
            return;
        }
        #endregion
        if (othrPlyr.slctPnl.transform.childCount < 1)
            return;
        #region enemy spell
        if (othrPlyr.b.GetComponent<CardInfo>().ID==CardId.Spell)
        {
            if (othrPlyr.b.GetComponent<CardInfo>().manaCost < Mana)
            {
                if (othrPlyr.b.GetComponent<Spell>().doSpecial(othrPlyr.b.GetComponent<Spell>().splTyp, othrPlyr.b.GetComponent<CardInfo>(), Sender.gameObject.GetComponent<CardInfo>()))
                {
                    Mana -= othrPlyr.b.GetComponent<CardInfo>().manaCost; 
                    othrPlyr.b.GetComponent<CardInfo>().health = -1;
                    othrPlyr.slctPnl.transform.GetChild(0).gameObject.GetComponent<CardInfo>().health = -1;
                }
            }
            return;
        }
        #endregion
        #region check for guards
        
        if (hasGuard)
        {
            if (Sender.gameObject.GetComponent<Guard>() == null)
                return;
        }
        #endregion
        if (othrPlyr.b.GetComponent<CardInfo>().ID != CardId.Weapon)
        {
        if (othrPlyr.b.GetComponent<CardInfo>().plyd)
            return;
        othrPlyr.b.GetComponent<CardInfo>().plyd = true;
        }
        int dmgeSndr = Sender.gameObject.GetComponent<CardInfo>().damage;
        int attckrDmg = othrPlyr.b.GetComponent<CardInfo>().damage;
       
        if (othrPlyr.slctPnl.transform.GetChild(0).gameObject.GetComponent<CardInfo>().ID == CardId.Weapon)
        {
            
            Sender.gameObject.GetComponent<CardInfo>().health -= attckrDmg;
            if (othrPlyr.wpnChrg == 1)
                DestroyImmediate(othrPlyr.slctPnl.transform.GetChild(0).gameObject);
            othrPlyr.wpnChrg--;
            wpnPlyd = true;
        }
        else
        {

            othrPlyr.b.GetComponent<CardInfo>().health -= dmgeSndr;
            othrPlyr.slctPnl.transform.GetChild(0).gameObject.GetComponent<CardInfo>().health -= dmgeSndr;
            Sender.gameObject.GetComponent<CardInfo>().health -= attckrDmg;
        }
        //Sender.gameObject.GetComponent<CardInfo>().health -= attckrDmg;
        
        
    }

    private void crdSlct(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        if (playerId != GameLogic.turn)
            return;
        if(Sender.GetComponent<CardInfo>().ownr==playerId)
        {
        b = Sender.gameObject;
        if (slctPnl.transform.childCount > 0)
        {
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }
        c= (GameObject)(Instantiate(b.GetComponent<CardInfo>().prefab, slctPnl.transform, false));
        c.GetComponent<Button>().interactable = false;
        }
    }

    private void crdPly(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        if (playerId != GameLogic.turn)
            return;
        if (slctPnl.transform.GetChild(0).gameObject.GetComponent<Spell>())
            return;
        CardInfo a = Sender.gameObject.GetComponent<CardInfo>();
       
        if (a._manaCost <= Mana)
        {
            Mana -= a._manaCost;
            a.ownr = GameLogic.turn;
            Instantiate(a.prefab, plyPnl.transform, false);
            removeCard(a);
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }
    }

    private void trnChngd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {

        pnlActve(GameLogic.turn==playerId);
        if (GameLogic.turn)
             ManaPool++;
        Mana = ManaPool;
      // hndActve(GameLogic.turn == playerNom);
        //pnlActve(GameLogic.turn == playerNom);
        if (GameLogic.turn == playerId)
        {
            turnString = plyrName;
            DrawCard();
        }
        
        if(slctPnl.transform.childCount>0)
        {
            b = null;
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }
    }


    public void hndActve(bool a)
    {
        if (hand.transform.childCount < 1)
            return;
        for (int i = 0; i < hand.transform.childCount;i++ )
        {
            hand.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = a;
        }
    }


    public void pnlActve(bool a)
    {
        if (hasWpn)
        {
            WpnPlyd = !a;
        }
        else
        {
            WpnPlyd = true;
        }
        if (plyPnl.transform.childCount < 1)
            return;
        for (int i = 0; i < plyPnl.transform.childCount; i++)
        {
            plyPnl.transform.GetChild(i).gameObject.GetComponent<CardInfo>().plyd = !a;
        }
    }

    public void initialize(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        
        cleanObj(hand);
        Hand.Clear();
        cleanObj(slctPnl);
        cleanObj(plyPnl);
        b = null;
        Health = 30;
        Mana = 0;
        ManaPool = 0;
        
        plyrName = _plyrName;
        Deck.Clear();
        tmpDeck.Clear();
        for (int j = 0; j < OriginDeck.Count; j++)
        {
            Deck.Add(OriginDeck[j]);
        }
        tmpDeck = Deck;
        for (int i = 0; i < 3; i++)
        {
           DrawCard();
        }
           
    }
    
    public void DrawCard()
    {
        if (tmpDeck.Count < 1)
            return;
        List<CardInfo> tmpDeck1 = tmpDeck;
        int c = Random.Range(0,tmpDeck.Count);
        if( hand.transform.childCount < 8)
        {
         Hand.Add(tmpDeck[c]);
         Instantiate(tmpDeck[c].GetComponent<CardInfo>().prefab, hand.transform, false);
        }
        tmpDeck1.Remove(tmpDeck[c]);
        tmpDeck = tmpDeck1;
    }

   public void removeCard(CardInfo card)
    {
        Hand.Remove(card);
     for(int i=0;i<hand.transform.childCount;i++)
      {
         GameObject a=hand.transform.GetChild(i).gameObject;
          if (a.GetComponent<CardInfo>().ID == card.ID)
          {
              DestroyImmediate(a);
              return;
          }
      }
    }

    public void OnPnlClck()
    {
        
       /* if (slctPnl.transform.childCount < 1 && othrPlyr.slctPnl.transform.childCount<1)
            return;*/
        #region frineldy panel clicked
        if (slctPnl.transform.childCount==1)
        {
            if(b.transform.parent.gameObject.tag=="Hand")
            {
                if (b.GetComponent<CardInfo>().ID == CardId.Item)
                    return;
                if (b.GetComponent<CardInfo>().ID == CardId.Weapon)
                {
                    EventManeger.Instance.PostNotification(EVENT_TYPE.Weapon_Equiped, b.GetComponent<CardInfo>());
                }
                else
                {
                    EventManeger.Instance.PostNotification(EVENT_TYPE.CARD_PLAY, b.GetComponent<CardInfo>());
                }
                    //DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
            }
        }
        #endregion


    }

    public void OnPLyrClck()
    {
        hasGuard = isPlyrHasGuard();
        if (GameLogic.turn == playerId)
            return;
        if (othrPlyr.slctPnl.transform.childCount < 1)
            return;
        if (othrPlyr.b.GetComponent<CardInfo>().ID == CardId.Weapon)
        {
            if (othrPlyr.wpnPlyd)
                return;
            
            if (hasGuard)
                return;
            Health -= othrPlyr.b.GetComponent<CardInfo>().damage;
            if (othrPlyr.wpnChrg == 1)
                DestroyImmediate(othrPlyr.slctPnl.transform.GetChild(0).gameObject);
            othrPlyr.wpnChrg--;
            
            wpnPlyd = true;

        }
        else
        {
            if (othrPlyr.b.GetComponent<CardInfo>().plyd )
                return;
            if (hasGuard)
                return;
            othrPlyr.b.GetComponent<CardInfo>().plyd = true;
            Health -= othrPlyr.b.GetComponent<CardInfo>().damage;
        }
    }

    public void cleanObj(GameObject a)
    {
        if (a.transform.childCount == 0)
            return;
        for (int i = a.transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(a.transform.GetChild(i).gameObject);
        }
    }

    public void WpnSlct()
    {
        if (GameLogic.turn != playerId)
            return;
        if (!hasWpn)
            return;
        if (slctPnl.transform.childCount > 0)
        {
            DestroyImmediate(slctPnl.transform.GetChild(0).gameObject);
        }
        b = wpnCrd.prefab;
        b.GetComponent<CardInfo>().health = wpnChrg;
        Instantiate(b,slctPnl.transform,false);
    
    }

    public bool isFriendly(CardInfo crd)
    { 
    if(crd.crdOwnr.playerId==playerId)
    {
        return true;
    }
    else
    {
     return false;    
    }
      
    }
    public bool isPlyrHasGuard()
    { 
    if(plyPnl.transform.childCount>0)
    {
        
        for (int i = 0; i < plyPnl.transform.childCount; i++)
        {
            if (plyPnl.transform.GetChild(i).gameObject.GetComponent<Guard>())
                return true;
        }
        
    }
    return false;
    }
    #endregion
}
