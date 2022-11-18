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

    private int isHideHash = Animator.StringToHash("isHide");
    public void HideShow()
    {
        //go.SetActive(!go.activeInHierarchy);
        var an = go.GetComponent<Animator>();
        bool isRun = an.GetBool(isHideHash);
        if (isRun)
        {
            an.SetBool(isHideHash,false);
        }
        else
        {
            an.SetBool(isHideHash,true);
        }
    }
}
