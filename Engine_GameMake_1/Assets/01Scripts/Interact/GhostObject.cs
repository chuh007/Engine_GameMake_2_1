using System;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using _01Scripts.Players;
using Chuh007Lib.Dependencies;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01Scripts.Interact
{
    public class GhostObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private EntityFinderSO playerFinder;
        [SerializeField] private GameEventChannelSO uiChannel;
        
        public string Name { get; private set; }
        

        private void Awake()
        {
            Name = "???";
        }

        public void Interact()
        {
            Debug.Log("유령발견ㄴㄴㄴㄴㄴㄴㄴ");
            playerFinder.Target.GetCompo<CharacterMovement>().CanManualMovement = false;
            FadeEvent fadeEvt = UIEvents.FadeEvent;
            fadeEvt.isFadeIn = false;
            fadeEvt.fadeTime = 0.5f;
                
            uiChannel.AddListener<FadeCompleteEvent>(HandleFadeComplete);
            uiChannel.RaiseEvent(fadeEvt);
        }

        private void HandleFadeComplete(FadeCompleteEvent obj)
        {
            uiChannel.RemoveListener<FadeCompleteEvent>(HandleFadeComplete); //이벤트 제거후 씬변환.
            SceneManager.LoadScene("BattleScene");
        }
    }
}