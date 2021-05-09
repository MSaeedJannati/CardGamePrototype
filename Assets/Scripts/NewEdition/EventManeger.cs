using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public enum EVENT_TYPE
{
    TURN_END, CARD_PLAY, CARD_ATTACK, PLAYER_ATTACK, GAME_START, CARD_SLCT, CARD_ATTACKED/*,ITEM_SLCT*/, ITEM_UsdOnThis,Weapon_Equiped,CheckFor_Guards
};

public class EventManeger : MonoBehaviour
{
    #region Properties
    public static EventManeger Instance
    {
        get { return instance; }
        set { }
    }
    #endregion

    #region Variables
    private static EventManeger instance = null;

    public delegate void OnEvent(EVENT_TYPE Event_Type, Component Sender, object param = null);

    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
    #endregion
    #region Methods
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform.root.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(this);
        }
    }

    public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
    {
        List<OnEvent> ListenList = null;
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }
        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, Object Param = null)
    {
        List<OnEvent> ListenList = null;
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;
        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i](Event_Type, Sender, Param);
        }

    }

    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
        foreach (KeyValuePair<EVENT_TYPE, List<OnEvent>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                {
                    Item.Value.RemoveAt(i);
                }
            }
            if (Item.Value.Count > 0)
            {
                TmpListeners.Add(Item.Key, Item.Value);
            }
        }
        Listeners = TmpListeners;
    }
    void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }

    #endregion
}


