using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void LeaveVillage()
    {
        Debug.Log("Leaving village...");
    }

    public void OpenMenu(GameObject go)
    {
        go.SetActive(true);
    }
}
