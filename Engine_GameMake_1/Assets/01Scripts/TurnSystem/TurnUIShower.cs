using System;
using System.Collections.Generic;
using _01Scripts.Core.EventSystem;
using Chuh007Lib.Dependencies;
using TMPro;
using UnityEngine;

namespace _01Scripts.TurnSystem
{
    public class TurnUIShower : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO turnEventChannel;
        [SerializeField] private TextMeshProUGUI[] turnTexts;
        [Inject] private TurnManager _turnManager;
        
        private Queue<ITurnActor> _turnActors = new();

        private void Awake()
        {
            turnEventChannel.AddListener<TurnStartEvent>(HandleTurnStartEvent);
            turnEventChannel.AddListener<TurnEndEvent>(HandleTurnEndEvent);
            turnEventChannel.AddListener<TurnUIEvent>(HandleTurnUIEvent);
        }



        private void OnDestroy()
        {
            turnEventChannel.RemoveListener<TurnStartEvent>(HandleTurnStartEvent);
            turnEventChannel.RemoveListener<TurnEndEvent>(HandleTurnEndEvent);
            turnEventChannel.RemoveListener<TurnUIEvent>(HandleTurnUIEvent);

        }

        private void HandleTurnStartEvent(TurnStartEvent obj)
        {
            _turnActors = _turnManager.ActionOrders;
        }
        
        private void HandleTurnUIEvent(TurnUIEvent evt)
        {
            _turnActors = _turnManager.ActionOrders;
        }

        private void HandleTurnEndEvent(TurnEndEvent evt)
        {
            for (int i = 0; i < 5; i++)
            {
                turnTexts[i].text = _turnActors.Dequeue().Name;
            }
        }
    }
}
