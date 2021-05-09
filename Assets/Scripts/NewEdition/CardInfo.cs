using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#region Enums
public enum CardId { 
Sample,Type1,Type2,Type3,Type4,Item,Weapon,Spell
}

public enum KeyWords
{ 
gaurd=1,charge=2
}
#endregion
public class CardInfo : MonoBehaviour
{
    #region Variables
    public GameObject prefab;
    public int _manaCost = 1;
    public int _damage = 1;
    public int _health = 1;
    public string _cardName = "Sample";
    public CardId ID = CardId.Sample;  
    public Text manaTxt = null;
    public Text hlthTxt = null;
    public Text dmgTxt = null;
    public Text crdNme = null;
    public Text keyWordsTxt = null;
    public string _keyWords = null;
    public bool _plyd = true;
    public GameObject parent = null;
    public string ownrTg = null;
    public bool ownr = false;
    public Shadow crdShdw = null;
    public Quaternion qtrn = Quaternion.Euler(new Vector3(5,0,0));
    public PlayerInfo crdOwnr = null;
    
    #endregion
    //-------------------------------------------------------------------------------------
    #region Properties
    public int manaCost
    {
        get
        {
            return _manaCost;
        }
        set
        {
            _manaCost = value;
            manaTxt.text = _manaCost.ToString();
        }
    }
    
    public int health
    {
        get 
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0 )
            {
                
               
                
                DestroyImmediate(gameObject);
                
                return;
                //EventManeger.Instance.PostNotification(EVENT_TYPE.CheckFor_Guards, this);
            }
            hlthTxt.text = _health.ToString();
        }
    }
    
    public int damage
    {
        get 
        {
            return _damage;
        }
        set 
        {
            _damage = value;
            dmgTxt.text = _damage.ToString();
        }
    }
    public bool plyd
    { 
   
        get
        {
            return _plyd;
        }

        set 
        {
            _plyd = value;
            if (!parent)
                return;
            if (parent.gameObject.tag == "Grnd")
            {
                if (!value)
                {
                    crdShdw.effectDistance = new Vector2(1, -7);

                    transform.rotation = qtrn;
                   
                }
                else
                {
                    
                    crdShdw.effectDistance = new Vector2(1, -1);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }

    public string cardName
    {
        get 
        {
            return _cardName;
        }
        set 
        {
            _cardName = value;
            crdNme.text = _cardName;
        }
    }

    public string keyWords 
    {
        get
        {
            return _keyWords;
        }
        set
        {
            string a = keyWordsTxt.text;
            if (a == "None")
            {
                keyWordsTxt.text = value;
            }
            else
            {
               // keyWordsTxt.text += "," + value;
                keyWordsTxt.text = value;
            }
        }
    }
    #endregion
    //------------------------------------------------------------------------------------------
    #region Functions
    // Use this for initialization
    void Awake()
    {
       if( transform.parent)
        parent = transform.parent.gameObject;
    
    }
    void Start()
    {
        qtrn = Quaternion.Euler(5, 0, 0);
        health = _health;
        plyd = true;
        damage = _damage;
        manaCost = _manaCost;
        cardName = _cardName;
        if (transform.parent)
        {
            ownr = transform.parent.parent.gameObject.GetComponent<PlayerInfo>().playerId;
            ownrTg = transform.parent.gameObject.tag;
            crdOwnr = transform.parent.parent.gameObject.GetComponent<PlayerInfo>();
        }
       // keyWords = "None";
       if(gameObject.GetComponent<Guard>())
       {
          // keyWords = "None";
           keyWords = "Guard"; 
       }
       
    }
    
    public void OnClck()
    {
        //EventManeger.Instance.PostNotification(EVENT_TYPE.CheckFor_Guards, this);
         //EventManeger.Instance.PostNotification(EVENT_TYPE.CARD_PLAY,this);
        if (GameLogic.turn == ownr)
        {
            if (crdOwnr.slctPnl.transform.childCount > 0)
            {
                if (ownrTg == "Grnd")
                { 
                   if(crdOwnr.slctPnl.transform.GetChild(0).gameObject.GetComponent<CardInfo>().ID==CardId.Item)
                   {
                    EventManeger.Instance.PostNotification(EVENT_TYPE.ITEM_UsdOnThis,this);
                    
                    }
                   else if (crdOwnr.slctPnl.transform.GetChild(0).gameObject.GetComponent<Spell>())
                   {
                    EventManeger.Instance.PostNotification(EVENT_TYPE.CARD_ATTACKED, this);
                    return;
                   
                    }
                }
            }
            
            EventManeger.Instance.PostNotification(EVENT_TYPE.CARD_SLCT, this);

        }
        else if (ownrTg == "Grnd" )
        {
            EventManeger.Instance.PostNotification(EVENT_TYPE.CARD_ATTACKED, this);
        }
       
        
    
    }
    #endregion

   
}
