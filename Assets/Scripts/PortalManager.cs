using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    Portal portal;
    int numItemsRemaining;

    void Start()
    {
        portal = FindObjectOfType<Portal>();
        portal.gameObject.SetActive(false);
        numItemsRemaining = GetNumRemaining();
    }

    void Update()
    {
        numItemsRemaining = GetNumRemaining();
        if (numItemsRemaining < 1) {
        portal.gameObject.SetActive(true);
        } 
    }

    public int GetNumRemaining()
    {
        int numBags = FindObjectsOfType<Bag>().Length;
        int numHandles = GetNumHandles();
        int numSwitches = GetNumSwitches();
        int total = numBags + numHandles + numSwitches;
        return total;
    }

    int GetNumHandles()
    {
        int n = 0;
        Handle[] handles = FindObjectsOfType<Handle>();
        if (handles.Length > 0) {
            foreach (Handle h in handles) 
            {
                if(h.GetIsInteractable()) { n++; }
            }
        }
        return n;
    }

    int GetNumSwitches()
    {
        int n = 0;
        Switch[] switches = FindObjectsOfType<Switch>();
        if (switches.Length > 0) {
            foreach (Switch s in switches) 
            {
                if(s.GetIsInteractable()) { n++; }
            }
        }
        return n;
    }
}
