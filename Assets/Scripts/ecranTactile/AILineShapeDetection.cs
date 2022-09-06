using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILineShapeDetection : MonoBehaviour
{
    public string shape = "";
    private LineRenderer line;
    [SerializeField] private List<Vector2> positions;
    [SerializeField] private List<float> coef;
    [SerializeField] private float tolerence;

    //pour x seulement
    private float moyenne;
    private float total;
    [SerializeField] private List<float> positionsX;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    public void AIShape(Vector2 startShape, Vector2 endShape)
    {
        //travaillons dans une liste
        for (int i = 0; i < line.positionCount; i++)
        {
            positions.Add(line.GetPosition(i));
            total += line.GetPosition(i).x;
            positionsX.Add(positions[i].x);
        }
        moyenne = total / line.positionCount;
        //est ce un trait 
        for (int i = 1; i < positions.Count; i++)
        {
            //evitons de diviser par 0
            if((positions[i].x - positions[0].x) !=0)
            {
                //sont ils plus ou moins alignés
                coef.Add((positions[i].y - positions[0].y) / (positions[i].x - positions[0].x));
            }
            //on récup la différence des 2 coef les plus eloignés           

        }
        coef.Sort();//tri
        positionsX.Sort();
        tolerence += (tolerence / 25)*line.positionCount;
        //si la droite n est pas verticale
        if (Mathf.Sqrt((coef[0] - coef[coef.Count - 1]) * (coef[0] - coef[coef.Count - 1])) < tolerence)//valeur absolue
        {
            Debug.Log("trait");
            Debug.Log(Mathf.Sqrt((coef[0] - coef[coef.Count - 1]) * (coef[0] - coef[coef.Count - 1])));
        }
        //si elle est verticale
        else if (moyenne - tolerence/2 < positionsX[0] && moyenne + tolerence/2 > positionsX[positionsX.Count - 1] && ValeurAbsolue(startShape.y - endShape.y) > tolerence)
        {
            Debug.Log("trait v");
            Debug.Log(moyenne + tolerence / 2);
            Debug.Log(moyenne - tolerence / 2);
        }
    }
    private float ValeurAbsolue(float nb)
    {
        return Mathf.Sqrt(nb * nb);
    }


}
