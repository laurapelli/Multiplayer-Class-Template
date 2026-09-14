using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class OnlineElementsEnable : NetworkBehaviour
{
    public GameObject[] m_ObjectsToEnableInLocal;
    public GameObject[] m_ObjectsToEnableInRemote;
    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner)
        {
            foreach(var item in m_ObjectsToEnableInLocal)
            {
                item.SetActive(true);
            }
            foreach (var item in m_ObjectsToEnableInRemote)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (var item in m_ObjectsToEnableInLocal)
            {
                item.SetActive(false);
            }
            foreach (var item in m_ObjectsToEnableInRemote)
            {
                item.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
