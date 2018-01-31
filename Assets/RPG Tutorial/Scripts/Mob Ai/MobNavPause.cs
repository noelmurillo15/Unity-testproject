﻿// Allan Murillo : Unity RPG Core Test Project
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


namespace RPG
{
    public class MobNavPause : MonoBehaviour
    {

        private MobMaster mobMaster;
        private NavMeshAgent myNavMeshAgent;

        [SerializeField] float pauseTime = 1f;


        void Initialize()
        {
            mobMaster = GetComponent<MobMaster>();
            if (GetComponent<NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            }
        }

        void OnEnable()
        {
            Initialize();
            mobMaster.EventMobDie += DisableThis;
            mobMaster.EventMobDeductHealth += PauseNavMeshAgent;
        }

        void OnDisable()
        {
            mobMaster.EventMobDie -= DisableThis;
            mobMaster.EventMobDeductHealth -= PauseNavMeshAgent;
        }

        void PauseNavMeshAgent(int num)
        {
            if (myNavMeshAgent != null)
            {
                if (myNavMeshAgent.enabled)
                {
                    myNavMeshAgent.ResetPath();
                    mobMaster.IsNavPaused = true;
                    StartCoroutine(RestartNavMeshAgent());
                }
            }
        }

        IEnumerator RestartNavMeshAgent()
        {
            yield return new WaitForSeconds(pauseTime);
            mobMaster.IsNavPaused = false;
        }

        void DisableThis()
        {
            StopAllCoroutines();
        }
    }
}