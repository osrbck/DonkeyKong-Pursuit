using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit {
    public class GameBehaviur : MonoBehaviour
    {
        protected GameManager _gameManager;
        public virtual void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
    }
}