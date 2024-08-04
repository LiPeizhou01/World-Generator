using Map_generator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.Globalization;
using System.Linq.Expressions;

public class UI : MonoBehaviour
{
    private static UI _instance;
    public static UI Instance
    {
        get 
        { 
            if (_instance == null)
            {
                _instance = (UI)FindObjectOfType(typeof(UI));
            }
            return _instance; 
        }
    }
    private UI() { }

    public Button startNewGenerate;
    public TMP_Text currentMapMessage;
    public ScrollRect scrollRect;
    public TMP_InputField mapWidthSetting;
    public TMP_InputField mapLengthSetting;
    public Toggle mapSeedNeedSetting;
    public Toggle magnificationNeedSetting;
    public TMP_InputField mapSeedSetting;
    public Dropdown magnificationSetting;
    public TMP_InputField mapOctaveSetting;
    public TMP_InputField mapPersistenceSetting;

    private int _mapWidth;
    private int _mapLength;
    private string _mapSeed;
    private int _mapOctave;
    private double _mapPersistence;
    public int mapWidth
    {
        set 
        { 
            if (value>=2 && value<= 50)
            {
                _mapWidth = value;
            }
            else if (value <= 1)
            {
                throw new Exception("Illegal input number!");
            }
            else if ( value > 50)
            {
                throw new Exception("The number is setting is too big");
            }
        }
        get { return _mapWidth; }
    }

    public int mapLength
    {
        set 
        {
            if (value >= 2 && value <= 50)
            {
                _mapLength = value;
            }
            else if (value <= 1)
            {
                throw new Exception("Illegal input number!");
            }
            else if (value > 50)
            {
                throw new Exception("The number is setting is too big");
            }
        }
        get { return _mapLength; }
    }

