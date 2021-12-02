using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed; // speed of camera?
    private float currentPosX; // position of player
    private Vector3 velocity = Vector3.zero;

    // Camera follows the player
    [SerializeField] private Transform player; // refernce to the player, as that is the object we want to be following
    [SerializeField] private float aheadDistance; // tweak how far the camera can look forward
    [SerializeField] private float cameraSpeed; //speed of which the camera will go forward
    private float lookAhead;

    // Update is called once per frame
    private void Update()
    {
        // follow the player...
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z); //...only on the x-axis (for now), player.position.y to follow y axis also
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed); // make the camera look ahead, when facing either left or right
    }

    // this is irrelivant, for a different camera movement which was not implemented (camera shifts for each room)
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
