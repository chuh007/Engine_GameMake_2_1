using System.Collections.Generic;
using System.Linq;
using _01Scripts.Core.EventSystem;
using _01Scripts.Players;
using DG.Tweening;
using UnityEngine;

namespace _01Scripts.TurnSystem
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private GameEventChannelSO turnEventChannel;
        [SerializeField] private Player player;

        private List<ITurnActor> _turnActors = new();
        private Queue<ITurnActor> _actionOrders = new();
        private bool _isBattleEnd = false;
        
        private void Awake()
        {
            uiChannel.AddListener<FadeCompleteEvent>(HandleFadeCompleteEvent);
            turnEventChannel.AddListener<TurnEndEvent>(HandleTurnEndEvent);
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<FadeCompleteEvent>(HandleFadeCompleteEvent);
            turnEventChannel.RemoveListener<TurnEndEvent>(HandleTurnEndEvent);
        }

        private void HandleFadeCompleteEvent(FadeCompleteEvent obj)
        {
            _turnActors = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ITurnActor>()
                .OrderByDescending(actor => actor.Speed)
                .ToList();
            SelectNextTurn();
        }
        
        private void HandleTurnEndEvent(TurnEndEvent obj)
        {
            SelectNextTurn();
        }

        private void SelectNextTurn()
        {
            if (_actionOrders.Count <= 0) FillTurnQueue();
            // _actionOrders.Dequeue().TurnAction();
            DOVirtual.DelayedCall(0.5f, () => _actionOrders.Dequeue().TurnAction());
        }

        private void FillTurnQueue()
        {
            int threshold = _turnActors[0].Speed;

            while (_actionOrders.Count < 10)
            {
                var readyActors = _turnActors
                    .Where(a => a.ActionValue >= threshold)
                    .ToList();

                if (readyActors.Count == 0)
                {
                    foreach (var actor in _turnActors)
                        actor.ActionValue += actor.Speed;

                    continue;
                }

                var executionActor = readyActors
                    .OrderByDescending(a => a.ActionValue)
                    .ThenByDescending(a => a.Speed)
                    .First();

                executionActor.ActionValue -= threshold;
                _actionOrders.Enqueue(executionActor);
            }
        }
    }
}