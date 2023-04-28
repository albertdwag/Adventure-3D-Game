using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private SaveManager manager;

    private void Start()
    {
        GameObject saveManagerObject = GameObject.Find("PFB_SaveManager");
        manager = saveManagerObject.GetComponent<SaveManager>();
    }

    public void Save()
    {
        manager.SaveItems();
    }
}