    public string mapSeed
    {
        set
        {
            if (value.Length == 8 && int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int _))
            {
                _mapSeed = value;
            }
            else
            {
                throw new ArgumentException("the seed is incorrect!");
            }
        }
        get
        {
            return _mapSeed;
        }
    }

    public int mapOctave
    {
        set
        {
            if (value > 0 && value < 11)
            {
                _mapOctave = value;
            }
            else if(value <=0)
            {
                throw new Exception("Illegal input number!");
            }
            else
            {
                throw new Exception("Input number is to big!");
            }
        }
        get
        {
            return _mapOctave;
        }
    }

    public double mapPersistence
    {
        set 
        {
            if (value >= 0 && value < 1.0)
            {
                _mapPersistence = value;
            }
            else
            {
                throw new Exception("Illegal input number!");
            }
        }
        get
        {
            return _mapPersistence;
        }
    }
    public void UpdateMapWidth(TMP_InputField input)
    {
        try
        {
            mapWidth = int.Parse(input.text);
            Debug.Log("mapwidthSetting:" + mapWidth);
            MessageBox.Log("mapwidthSetting:" + mapWidth);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }
    }

    public void UpdateMapLength(TMP_InputField input)
    {
        try
        {
            mapLength = int.Parse(input.text);
            Debug.Log("mapLengthSetting:" + mapLength);
            MessageBox.Log("mapLengthSetting:" + mapLength);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }
    }

    public void UpdateMapOctave(TMP_InputField input)
    {
        try
        {
            mapOctave = int.Parse(input.text);
            Debug.Log("mapOctaveSetting:" + mapLength);
            MessageBox.Log("mapOctaveSetting:" + mapLength);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }
    }

    public void UpdateMapPersistence(TMP_InputField input)
    {
        try
        {
            mapPersistence = double.Parse(input.text);
            Debug.Log("mapPersistenceSetting:" + mapPersistence);
            MessageBox.Log("mapPersistenceSetting:" + mapPersistence);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }
    }
    public void UpdateMapSeed()
    {
        try
        {
            mapSeed = mapSeedSetting.text;
            Debug.Log("mapSeed:" + mapSeed + "has been setted");
            MessageBox.Log("mapSeed:" + mapSeed + "has been setted");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }
    }

    public void UpdateMagnification()
    {
        MapGenerator.Instance.magnification = int.Parse(magnificationSetting.captionText.text);
        Debug.Log("magnification:"+ MapGenerator.Instance.magnification);
        MessageBox.Log("magnification:" + MapGenerator.Instance.magnification);
    }
    public void OnClickNewGenerate()
    {
        for (int i = 0; i < MapGenerator.Instance.world.transform.childCount;i++)
        {
            Transform childTransform = MapGenerator.Instance.world.transform.GetChild(i);
            Destroy(childTransform.gameObject);
        }

        try
        {
            MapGenerator.Instance.mapWidth = mapWidth;
            MapGenerator.Instance.mapLength = mapLength;
            MapGenerator.Instance.octave = mapOctave;
            MapGenerator.Instance.persistence = mapPersistence;
            if (mapSeedNeedSetting.isOn)
            {
                Map map = new Map(MapGenerator.Instance.mapWidth, MapGenerator.Instance.mapLength, MapGenerator.Instance.octave,
                                    MapGenerator.Instance.persistence, mapSeed, MapGenerator.Instance.magnification);
                MapGenerator.Instance.mapSeed = map.mapSeed;
                map.IsLinearNeed = MapGenerator.Instance.IslinearNeed;
                MapGenerator.Instance.SetCamera(map);
                MapGenerator.Instance.GenerateWorld(map);
                Debug.Log($"world seed: {map.mapSeed}");
                MessageBox.Log($"world seed: {map.mapSeed}");
            }
            else
            {
                Map map = new Map(MapGenerator.Instance.mapWidth, MapGenerator.Instance.mapLength, MapGenerator.Instance.octave,
                    MapGenerator.Instance.persistence, MapGenerator.Instance.magnification);
                MapGenerator.Instance.mapSeed = map.mapSeed;
                map.IsLinearNeed = MapGenerator.Instance.IslinearNeed;
                MapGenerator.Instance.SetCamera(map);
                MapGenerator.Instance.GenerateWorld(map);
                Debug.Log($"world seed{map.mapSeed}");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            MessageBox.Log(e.Message);
        }

        if (MapGenerator.Instance.mapSeed != null)
        {
            currentMapMessage.text = "地图种子:" + MapGenerator.Instance.mapSeed + "\n" +
                                  "地图长:" + MapGenerator.Instance.mapLength + "\n" +
                                  "地图宽:" + MapGenerator.Instance.mapWidth + "\n" +
                                  "波形叠加数:" + MapGenerator.Instance.octave + "\n" +
                                  "衰减率:" + MapGenerator.Instance.persistence + "\n" +
                                  "是否线性连接:" + MapGenerator.Instance.IslinearNeed + "\n" +
                                  "等比扩大比率:" + MapGenerator.Instance.magnification;

            MapGenerator.Instance.mapSeed = null;
        }
        else
        {
            currentMapMessage.text = "请输入地图参数，并生成世界";
        }
    }

    void MapSeedNeedIsOn()
    {
        if (mapSeedNeedSetting.isOn)
        {
            mapSeedSetting.interactable = true;
            Debug.Log("启用用户地图种子设置选择!");
            MessageBox.Log("Map Seed Setting is on!");
        }
        else
        {
            mapSeedSetting.interactable = false;
            Debug.Log("停用用户地图种子设置选择!");
            MessageBox.Log("Map Seed Setting is off!");
        }
    } 

    void MagnificationNeedIsOn()
    {
        if (magnificationNeedSetting.isOn)
        {
            magnificationSetting.interactable = true;
            MapGenerator.Instance.IslinearNeed = true;
            MapGenerator.Instance.magnification = int.Parse(magnificationSetting.captionText.text);
            Debug.Log("启用等比扩大下拉选单");
            MessageBox.Log("Magnification Setting is on!");
        }
        else
        {
            magnificationSetting.interactable = false;
            MapGenerator.Instance.IslinearNeed = false;
            MapGenerator.Instance.magnification = 1;
            Debug.Log("停用等比扩大下拉选单");
            MessageBox.Log("Magnification Setting is off!");
        }
    }
    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        if ( MapGenerator.Instance.mapSeed != null)
        {
            currentMapMessage.text = "地图种子:" + MapGenerator.Instance.mapSeed + "\n" +
                                  "地图长:" + MapGenerator.Instance.mapLength + "\n" +
                                  "地图宽:" + MapGenerator.Instance.mapWidth + "\n" +
                                  "波形叠加数:" + MapGenerator.Instance.octave + "\n" +
                                  "衰减率:" + MapGenerator.Instance.persistence + "\n" +
                                  "是否线性连接:" + MapGenerator.Instance.IslinearNeed + "\n" +
                                  "等比扩大比率:" + MapGenerator.Instance.magnification;

            mapWidth = MapGenerator.Instance.mapWidth;
            mapLength = MapGenerator.Instance.mapLength;
            MapGenerator.Instance.mapSeed = null;
        }
        else
        {
            currentMapMessage.text = "请输入地图参数，并生成世界";
        }

        if ( startNewGenerate != null)
        {
            startNewGenerate.onClick.AddListener(UI.Instance.OnClickNewGenerate);
        }

        if (mapWidthSetting != null)
        {
            mapWidthSetting.contentType = TMP_InputField.ContentType.IntegerNumber;
            mapWidthSetting.lineType = TMP_InputField.LineType.SingleLine;
            mapWidthSetting.onEndEdit.AddListener(delegate {UpdateMapWidth(mapWidthSetting); });
        }
        if (mapLengthSetting != null)
        {
            mapLengthSetting.contentType = TMP_InputField.ContentType.IntegerNumber;
            mapLengthSetting.lineType = TMP_InputField.LineType.SingleLine;
            mapLengthSetting.onEndEdit.AddListener(delegate { UpdateMapLength(mapLengthSetting); });
        }

        if (mapSeedNeedSetting != null && mapSeedSetting != null)
        {
            mapSeedSetting.contentType = TMP_InputField.ContentType.Alphanumeric;
            mapSeedSetting.lineType = TMP_InputField.LineType.SingleLine;
            mapSeedSetting.characterLimit = 8;
            mapSeedSetting.onEndEdit.AddListener(delegate { UpdateMapSeed(); });
            mapSeedNeedSetting.onValueChanged.AddListener(delegate { MapSeedNeedIsOn(); });
        }

        if (magnificationNeedSetting != null && magnificationSetting != null)
        {
            magnificationSetting.value = 1;
            magnificationSetting.onValueChanged.AddListener(delegate { UpdateMagnification(); });
            magnificationNeedSetting.onValueChanged.AddListener(delegate { MagnificationNeedIsOn(); });
        }

        if (mapOctaveSetting != null)
        {
            mapOctaveSetting.contentType = TMP_InputField.ContentType.IntegerNumber;
            mapOctaveSetting.lineType = TMP_InputField.LineType.SingleLine;
            mapOctaveSetting.onEndEdit.AddListener(delegate { UpdateMapOctave(mapOctaveSetting); });
        }

        if (mapPersistenceSetting != null)
        {
            mapPersistenceSetting.lineType = TMP_InputField.LineType.SingleLine;
            mapPersistenceSetting.onEndEdit.AddListener(delegate{ UpdateMapPersistence(mapPersistenceSetting); });
        }

        if (scrollRect != null)
        {
            scrollRect.vertical = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
