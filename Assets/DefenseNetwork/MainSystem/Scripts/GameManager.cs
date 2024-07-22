using System.Collections;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO startGameChannel;

        [Space] [SerializeField] private float startDelay = 2.0f;

         private IEnumerator Start()
         {
             yield return new WaitForSeconds(startDelay);
             startGameChannel.RaiseEvent();
        }
    }
}