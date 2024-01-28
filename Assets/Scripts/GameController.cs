using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int howManyPlayers = 2;
    public float initialPlayerSpawnDistance = 6.5f;
    public GameObject playerPrefab;
    public Transform playerRoot;
    public CinemachineTargetGroup cinemachineTargetGroup;
    public Material[] playerMaterials;
    public AudioSource matchStartAudio;
    public AudioSource matchInProgressAudio;
    
    private readonly List<PlayerController> players = new();

    public TextMeshProUGUI[] scoreTexts;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        InitializePlayers();
        matchStartAudio.Play();
        yield return new WaitForSeconds(3);
        matchInProgressAudio.Play();
        foreach (var player in players)
            player.Unfreeze();
    }
    
    private void InitializePlayers()
    {
        var deltaAngle = 360f / howManyPlayers * Mathf.Deg2Rad;
        var angle = 180f;
        for (var i = 0; i < howManyPlayers; ++i)
        {
            var p = new Vector3(initialPlayerSpawnDistance * Mathf.Cos(angle), 0, initialPlayerSpawnDistance*Mathf.Sin(angle));
            var playerObject = Instantiate(playerPrefab, playerRoot);
            playerObject.transform.position = p;
            playerObject.transform.LookAt(Vector3.zero);
            cinemachineTargetGroup.AddMember(playerObject.transform, 1, 1);
            
            var playerScript = playerObject.GetComponent<PlayerController>();
            playerScript.playerNumber = i + 1;
            playerScript.material = playerMaterials[i];
            playerScript.scoreText = scoreTexts[i];
            playerScript.Freeze();
            players.Add(playerScript);
            
            angle += deltaAngle;
        }
    }
}
