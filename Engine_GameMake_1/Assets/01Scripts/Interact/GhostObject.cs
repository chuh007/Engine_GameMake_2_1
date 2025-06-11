using System;
using System.Collections;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using _01Scripts.Players;
using Chuh007Lib.Dependencies;
using Code.Core.GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01Scripts.Interact
{
    public class GhostObject : MonoBehaviour, IInteractable, ISavable
    {
        [SerializeField] private EntityFinderSO playerFinder;
        [SerializeField] private GameEventChannelSO uiChannel;
        
        public string Name { get; private set; }
        public bool IsInteracted { get; private set; }


        private Player _player;

        private void Awake()
        {
            Name = "???";
            _player = playerFinder.Target as Player;
        }

        private void Update()
        {
            
        }

        public void Interact()
        {
            Debug.Log("유령발견ㄴㄴㄴㄴㄴㄴㄴ");
            _player.GetCompo<CharacterMovement>().CanManualMovement = false;
            IsInteracted = true;
            uiChannel.AddListener<FadeCompleteEvent>(HandleFadeComplete);
            StartCoroutine(GotoBattle());
        }

        private IEnumerator GotoBattle()
        {
            FadeEvent fadeEvt = UIEvents.FadeEvent;
            fadeEvt.isFadeIn = false;
            fadeEvt.fadeTime = 0.5f;
            yield return new WaitForSeconds(0.5f);
            uiChannel.RaiseEvent(fadeEvt);

        }

        private void HandleFadeComplete(FadeCompleteEvent obj)
        {
            gameObject.SetActive(false);
            uiChannel.RemoveListener<FadeCompleteEvent>(HandleFadeComplete);
            SceneManager.LoadScene("BattleScene");
        }

        [field: SerializeField] public SaveIdSO SaveID { get; private set; }

        [Serializable]
        public struct GhostObjectSaveData
        {
            public bool isInteracted;
        }
        
        public string GetSaveData()
        {
            GhostObjectSaveData data = new GhostObjectSaveData
            {
                isInteracted = IsInteracted,
            };
            return JsonUtility.ToJson(data);
        }

        public void RestoreData(string loadedData)
        {
            GhostObjectSaveData loadSaveData = JsonUtility.FromJson<GhostObjectSaveData>(loadedData);
            if (loadSaveData.isInteracted)
            {
                gameObject.SetActive(false);
            }
        }
    }
}