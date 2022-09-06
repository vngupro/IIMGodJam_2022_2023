using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScreen : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 screenSize = new Vector2(256, 196);
    [SerializeField] private LineRenderer line;
    private LineRenderer line_copy;
    [SerializeField] private int pixelByUnits;
    private Vector2 startShape;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetButtonDown("Fire1") && mousePosition[0] >= 0 && mousePosition[0] < screenSize[0] && mousePosition[1] >= 0 && mousePosition[1] < screenSize[1])
        {
            startShape = new Vector3(mousePosition[0] / pixelByUnits, mousePosition[1] / pixelByUnits);
            line_copy = Instantiate(line);
            line_copy.positionCount = line_copy.positionCount + 1;
            line_copy.SetPosition(line_copy.positionCount - 1, new Vector3(mousePosition[0] / pixelByUnits, mousePosition[1] / pixelByUnits, -1));
            
        }
        if (Input.GetButton("Fire1") && mousePosition[0] >= 0 && mousePosition[0] < screenSize[0] && mousePosition[1] >= 0 && mousePosition[1] < screenSize[1] && line_copy != null)
        {
            if (!line_copy.GetComponent<LineAutoDestroy>().readyToDie)
            {
                if(line_copy.GetPosition(line_copy.positionCount - 1) != new Vector3(mousePosition[0] / pixelByUnits, mousePosition[1] / pixelByUnits, -1))
                {//evitons les doublons
                    line_copy.positionCount = line_copy.positionCount + 1;
                    line_copy.SetPosition(line_copy.positionCount - 1, new Vector3(mousePosition[0] / pixelByUnits, mousePosition[1] / pixelByUnits, -1));

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
                line_copy.GetComponent<AILineShapeDetection>().AIShape(startShape, new Vector3(mousePosition[0] / pixelByUnits, mousePosition[1] / pixelByUnits, -1));

            }

        }

    }
}
