using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour
{

    private Transform block;
    public GameObject pivotPoint;
    private GameObject Block;
    private Rigidbody blockRB;
    public Color baseColor;
    public Color selectColor;
    public float cameraRotateSpeed;
    public float blockMoveSpeed;
    public float blockRotateSpeed;
    public float maneuverTime;

    void Start ()
    {

	}
	


	void Update ()
    {
        cameraMovement();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000);
            if (hit.collider && hit.transform.tag == "Block")
            {
                block = hit.transform ;
            }
        }

        if(block)
        {
            blockFunction();
            Block = block.gameObject;
            blockRB = Block.GetComponent<Rigidbody>();
            StartCoroutine(blockControl());
        }
    }

    public IEnumerator blockControl()
    {
        Block.GetComponent<Renderer>().material.color = selectColor;
        blockRB.isKinematic = true;
        yield return new WaitForSeconds(maneuverTime);

        Block.GetComponent<Renderer>().material.color = baseColor;
        blockRB.isKinematic = false;
    }

    public void blockFunction()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Block.transform.position += Block.transform.forward * blockMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Block.transform.position -= Block.transform.forward * blockMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Block.transform.Rotate(Vector3.up * blockRotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Block.transform.Rotate(Vector3.down * blockRotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Block.transform.position += block.up * blockMoveSpeed * Time.deltaTime;
        }
    }

    public void cameraMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pivotPoint.transform.Rotate(Vector3.up * cameraRotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pivotPoint.transform.Rotate(Vector3.down * cameraRotateSpeed * Time.deltaTime);
        }
    }
}
