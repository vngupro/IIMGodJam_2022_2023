using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AILineShapeDetection : MonoBehaviour
{
    public ShapeType shape = ShapeType.None;
    private LineRenderer line;
    public List<Vector2> positions;
    [SerializeField] private List<float> coef;
    [SerializeField] private float tolerence;

    //pour x seulement
    private float moyenne;
    private float total;
    private float moyenneY;
    private float totalY;
    [SerializeField] private Vector2 centre;
    [SerializeField] private Vector2 centreCardinal;

    [SerializeField] private float lineLength;
    [SerializeField] private float supposeLineLength;
    [SerializeField] private List<float> positionsX;

    [SerializeField] private List<float> positionsY;
    [SerializeField] private List<Vector2> cardialsPoints;
    [SerializeField] private List<float> cardinalsLength;
    [SerializeField] private List<float> cardinalsLengthMaths;
    [SerializeField] private float yHigh;
    [SerializeField] private float yLow;
    [SerializeField] private float xHigh;
    [SerializeField] private float xLow;
    [SerializeField] private List<Vector2> yHighList;
    [SerializeField] private List<Vector2> yLowList;
    [SerializeField] private List<Vector2> xHighList;
    [SerializeField] private List<Vector2> xLowList;
    [SerializeField] private List<float> cardinalsCoef;
    private bool isCircle;
    private bool antiExploit;
    private Animator anim;
    public bool shot;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }
    public void AIShape(Vector2 startShape, Vector2 endShape)
    {
        //travaillons dans une liste
        for (int i = 0; i < line.positionCount; i++)
        {
            positions.Add(line.GetPosition(i));
            total += line.GetPosition(i).x;
            totalY += line.GetPosition(i).y;
            positionsX.Add(positions[i].x);
            positionsY.Add(positions[i].y);
        }
        moyenne = total / line.positionCount;
        moyenneY = totalY / line.positionCount;
        centre = new Vector2(moyenne, moyenneY);
        //est ce un trait 
        for (int i = 1; i < positions.Count; i++)
        {
            //evitons de diviser par 0
            if((positions[i].x - positions[0].x) !=0)
            {
                //sont ils plus ou moins alignés
                coef.Add((positions[i].y - positions[0].y) / (positions[i].x - positions[0].x));
            }
            //"perimetre"         
            lineLength += Mathf.Sqrt(Mathf.Pow(positions[i].x - positions[i - 1].x, 2) + Mathf.Pow(positions[i].y - positions[i - 1].y, 2));
        }
        coef.Sort();//tri
        positionsX.Sort();
        positionsY.Sort();
        tolerence += (tolerence / 20)*line.positionCount;
        supposeLineLength = Mathf.Sqrt(Mathf.Pow(positions[0].x - positions[positions.Count-1].x, 2) + Mathf.Pow(positions[0].y - positions[positions.Count - 1].y, 2));
        //si la droite n est pas verticale
        if(lineLength > 1)
        {
            if (Mathf.Sqrt((coef[0] - coef[coef.Count - 1]) * (coef[0] - coef[coef.Count - 1])) < tolerence && (supposeLineLength - supposeLineLength / 10 < lineLength && supposeLineLength + supposeLineLength / 10 > lineLength))//valeur absolue
            {
                shape = ShapeType.Line;
            }
            //si elle est verticale
            else if (moyenne - tolerence / 4 < positionsX[0] && moyenne + tolerence / 4 > positionsX[positionsX.Count - 1] && ValeurAbsolue(positions[0].y - positions[positions.Count - 1].y) > tolerence)
            {
                shape = ShapeType.Line;
                Debug.Log("v");

            }
        }


        if(shape != ShapeType.Line)
        {
            supposeLineLength = 0;
            yHigh = positionsY[positionsY.Count - 1];
            yLow = positionsY[0];
            xHigh = positionsX[positionsX.Count - 1];
            xLow = positionsX[0];
            centreCardinal = new Vector2((xHigh + xLow)/2,(yHigh + yLow)/2);
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].y == positionsY[positionsY.Count - 1])
                {
                    yHighList.Add(positions[i]);
                    
                }
                if (positions[i].y == positionsY[0])
                {
                    yLowList.Add(positions[i]);
                }
                if (positions[i].x == positionsX[positionsX.Count - 1])
                {
                    xHighList.Add(positions[i]);
                }
                if (positions[i].x == positionsX[0])
                {
                    xLowList.Add(positions[i]);
                }

            }
            cardialsPoints.Add(new Vector2(yHighList[(int)yHighList.Count / 2].x, yHigh));
            cardialsPoints.Add(new Vector2(xHigh, xHighList[(int)xHighList.Count / 2].y));

            cardialsPoints.Add(new Vector2(yLowList[(int)yLowList.Count / 2].x, yLow));
            cardialsPoints.Add(new Vector2(xLow, xLowList[(int)xLowList.Count / 2].y));

            for (int i = 1; i < 4; i++)
            {
                cardinalsLength.Add(Mathf.Sqrt(Mathf.Pow(cardialsPoints[i].x - cardialsPoints[i - 1].x, 2) + Mathf.Pow(cardialsPoints[i].y - cardialsPoints[i - 1].y, 2)));
            }
            cardinalsLength.Add(Mathf.Sqrt(Mathf.Pow(cardialsPoints[3].x - cardialsPoints[0].x, 2) + Mathf.Pow(cardialsPoints[3].y - cardialsPoints[0].y, 2)));
            for (int i = 0; i < cardialsPoints.Count; i++)
            {
                cardinalsLengthMaths.Add((cardinalsLength[i]*Mathf.PI/2));
                supposeLineLength += cardinalsLengthMaths[i];
            }
            supposeLineLength -= tolerence;
            for(int i = 0; i < cardinalsLengthMaths.Count; i++)
            {
                for(int j = 0; j < cardinalsLengthMaths.Count; j++)
                {
                    if(cardinalsLengthMaths[i] / cardinalsLengthMaths[j] != 1)
                    {
                        cardinalsCoef.Add(cardinalsLengthMaths[i] / cardinalsLengthMaths[j]);
                    }
                }
            }
            cardinalsCoef.Sort();
            isCircle = true;
            for (int i = 0; i < positions.Count; i++)
            {
                //rayon = perimetre/2PI
                float rayon = Mathf.Sqrt(Mathf.Pow((centreCardinal.x - positions[i].x), 2) + Mathf.Pow((centreCardinal.y - positions[i].y), 2));
                

                if (rayon - tolerence / 6 < lineLength/(Mathf.PI * 2) && rayon + tolerence / 6 > lineLength / (Mathf.PI * 2))
                {
                }
                else
                {
                    isCircle = false;
                }
            }
            if (isCircle || (ValeurAbsolue(supposeLineLength - lineLength) < tolerence && cardinalsCoef[0] + tolerence/4 > 1 && cardinalsCoef[cardinalsCoef.Count-1] - tolerence/4 < 2))
            {
                shape = ShapeType.Circle;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().type = 2;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().start = true;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().duration = 0.325f;
            }
            
        }
        else// = trait
        {
            //shake screen

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().type = 1;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().start = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().duration = 0.25f;
        }
        if (lineLength < 1)
        {
            shape = ShapeType.Point;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().type = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().duration = 0.125f;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().start = true;
        }
        if (shape == ShapeType.None)
        {
            anim.SetBool("wrongShape",true);
        }
        else
        {
            anim.SetBool("goodShape", true);
        }


    }
    private float ValeurAbsolue(float nb)
    {
        return Mathf.Sqrt(nb * nb);
    }


}

public enum ShapeType
{
    None,
    Circle,
    Line,
    Point,

}
