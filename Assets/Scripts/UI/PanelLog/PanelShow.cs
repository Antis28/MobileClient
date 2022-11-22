using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanelLog
{
    public class PanelShow : MonoBehaviour
    {
        [SerializeField]
        private GameObject go;

        private int isHideHash = Animator.StringToHash("isHide");

        public void HideShow()
        {
            //go.SetActive(!go.activeInHierarchy);
            var an = go.GetComponent<Animator>();
            bool isRun = an.GetBool(isHideHash);
            if (isRun) { an.SetBool(isHideHash, false); }
            else { an.SetBool(isHideHash, true); }
        }
    }
}
