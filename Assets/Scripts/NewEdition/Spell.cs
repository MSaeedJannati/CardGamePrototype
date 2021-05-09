using UnityEngine;
using System.Collections;
 public enum SpellType 
    { 
        heal=0,
        castDamage=1,


    };
public class Spell : CardInfo
{
   
    #region Varibales
    public CardInfo crdInfo = null;
    public SpellType splTyp = SpellType.heal;
    public GameLogic gmLgc = null;
    #endregion
    #region Properties
    #endregion
    #region Functions
    // Use this for initialization
    void Awake()
    {
        if (transform.parent)
            parent = transform.parent.gameObject;
    }
	void Start () {
        gmLgc = GameLogic.Instanse;
        crdInfo = gameObject.GetComponent<CardInfo>();

        qtrn = Quaternion.Euler(5, 0, 0);
        health = _health;
        plyd = false;
        damage = _damage;
        manaCost = _manaCost;
        cardName = _cardName;
        if (transform.parent)
        {
            ownr = transform.parent.parent.gameObject.GetComponent<PlayerInfo>().playerId;
            ownrTg = transform.parent.gameObject.tag;
            crdOwnr = transform.parent.parent.gameObject.GetComponent<PlayerInfo>();
        }
       
	}
    public bool doSpecial(SpellType splType, CardInfo subjCrd,CardInfo objCard=null)
    {

        switch (splType)
      {
            case SpellType.heal:
                return heal(subjCrd,objCard);
             break;
            case SpellType.castDamage:
              return  castDamage(subjCrd,objCard);
                break;
            default:
                return false;
                break;
    
      }
    }

    public bool heal(CardInfo healerCrd,CardInfo toBeHealadCrd)
    {
       
        
       if (!isFriendly(toBeHealadCrd, healerCrd))
            return false;
        if (!isGrndd(toBeHealadCrd))
            return false;
        if (plyd)
            return false;
        plyd = true;
        
        toBeHealadCrd.health += healerCrd.damage;
        return true;

    }

    public bool castDamage(CardInfo damageCasterCrd,CardInfo toBeDmgdCrd)
    {
        
        if (isFriendly(damageCasterCrd, toBeDmgdCrd))
            return false;
        if (!isGrndd(toBeDmgdCrd))
            return false;
        if (plyd)
            return false;
        plyd = true;
        toBeDmgdCrd.health -= damageCasterCrd.damage;
        return true;
    }

    public bool isFriendly(CardInfo crd1,CardInfo crd2)
    { 
      if(crd1.ownr==crd2.ownr)
      {
        return true;
      }
        else
      {
        return false;
      }
    }

    public bool isGrndd(CardInfo crd)
    { 
     if( crd.ownrTg=="Grnd")
     {
         return true;
     }
     else
     {
         return false;
     }
    }

    #endregion
}
