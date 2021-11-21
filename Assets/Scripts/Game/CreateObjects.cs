using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjects : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 Scale_Image;
    [SerializeField] private float finalStep;

    [Header("Test settings")]
    public bool tester = false;
    public int level;
    [SerializeField] private int objectCount;
    [SerializeField] private float radius;
    private int objInGame;

    [HideInInspector] public float memory_point;
    private int Number_of_levels;

    [Header("Objects")]
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private GameObject prefabs;
    [SerializeField] private PointCheck prefabPoint;
    [SerializeField] private GameObject poinSpawn;
    [SerializeField] private List<PointCheck> currentPoints;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private UI_buttons _Ui;
    [SerializeField] private GenerateObjects _generateMatrix;
    [SerializeField] private SoundPlay _soundPlay;
    [SerializeField] private GameObject collider_go;
    [SerializeField] private Text upper_text;
    private List<Cell> GoCell;
    private int tutor = 0;
    




    private int cellChecked;

   
   // public InterAd interAd;
    private GameObject spawnPos;
    void Start()
    {
        if (!tester)
        {
            LevelDifficultCHeck();
        }
        else
        {
            ResetSave();
        }
        if (level < 5)
        {
            if (level == 1)
            {
                upper_text.text = "MEMORIZE THE POSITION OF OBJECTS";
            } 
            else if (level == 2)
            {
                upper_text.text = "MEMORIZE THE POSITION AND INCLINATION OF OBJECTS";
            }
            else if (level > 2)
            {
                upper_text.text = "MEMORIZE THE EXACT POSITION AND INCLINATION OF OBJECTS";
            }
            resilutionSettings();
            _generateMatrix.GenerateMatrix();
         
            _Ui.StartGamePanel(level);
            objInGame = objectCount;
            cellChecked = 0;
            GenerateObjects();

        }
       

        else if (level >= 5)
        {
            _Ui.FinishGamePanel();

        }
        if (tutor == 1)
        {
            _Ui.ShowRotationTutorñialPanel(1);
        }
        else if (tutor == 2)
        {
            _Ui.ShowRotationTutorñialPanel(2);
        }

    }

    public void PointIsEmpty()
    {
        foreach (var item in currentPoints)
        {
            if(item.GetComponentInChildren<Cell>() != null)
            {
                item.GetComponent<PointCheck>().isEmpty = false;
            }
            else
            {
                item.GetComponent<PointCheck>().isEmpty = true;
                item.GetComponent<CircleCollider2D>().enabled = true;

            }
        }
    }

    private void LevelDifficultCHeck()
    {
        
        if (PlayerPrefs.HasKey("level"))
        {
            int levelNumber = PlayerPrefs.GetInt("level");
            if (levelNumber < 80)
            {
                level = 1;
            }
            else if (levelNumber >= 80 && levelNumber < 150)
            {
                level = 2;

            }
            else if (levelNumber >= 150 && levelNumber < 210)       //Óñëîâíî
            {
                level = 3;

            }
            else if (levelNumber >= 210 && levelNumber < 241)       //Óñëîâíî
            {
                level = 4;

            }
            else if (levelNumber >= 241)
            {
                level = 5;
                _Ui.FinishGamePanel();
            }
            LevelNumberCheck(levelNumber);
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("level", 1);
            int levelNumber = PlayerPrefs.GetInt("level");
            LevelNumberCheck(levelNumber);
        }
        if(PlayerPrefs.HasKey("points"))
        {
            memory_point = PlayerPrefs.GetFloat("points");
        }
        else
        {
            PlayerPrefs.SetFloat("points", 0);
            memory_point = PlayerPrefs.GetFloat("points");

        }
    }
    private void LevelNumberCheck(int level)
    {
        if (level >= 1 && level < 10)
        {
            objectCount = 3;
            if (level == 1)
            {
                tutor = 1;
            }
            
        }
        else if (level >= 10 && level < 20 || level >= 80 && level < 90)
        {
            objectCount = 4;
            if (level == 80)
            {
                tutor = 2;

            }
        }
        else if (level >= 20 && level < 30 || level >= 90 && level < 100 || level >= 150 && level < 160 || level >= 210 && level < 216)
        {
            objectCount = 5;
        }
        else if (level >= 30 && level < 40 || level >= 100 && level < 110 || level >= 160 && level < 170 || level >= 216 && level < 221)
        {
            objectCount = 6;
        }
        else if (level >= 40 && level < 50 || level >= 110 && level < 120 || level >= 170 && level < 180 || level >= 221 && level < 226)
        {
            objectCount = 7;
        }
        else if (level >= 50 && level < 60 || level >= 120 && level < 130 || level >= 180 && level < 190 || level >= 226 && level < 231)
        {
            objectCount = 8;
        }
        else if (level >= 60 && level < 70 || level >= 130 && level < 140 || level >= 190 && level < 200 || level >= 231 && level < 236)
        {
            objectCount = 9;
        }
        else if (level >= 70 && level < 80 || level >= 140 && level < 150 || level >= 200 && level < 210 || level >= 236 && level < 241)
        {
            objectCount = 10;
        }
        
      
    }
    private void ResetSave()
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("points");

    }
   

  
    /// <summary>
    /// óñòàíîâêà óðîâåíü ìåõàíèêè
    /// </summary>
    /// <param name="obj">ãåíàðèðóåòñÿ ïðè ñîçäàíèè îáúåêòà</param>
    private void LevelCheck(GameObject obj)
    {
        
        if( level == 1)
        {
            collider_go.SetActive(false);

            obj.GetComponentInChildren<Cell>().level = Cell.Type.first;
        }
        else if(level == 2)
        {
            collider_go.SetActive(false);

            obj.GetComponentInChildren<Cell>().level = Cell.Type.second;
        }
        else if (level == 3)
        {
            collider_go.SetActive(true);
            obj.GetComponentInChildren<Cell>().level = Cell.Type.third;
        }
        else if (level == 4)
        {
            collider_go.SetActive(true);

            obj.GetComponentInChildren<Cell>().level = Cell.Type.fourth;
        }
      
    }
    public void ReadyButton()
    {
        foreach (var item in currentPoints)
        {
            item.GetComponent<PointCheck>().SHowFinishImage();
        }
        
        for (int i = 0; i < _inventory.Cell.Count; i++)
        {
            _inventory.Cell[i].GetComponentInChildren<Cell>().FinishGameMove = true;
            _inventory.Cell[i].GetComponentInChildren<Cell>().HideRotationSPrites();

        }
        if (level == 1)
        {
            CheckCellFirstLvl();
        }
        else if (level == 2)
        {
            CheckCellSecondLvl();
        }
        else if (level == 3)
        {
            CheckCellThirdLvl();
        }
        else if (level == 4)
        {
            CheckCellThirdLvl();
        }
    }
    public void resetrotateImage(Cell cell)
    {
        foreach (var item in _inventory.Cell)
        {
            if (item.GetComponentInChildren<Cell>() != cell)
            {
                item.GetComponentInChildren<Cell>().MovePanel.SetActive(false);
                item.GetComponentInChildren<Cell>().RotatePanel_1.SetActive(false);
                item.GetComponentInChildren<Cell>().RotatePanel_2.SetActive(false);
            }

        }
    }
    public void CheckCellFirstLvl()
    {
        if (_inventory.currentInventory.Count <= 0)
        {
            for (int i = 0; i < currentPoints.Count; i++)
            {
                if(currentPoints[i].GetComponent<PointCheck>().ID == currentPoints[i].GetComponentInChildren<Cell>().ID)
                {
                    Debug.Log("true");
                    cellChecked++;
                }
            }
            float percent = (100 * cellChecked) / objectCount;
            FinishPercent(percent);
        }
    }

    public void CheckCellSecondLvl()
    {

        float fullRotationPercent = 0;
        for (int i = 0; i < _inventory.Cell.Count; i++)
        {
            for (int x = 0; x < currentPoints.Count; x++)
            {
                if (_inventory.Cell[i].GetComponentInChildren<Cell>().ID == currentPoints[x].GetComponent<PointCheck>().ID)
                {
                    
                    float angle = Quaternion.Angle(_inventory.Cell[i].transform.rotation, currentPoints[x].transform.rotation);
                    float percent = ((180 - angle) / 180) * 100;
                    fullRotationPercent += percent;
                }
            }
        }

        for (int i = 0; i < currentPoints.Count; i++)
        {
            if (currentPoints[i].GetComponent<PointCheck>().ID == currentPoints[i].GetComponentInChildren<Cell>().ID)
            {
                cellChecked++;
            }
        }
  
        float tmp = (fullRotationPercent + (100 * cellChecked)) / (objectCount * 2);
        FinishPercent(tmp);
    }
    public void CheckCellThirdLvl()
    {
        float fullRotationPercent = 0;
        for (int i = 0; i < _inventory.Cell.Count; i++)
        {
            for (int x = 0; x < currentPoints.Count; x++)
            {
                if (_inventory.Cell[i].GetComponentInChildren<Cell>().ID == currentPoints[x].GetComponent<PointCheck>().ID)
                {
                    float angle = Quaternion.Angle(_inventory.Cell[i].transform.rotation, currentPoints[x].transform.rotation);
                    float percent = ((180 - angle) / 180) * 100;
                    fullRotationPercent += percent;
                    var dis = Vector3.Distance(_inventory.Cell[i].transform.position , currentPoints[x].transform.position);
                    if ( dis > finalStep)
                    {
                        fullRotationPercent += 0;
                    }
                    else if (dis < finalStep )
                    {
                        var step = (dis / finalStep) * 100;
                        var finalPercent = 100 - step;
                        fullRotationPercent += finalPercent;
                        cellChecked++;
                    }
                }
            }
        }

        float tmp = (fullRotationPercent + (100 * cellChecked)) / (objectCount * 3);
        FinishPercent(tmp);

    }
    /// ÷åêè íà êîíåö óðîâíÿ  (êîíåö)

    private void FinishPercent(float percent)
    {

        memory_point += (percent / 100f);
        if (percent >= 60)
        {

            PlayerPrefs.SetFloat("points", memory_point);
            int levelNumber = PlayerPrefs.GetInt("level");
            levelNumber++;
            PlayerPrefs.SetInt("level", levelNumber);
            
        }
        _Ui.LevelResult(percent);
    }

    public void HidePoints()
    {
        if(level >= 3)
        {
            foreach (var item in currentPoints)
            {
                item.GetComponent<PointCheck>().HideAll();
            }
        }
        else
        {
            foreach (var item in currentPoints)
            {
                item.GetComponent<PointCheck>().HideImage();
            }

        }
    }

    public void InventoryCheck()
    {
        PointIsEmpty();
        if (_inventory.currentInventory.Count <= 0)
        {
            _Ui.ReadyPanel();
        }
        else
        {
            if(_inventory.currentInventory.Count == 10)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2600, 325);
            }
            if (_inventory.currentInventory.Count == 9)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2300, 325);
            }
            if (_inventory.currentInventory.Count == 8)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2100, 325);
            }
            if (_inventory.currentInventory.Count == 7)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1800, 325);
            }
            if (_inventory.currentInventory.Count == 6)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1550, 325);
            }
            if (_inventory.currentInventory.Count <= 5)
            {
                _inventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 325);
            }
        }
    }


    private void GenerateObjects()
    {
        int IDcell = 0;
        for (int i = 0; i < objectCount; i++)
        {
            if (currentPoints.Count == 0)
            {
                GeneratePos();
                Vector3 rotation = GenerateRotation();
                int rngSprite = Random.Range(0, _sprites.Count - 1);
                GameObject obj = Instantiate(prefabs, new Vector3(0, 0, 0), Quaternion.identity, _inventory.transform);  // inventory spawn
                obj.GetComponentInChildren<Image>().sprite = _sprites[rngSprite];
                obj.GetComponentInChildren<Image>().SetNativeSize();
                obj.GetComponentInChildren<Cell>().swap_parrent = _inventory.gameObject;
                PointCheck point = Instantiate(prefabPoint, spawnPos.transform.position, Quaternion.identity, spawnPos.transform);          // point spawn
                point.GetComponent<Image>().sprite = _sprites[rngSprite];
                point.GetComponent<Image>().SetNativeSize();
                point.transform.localScale = Scale_Image;
                point.gameObject.transform.Rotate(rotation);
                LevelCheck(obj);

                _inventory.Cell.Add(obj);
                _inventory.currentInventory.Add(obj);

                point.ID = IDcell;
                obj.GetComponentInChildren<Cell>().ID = IDcell;
                point.ShowImage();

                currentPoints.Add(point);
                IDcell++;
                spawnPos = null;
                _sprites.RemoveAt(rngSprite);
            }
            else
            {
                bool check = true;
                Vector3 rotation = GenerateRotation();
                GeneratePos();
                while (check)
                {
                    if (spawnPos == null)
                    {
                        GeneratePos();
              //                 Debug.Log("Regenerate");
                    }
                    else
                    {
              //                Debug.Log("NICE");

                        check = false;
                    }
                }

                int rngSprite = Random.Range(0, _sprites.Count - 1);

                GameObject obj = Instantiate(prefabs, new Vector3(0, 0, 0), Quaternion.identity, _inventory.transform);  // inventory spawn
                obj.GetComponentInChildren<Image>().sprite = _sprites[rngSprite];
                obj.GetComponentInChildren<Image>().SetNativeSize();
                PointCheck point = Instantiate(prefabPoint, spawnPos.transform.position, Quaternion.identity, spawnPos.transform);          // point spawn
                point.GetComponent<Image>().sprite = _sprites[rngSprite];
                point.GetComponent<Image>().SetNativeSize();
                point.transform.localScale = Scale_Image;


                point.gameObject.transform.Rotate(rotation);

                LevelCheck(obj);
                _inventory.Cell.Add(obj);
                _inventory.currentInventory.Add(obj);
                point.ID = IDcell;
                obj.GetComponentInChildren<Cell>().ID = IDcell;

                point.ShowImage();

                _sprites.RemoveAt(rngSprite);
                currentPoints.Add(point);
                IDcell++;
                spawnPos = null;

            }
        }
        
    }
   
        
    
   
    public void GeneratePos()
    {
        int x = Random.Range(2, _generateMatrix.widthMatrix - 2);
        int y = Random.Range(3, _generateMatrix.heithhMatrix - 1);
    //    Debug.Log(x);
    //    Debug.Log(y);
        spawnPos =  _generateMatrix.SearchGo(x, y);
        
    }
  
    private Vector3 GenerateRotation()
    {
          Vector3 rotation = new Vector3(0, 0, Random.Range(0, 360));
          return rotation;
    }


    private void resilutionSettings()
    {
        var width = Screen.width;
        var height = Screen.height;
        int x = 11;
        int y = 17;
        // äëÿ 1080*1920
        Vector2 size = new Vector2(50, 50);
        Vector2 spacing = new Vector2(50, 40);

        //ïëàíøåòû
        if (width == 1700 && height == 2560)
        {
            x = 12;
            y = 16;
            size = new Vector2(50, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 800 && height == 1280)
        {
            x = 12;
            y = 16;
            size = new Vector2(50, 50);
            spacing = new Vector2(45, 40);
        }
        if (width == 1200 && height == 1920)
        {
            x = 12;
            y = 16;
            size = new Vector2(50, 50);
            spacing = new Vector2(45, 40);
        }
        if (width == 1536 && height == 2048)
        {
            x = 13;
            y = 15;
            size = new Vector2(50, 50);
            spacing = new Vector2(45, 40);
        }
        if (width == 1440 && height == 2560)
        {
            x = 11;
            y = 17;
            size = new Vector2(50, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 1080 && height == 2160)            
        {
            x = 10;
            y = 18;
            size = new Vector2(50, 50);
            spacing = new Vector2(42, 45);
        }
        if (width == 828 && height == 1792)
        {
            x = 10;
            y = 19;
            size = new Vector2(42, 50);
            spacing = new Vector2(52, 45);
        }
        if (width == 1214 && height == 2688)
        {
            x = 10;
            y = 19;
            size = new Vector2(42, 50);
            spacing = new Vector2(52, 45);
        }
        if (width == 750 && height == 1334)
        {
            x = 10;
            y = 16;
            size = new Vector2(60, 60);
            spacing = new Vector2(45, 40);
        }
        if (width == 1125 && height == 2436)
        {
            x = 10;
            y = 20;
            size = new Vector2(50, 50);
            spacing = new Vector2(48, 40);
        }
        if (width == 900 && height == 1600)
        {
            x = 11;
            y = 18;
            size = new Vector2(50, 50);
            spacing = new Vector2(48, 40);
        }
        if (width == 720 && height == 1280)
        {
            x = 11;
            y = 18;
            size = new Vector2(50, 50);
            spacing = new Vector2(48, 40);
        }
        if (width == 900 && height == 1440)
        {
            x = 11;
            y = 16;
            size = new Vector2(55, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 768 && height == 1366)
        {
            x = 10;
            y = 18;
            size = new Vector2(60, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 1440 && height == 2960)
        {
            x = 9;
            y = 19;
            size = new Vector2(60, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 1440 && height == 2560)
        {
            x = 10;
            y = 18;
            size = new Vector2(60, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 1080 && height == 2160)
        {
            x = 9;
            y = 19;
            size = new Vector2(60, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 1080 && height == 2400)
        {
            x = 9;
            y = 20;
            size = new Vector2(60, 50);
            spacing = new Vector2(50, 40);
        }
        if (width == 720 && height == 900)
        {
            x = 13;
            y = 14;
            size = new Vector2(50, 50);
            spacing = new Vector2(50, 40);
        }

        _generateMatrix.widthMatrix = x;
        _generateMatrix.heithhMatrix = y;
        _generateMatrix.gameObject.GetComponent<GridLayoutGroup>().cellSize = size;
        _generateMatrix.gameObject.GetComponent<GridLayoutGroup>().spacing = spacing;
    }
}
