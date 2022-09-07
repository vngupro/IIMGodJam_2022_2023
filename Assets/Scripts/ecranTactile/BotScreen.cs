using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScreen : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 screenSize = new Vector2(256, 196);
    [SerializeField] private LineRenderer line;
    [SerializeField] private Vector2 offSet;
    private LineRenderer line_copy;
    [SerializeField] private int pixelByUnits;

    private Vector2 startShape;
    void Start()
    {
        Transform mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Transform mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetButtonDown("Fire1") && mousePosition[0] >= 0 && mousePosition[0] < screenSize[0] && mousePosition[1] >= 0 && mousePosition[1] < screenSize[1])
        {
            startShape = new Vector3(mousePosition[0] / pixelByUnits +mainCamera.transform.position.x - offSet.x, mousePosition[1] / + mainCamera.transform.position.y - offSet.y, -1);
            line_copy = Instantiate(line);
            line_copy.positionCount = line_copy.positionCount + 1;
            line_copy.SetPosition(line_copy.positionCount - 1, new Vector3(mousePosition[0] / pixelByUnits + mainCamera.transform.position.x - offSet.x, mousePosition[1] / pixelByUnits + mainCamera.transform.position.y - offSet.y, -1));
            
        }
        if (Input.GetButton("Fire1") && mousePosition[0] >= 0 && mousePosition[0] < screenSize[0] && mousePosition[1] >= 0 && mousePosition[1] < screenSize[1] && line_copy != null)
        {
            if (!line_copy.GetComponent<LineAutoDestroy>().readyToDie)
            {
                if(line_copy.GetPosition(line_copy.positionCount - 1) != new Vector3(mousePosition[0] / pixelByUnits + mainCamera.transform.position.x - offSet.x, mousePosition[1] / pixelByUnits + mainCamera.transform.position.y - offSet.y, -1))
                {//evitons les doublons
                    line_copy.positionCount = line_copy.positionCount + 1;
                    line_copy.SetPosition(line_copy.positionCount - 1, new Vector3(mousePosition[0] / pixelByUnits + mainCamera.transform.position.x - offSet.x, mousePosition[1] / pixelByUnits + mainCamera.transform.position.y - offSet.y, -1));

                }
            }
            else
            {
                line_copy = Instantiate(line);
            }
        }
        if ((Input.GetButtonUp("Fire1") || !( mousePosition[0] >= 0 && mousePosition[0] < screenSize[0] && mousePosition[1] >= 0 && mousePosition[1] < screenSize[1])) && line_copy != null)
        {

            if (!line_copy.GetComponent<LineAutoDestroy>().readyToDie)
            {
                line_copy.GetComponent<LineAutoDestroy>().readyToDie = true;
                line_copy.GetComponent<AILineShapeDetection>().AIShape(startShape, new Vector3(mousePosition[0] / pixelByUnits + mainCamera.transform.position.x, mousePosition[1] / pixelByUnits + mainCamera.transform.position.y, -1));

            }

        }

    }
}
