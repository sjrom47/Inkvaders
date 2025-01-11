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
    public Color color;
    public Player lastSeenEnemyPlayer;
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
        //Debug.LogError(currentState);
    }

    public bool CanSeePlayer()
    {
        // any player?
        if (enemyTeamPlayers != null)
        {
            foreach (Player player in enemyTeamPlayers)
            {
                if (player.PlayerColor != color) 
                {
                    // player close enough?
                    if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
                    {
                        Vector3 targetDirection = player.transform.position - transform.position + Vector3.up * 0.1f;
                        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                        // player in field of view?
                        if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                        {
                            Ray ray = new Ray(transform.position, targetDirection);
                            RaycastHit hitInfo = new RaycastHit();
                            // any obstacle between player and enemy?
                            if (Physics.Raycast(ray, out hitInfo, sightDistance))
                            {
                                if (hitInfo.transform.gameObject.GetComponent<Player>() == player)
                                {
                                    lastSeenEnemyPlayer = player;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        //Debug.Log("Va a devolver false");
        return false;
    }
}
