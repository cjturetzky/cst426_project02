using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeSpawner : MonoBehaviour
{
  public enum MazeGenerationAlgorithm
  {
    PureRecursive
  }
  public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
  public bool FullRandom = false;
  public int RandomSeed = 12345;
  public GameObject Wall = null;
  public GameObject Pillar = null;
  public int Rows = 5;
  public int Columns = 5;
  public float CellWidth = 5;
  public float CellHeight = 5;
  public bool AddGaps = false;
  public GameObject GoalPrefab = null;
  private BasicMazeGenerator mMazeGenerator = null;
  public GameObject PlayerPrefab = null;
  // Start is called before the first frame update
  void Start()
  {
    GenerateNewMaze();  
  }

  public void ClearMaze()
  {
    List<GameObject> children = new List<GameObject>();
    foreach (Transform child in transform)
    {
      children.Add(child.gameObject);
    }
    children.ForEach(child => Destroy(child));
  }

  public void GenerateNewMaze()
  {
    if (!FullRandom)
    {
      Random.InitState(RandomSeed);
    }
    switch (Algorithm)
    {
      case MazeGenerationAlgorithm.PureRecursive:
        mMazeGenerator = new RecursiveMazeAlgorithm(Rows, Columns);
        break;
    }
    mMazeGenerator.GenerateMaze();
    for (int row = 0; row < Rows; row++)
    {
      for (int column = 0; column < Columns; column++)
      {
        float x = column * (CellWidth + (AddGaps ? .2f : 0));
        float y = row * (CellHeight + (AddGaps ? .2f : 0));
        MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
        GameObject tmp;
        if (cell.WallRight)
        {
          tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, y, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 90)) as GameObject;// right
          tmp.transform.parent = transform;
        }
        if (cell.WallFront)
        {
          tmp = Instantiate(Wall, new Vector3(x, y + CellHeight / 2, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
          tmp.transform.parent = transform;
        }
        if (cell.WallLeft)
        {
          tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, y, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 270)) as GameObject;// left
          tmp.transform.parent = transform;
        }
        if (cell.WallBack)
        {
          tmp = Instantiate(Wall, new Vector3(x, y - CellHeight / 2, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;// back
          tmp.transform.parent = transform;
        }
        if (cell.IsGoal && GoalPrefab != null)
        {
          tmp = Instantiate(GoalPrefab, new Vector3(x, y, -1), Quaternion.Euler(0, 0, 0)) as GameObject;
          tmp.transform.parent = transform;
        }
      }
    }
    if (Pillar != null)
    {
      for (int row = 0; row < Rows + 1; row++)
      {
        for (int column = 0; column < Columns + 1; column++)
        {
          float x = column * (CellWidth + (AddGaps ? .2f : 0));
          float y = row * (CellHeight + (AddGaps ? .2f : 0));
          GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, y - CellHeight / 2, 0), Pillar.transform.rotation) as GameObject;
          tmp.transform.parent = transform;
        }
      }
    }
    
    
    GameObject player = Instantiate(PlayerPrefab, new Vector3((Columns-1), (Rows-1), 0), PlayerPrefab.transform.rotation) as GameObject;
    player.transform.parent = transform;
    this.transform.Translate(new Vector3(-2.5f, -2.5f, 0));
  }
}