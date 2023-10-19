using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit {
    public class FailedMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Invoke(nameof(LoadAgain), 2f);
        }


        private void LoadAgain()
        {
            GameManager.Instance.OnLevelFailed();
        }
    }
}