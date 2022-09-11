using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShow : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void HideShow()
    {
        go.SetActive(!go.activeInHierarchy);
    }
}
