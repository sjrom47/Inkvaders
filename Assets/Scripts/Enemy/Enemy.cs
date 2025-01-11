using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }

    [SerializeField]
    private string currentState;
    public Path path;
    private UnityEngine.Object[] enemyTeamPlayers;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
        enemyTeamPlayers = GameObject.FindObjectsOfType(typeof(Player), false);
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        Debug.Log(currentState);
    }

    public bool CanSeePlayer()
    {
        // any player?
        if (enemyTeamPlayers != null)
        {
            //Debug.Log("No son null");
            foreach (Player player in enemyTeamPlayers)
            {
                //Debug.Log(player.transform.position);
                // player close enough?
                if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
                {
                    Vector3 targetDirection = player.transform.position - transform.position + Vector3.up*0.1f;

                    Debug.Log(targetDirection);
                    float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                    Debug.Log(angleToPlayer);
                    // player in field of view?
                    if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                    {
                        Debug.Log("Se debería ver");
                        Ray ray = new Ray(transform.position, targetDirection);
                        RaycastHit hitInfo = new RaycastHit();
                        // any obstacle between player and enemy?
                        if (Physics.Raycast(ray, out hitInfo, sightDistance))
                        {
                            //Debug.Log("Se ve");
                            if (hitInfo.transform.gameObject == player && hitInfo.transform.gameObject != this)
                            {
                                //Debug.Log("Se devuelve");
                                return true;
                            }
                        }
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    }
                }
            }
        }
        //Debug.Log("Va a devolver false");
        return false;
    }
}
