using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using Map_generator;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Xml.Linq;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    private static MapGenerator _instance;
    public static MapGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (MapGenerator)FindObjectOfType(typeof(MapGenerator));
            }
            return _instance;
        }
    }

    string blockFilePath = Path.Combine(Application.streamingAssetsPath,"BlockINFO.xml");
    Sprite[] blockSprites;
    Texture2D[] blockTextures;
    void LoadBlockResourse(string path, Texture2D[] TextureList)
    {
        Debug.Log(path);
        IEnumerable<XElement> blockTypes;
        if (File.Exists(path))
        {
            XDocument blockINFO = XDocument.Load(blockFilePath);
            Debug.Log("read BlockINFO.xml");
            XElement rt = blockINFO.Root;
            Debug.Log(rt);
            blockTypes = rt.Elements();

            foreach (var blockType in blockTypes)
            {
                string tag = blockType.Element("Tag").Value;
                Debug.Log(tag);
            }

            foreach (var blockType in blockTypes)
            {
                string tag = blockType.Element("Tag").Value;
                Debug.Log(tag);
                Material[] blockTypeMaterial = SetMaterial(TextureList, blockType);

                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                if (gameObjects != null)
                {
                    Debug.Log("Find block tag object: " + gameObjects.Length);
                    foreach (GameObject gameObject in gameObjects)
                    {
                        Material[] objectMaterial = gameObject.GetComponent<MeshRenderer>().materials;

                        for (int j = 0; j < objectMaterial.Length; j++)
                        {
                            Debug.Log("Material numbers: " + objectMaterial.Length);
                            objectMaterial[j] = blockTypeMaterial[j];
                        }
                        gameObject.GetComponent<MeshRenderer>().materials = blockTypeMaterial;

                    }
                }
            }
        }
        else
        {
            Debug.Log("BlockINFO.xml is not exist!");
        }

    }
   

    public GameObject world;
    /*
    static int[] stoneIndex = { 1, 1, 1, 1, 1, 1 };
    static int[] soilIndex = { 2, 2, 2, 2, 2, 2 };
    static int[] glassIndex = { 49, 49, 49, 49, 49, 49 };
    static int[] mossyIndex_1 = { 1, 36, 40, 40, 40, 40 };
    static int[] mossyIndex_2 = { 1,36,53,53,53,53};
    static int[] waterIndex = { 203, 203, 203, 203, 203, 203 };
    static int[] badlandIndex = { 208, 208, 208, 208, 208, 208 };
    static int[] soilIndex_grass = { 2, 145, 3, 3, 3, 3 };
    static int[] soilIndex_snow = { 2, 66, 68, 68, 68, 68 };
    static int[] iceIndex = { 67, 67, 67, 67, 67, 67 };
    static int[] snowIndex = { 66, 66, 66, 66, 66, 66 };
    static int[] sandIndex = { 206, 206, 206, 206, 206, 206 };
    static int[] ashLandIndex = { 2, 78, 77, 77, 77, 77 };
    static int[] volcanicIndex = { 37, 37, 37, 37, 37, 37 };
    static int[] lavaIndex = { 235, 235, 235, 235, 235, 235 };
    public static Material[] stone;
    public static Material[] glass;
    public static Material[] mossy_1;
    public static Material[] mossy_2;
    public static Material[] water;
    public static Material[] ashLand;
    public static Material[] soil;
    public static Material[] soil_grass;
    public static Material[] soil_snow;
    public static Material[] ice;
    public static Material[] snow;
    public static Material[] sand;
    public static Material[] badland;
    public static Material[] volcanic;
    public static Material[] lava;

    public static Material[][] materialList = {stone, glass, mossy_1, mossy_2, water, ashLand,
        soil,soil_grass,soil_snow,ice,snow,sand,badland,volcanic,lava};

    public static int[][] materialIndexList = { stoneIndex, glassIndex, mossyIndex_1, mossyIndex_2,
        waterIndex, ashLandIndex, soilIndex,soilIndex_grass,soilIndex_snow,iceIndex,snowIndex,sandIndex,
        badlandIndex,volcanicIndex,lavaIndex};

    public static string[] tags = {"stone", "glass", "mossy_1", "mossy_2", "water","ashLand", "soil",
        "soil_grass", "soil_snow", "ice", "snow", "sand", "badland", "volcanic", "lava"};
    */
    
    /*
    public void ApplyMaterialList(Texture2D[] textureList, string blockINFOPath)
    {
        for (int i = 0; i < materialList.Length; i++)
        {
            MapGenerator.materialList[i] = SetMaterial(textureList, MapGenerator.materialIndexList[i]);
            Debug.Log("配置了资源" + MapGenerator.tags[i]);
        }
    }
    */

    Texture2D SpriteToTexture2D(Sprite sprite)
    {
        // 获取 Sprite 的纹理数据
        Texture2D texture = sprite.texture;

        // 获取 Sprite 的边界和矩形区域
        Rect rect = sprite.textureRect;
        Texture2D newTexture = new Texture2D((int)rect.width, (int)rect.height);
        newTexture.SetPixels(texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        newTexture.Apply();

        return newTexture;
    }

    Material[] SetMaterial(Texture2D[] TextureList, XElement blockType)
    {
        try 
        {
            Material[] materialList = new Material[6];
            XElement TypeIndex = blockType.Element("SpriteIndex");
            IEnumerable<XElement> IndexSettment = TypeIndex.Elements();


            foreach (XElement Index in IndexSettment)
            {
                switch (Index.Name.ToString())
                {
                    case "Bottom":
                        Material newMaterialBottom = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialBottom.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[0] = newMaterialBottom;
                        break;
                    case "Top":
                        Material newMaterialTop = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialTop.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[1] = newMaterialTop;
                        break;
                    case "Left":
                        Material newMaterialLeft = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialLeft.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[2] = newMaterialLeft;
                        break;
                    case "Right":
                        Material newMaterialRight = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialRight.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[3] = newMaterialRight;
                        break;
                    case "Back":
                        Material newMaterialBack = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialBack.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[4] = newMaterialBack;
                        break;
                    case "Front":
                        Material newMaterialFront = new Material(Shader.Find("Standard"));
                        Debug.Log(TypeIndex.ToString() + ":" + Index.Name.ToString() + " is Setting" + Index.Value);
                        newMaterialFront.SetTexture("_MainTex", TextureList[int.Parse(Index.Value)]);
                        materialList[5] = newMaterialFront;
                        break;
                }
            }
            return materialList;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    /*
    public static void ApplyMaterialToObject()
    {
        for (int i = 0; i < materialList.Length; i++)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(MapGenerator.tags[i]);
            foreach (GameObject gameObject in gameObjects)
            {
                Debug.Log("Find block tag object: " + gameObjects.Length);
                Material[] objectMaterial = gameObject.GetComponent<MeshRenderer>().materials;

                for (int j = 0; j < objectMaterial.Length; j++)
                {
                    objectMaterial[j] = MapGenerator.materialList[i][j];
                }

                gameObject.GetComponent<MeshRenderer>().materials = objectMaterial;
            }
        }
    }
    */

    string blockSetter(Point point,int i)
    {
        if (point.temperature > 0.9)
        {
            if (point.humidity > -0.4)
            {
                return "volcanic";
            }
            else
            {
                return "lava";
            }
        }
        else if (point.temperature >0.7 && point.temperature <= 0.9)
        {
            if(point.humidity > -0.4)
            {
                return "ashLand";
            }
            else
            {
                if (i <= 3)
                {
                    return "glass";
                }
                else
                {
                    return "stone";
                }
            }
        }
        else if (point.temperature <= 0.7 && point.temperature > 0.5)
        {
            if (point.humidity >0.5)
            {
                if (i <= 2)
                {
                    return "mossy_2";
                }
                else
                {
                    return "soil";
                }
            }
            else if (point.humidity > 0.1 && point.humidity <= 0.5)
            {
                return "mossy_1";
            }
            else if (point.humidity > -0.4 && point.humidity <= -0.1)
            {
                if (i ==0)
                {
                    return "badland";
                }
                else if (i>=1 && i<=3)
                {
                    return "soil";
                }
                else
                {
                    return "stone";
                }
            }
            else
            {
                return "sand";
            }
        }
        else if (point.temperature <= 0.4 && point.temperature >= -0.4)
        {
            if ( point.humidity > 0.6)
            {
                if(i <= 2)
                {
                    return "water";
                }
                else
                {
                    return "soil";
                }
            }
            else if (point.humidity > -0.3 && point.humidity <= 0.6)
            {
                if (i == 0)
                {
                    return "soil_grass";
                }
                else
                {
                    return "soil";
                }
            }
            else if (point.humidity <= -0.3 && point.humidity >= -0.6)
            {
                return "badland";
            }
            else 
            {
                return "sand";
            }
        }
        else if(point.temperature > -0.6 && point.temperature <= -0.4)
        {
            if (i == 0)
            {
                return "soil_snow";
            }
            else
            {
                return "soil";
            }
        }
        else
        {
            if (point.humidity > 0.6)
            {
                return "ice";
            }
            else if (point.humidity <= 0.6 && point.humidity> -0.25)
            {
                if (i == 0)
                {
                    return "soil_snow";
                }
                else
                {
                    return "soil";
                }
            }
            else
            {
                if (i <=　1)
                {
                    return "snow";
                }
                else
                {
                    return "stone";
                }
            }
        }
    }
    private MapGenerator() { }

    private int _mapWidth = 5;
    private int _mapLength = 5;
    private int _octave = 4;
    private double _persistence = 0.7;
    private int _magnification = 10;
    private string _mapSeed;
    private bool _isLinearNeed = true;
    public int mapWidth
    {
        set 
        { 
            _mapWidth = value;
            Debug.Log("new value has setted");
            Debug.Log(_mapWidth);
        }
        get { return _mapWidth; }
    }
    public int mapLength
    {
        set 
        { 
            _mapLength = value;
            Debug.Log("new value has setted");
            Debug.Log(_mapLength);
        }
        get { return _mapLength; }
    }

    public int octave
    {
        set { _octave = value; }
        get { return _octave; }
    }
    public double persistence
    {
        set { _persistence = value; }
        get { return _persistence; }
    }
    public int magnification
    {
        set { _magnification = value; }
        get { return _magnification;  }
    }
    public string mapSeed
    {
        set { _mapSeed = value; }
        get { return _mapSeed; }
    } 
    public bool IslinearNeed
    {
        set { _isLinearNeed = value; }
        get { return _isLinearNeed;}
    }

    public GameObject block;
    public void SetCamera(Map map)
    {
        float standardXScale = block.transform.localScale.x;
        float standardYScale = block.transform.localScale.y;
        float standardZScale = block.transform.localScale.z;

        Transform cameraTransform = transform.GetComponent<Transform>();
        if (map.IsLinearNeed)
        {
            cameraTransform.position = new Vector3(map.WidthBeGrid/2 * standardXScale,   map.LengthBeGrid * standardYScale, -map.LengthBeGrid / 2 * standardZScale);
            cameraTransform.eulerAngles = new Vector3(45,0,0);
        }
        else
        {
            cameraTransform.position = new Vector3(map.Width / 2 * standardXScale,  map.Length * standardYScale, -map.Length / 2 * standardZScale);
            cameraTransform.eulerAngles = new Vector3(45, 0, 0);
        }

    }
    public void GenerateWorld(Map map)
    {
        // 获取预制体的长宽高
        float standardXScale = block.transform.localScale.x;
        float standardYScale = block.transform.localScale.y;
        float standardZScale = block.transform.localScale.z;
        int i = 0;
        foreach (Point item in map)
        {
            if (map.IsLinearNeed)
            {
                int x = i / map.LengthBeGrid;
                int y = i % map.LengthBeGrid;
                // 实例化
                // 这里对于高度乘上了预制体的高，这是因为在MapBasicSetter里我添加了一个单位的概念。这里可以去除不同大小预制体而带来的影响。
                for (int num = 0; num < 5; num++)
                {
                    GameObject newBlock = Instantiate(block, new Vector3(x * standardXScale, (float)(item.altitude * map.Magnification - num) * standardYScale, y * standardZScale), Quaternion.Euler(-90, 0, 0));
                    newBlock.tag = blockSetter(item, num);
                    newBlock.transform.SetParent(world.transform);
                }
            }
            else
            {
                int x = i / map.Length;
                int y = i % map.Length;
                // 不需要线性连接时候的实例化方法。
                GameObject newBlock= Instantiate(block, new Vector3(x * standardXScale, (float)item.altitude * standardYScale, y * standardZScale), Quaternion.Euler(-90, 0, 0));
                newBlock.tag = blockSetter(item, 0);
                newBlock.transform.SetParent(world.transform);
            }
            i++;
        }
        worldCentralPoint.Instance.WorldCentralPointSetter();
        rotationAngle = 0;
        LoadBlockResourse(blockFilePath,blockTextures);
    }



    private void Awake()
    {
        try
        {
            blockSprites = Resources.LoadAll<Sprite>("");
            blockTextures = new Texture2D[blockSprites.Length];
            int i = 0;
            foreach (Sprite sprite in blockSprites)
            {
                Debug.Log($"Name: {sprite.name}, Type: {sprite.GetType()}");
                blockTextures[i] = SpriteToTexture2D(sprite);
                i++;
            }

            /*
            stone = SetMaterial(textures, stoneIndex);
            soil = SetMaterial(textures, soilIndex);
            soil_grass = SetMaterial(textures, soilIndex_grass);
            soil_snow = SetMaterial(textures, soilIndex_snow);
            ice = SetMaterial(textures, iceIndex);
            snow = SetMaterial(textures, snowIndex);
            sand = SetMaterial(textures, sandIndex);
            volcanic = SetMaterial(textures, volcanicIndex);
            lava = SetMaterial(textures, lavaIndex);
            badland = SetMaterial(textures, badlandIndex);
            */
            Map map = new Map(mapWidth, mapLength, octave, persistence, magnification);
            mapSeed = map.mapSeed;
            map.IsLinearNeed = IslinearNeed;
            SetCamera(map);
            GenerateWorld(map);
            Debug.Log($"world seed: {mapSeed}");
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    float rotationSpeed = 20f;
    float rotationAngle;
    // Update is called once per frame
    void Update()
    {
        // 获取预制体的长宽高
        float standardXScale = block.transform.localScale.x;
        float standardYScale = block.transform.localScale.y;
        float standardZScale = block.transform.localScale.z;

        if (Input.GetKey(KeyCode.E))
        {
            rotationAngle += rotationSpeed * Time.deltaTime;
            Debug.Log("has press key E");
            Transform cameraTransform = transform.GetComponent<Transform>();
            float xPostion = ((mapWidth - 1) * magnification + 1) * standardXScale * Mathf.Sin((1 + rotationAngle/360f) * MathF.PI) + ((mapWidth - 1) * magnification + 1) / 2 * standardXScale;
            float zPostion = ((mapLength - 1) * magnification + 1) * standardZScale * Mathf.Cos((1 + rotationAngle/360f) * MathF.PI) + ((mapWidth - 1) * magnification + 1) / 2 * standardZScale;
            float yPostion = ((mapLength - 1) * magnification + 1) * standardYScale;
            cameraTransform.position = new Vector3(xPostion, yPostion, zPostion);
            cameraTransform.LookAt(worldCentralPoint.Instance.CentralPoint);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotationAngle -= rotationSpeed * Time.deltaTime;
            Debug.Log("has press key Q");
            Transform cameraTransform = transform.GetComponent<Transform>();
            float xPostion = ((mapWidth - 1) * magnification + 1) * standardXScale * Mathf.Sin((1 + rotationAngle / 360f) * MathF.PI) + ((mapWidth - 1) * magnification + 1) / 2 * standardXScale;
            float zPostion = ((mapLength - 1) * magnification + 1) * standardZScale * Mathf.Cos((1 + rotationAngle / 360f) * MathF.PI) + ((mapWidth - 1) * magnification + 1) / 2 * standardZScale;
            float yPostion = ((mapLength - 1) * magnification + 1) * standardYScale;
            cameraTransform.position = new Vector3(xPostion, yPostion, zPostion);
            cameraTransform.LookAt(worldCentralPoint.Instance.CentralPoint);
        }
    }
}
