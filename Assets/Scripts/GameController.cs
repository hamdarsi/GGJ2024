using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int howManyPlayers = 2;
    public float initialPlayerSpawnDistance = 6.5f;
    public GameObject playerPrefab;
    public Transform playerRoot;
    public CinemachineTargetGroup cinemachineTargetGroup;
    
    private List<PlayerController> players = new();

    private void Start()
    {
        StartGame();
    }
    
    private void StartGame()
    {
        var deltaAngle = 360f / howManyPlayers * Mathf.Deg2Rad;
        var angle = 0f;
        for (var i = 0; i < howManyPlayers; ++i)
        {
            var p = new Vector3(initialPlayerSpawnDistance * Mathf.Cos(angle), 0, initialPlayerSpawnDistance*Mathf.Sin(angle));
            var playerObject = Instantiate(playerPrefab, playerRoot);
            playerObject.transform.position = p;
            playerObject.transform.LookAt(Vector3.zero);
            var playerScript = playerObject.GetComponent<PlayerController>();
            playerScript.playerNumber = i + 1;
            players.Add(playerScript);
            angle += deltaAngle;
            
            cinemachineTargetGroup.AddMember(playerObject.transform, 1, 1);
        }
    }
}
