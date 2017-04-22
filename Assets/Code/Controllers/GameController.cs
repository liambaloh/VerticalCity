﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController obj;

    //Consts
    public const float BUILDABLE_ROOM_BUTTON_SPACING = 35f;

    //Variables
    [Header("Variables")]

    public List<RoomData> BuildableRooms;
    public RoomData SelectedBuildableRoom;
    public RoomPreview BuildableRoomPreview;

    private Tile _selectedTile = null;
    public Tile SelectedTile
    {
        get { return _selectedTile; }
        set
        {
            if (_selectedTile != value)
            {
                if (_selectedTile != null)
                {
                    _selectedTile.OnDeselect();
                }
                _selectedTile = value;
                if (_selectedTile != null)
                {
                    _selectedTile.OnSelect();
                }
            }
        }
    }

    private GameState _state = GameState.NONE;
    public GameState State
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                _state = value;

                Building.obj.OnModeChange();

                switch (_state)
                {
                    case GameState.PLAY_MODE:
                        BuildModeButtonImg.color = Color.white;
                        SelectedBuildableRoom = null;
                        BuildableRoomPreview.SetRoomToPreview(null);
                        break;
                    case GameState.BUILD_MODE:
                        BuildModeButtonImg.color = Color.green;
                        break;
                }
            }
        }
    }

    //Scene references
    [Header("Scene References")]
    public Image BuildModeButtonImg;
    public RectTransform BuildableRoomButtonsAnchor;

    //Sprites
    [Header("Sprites")]
    public Sprite TileBuilding;
    public Sprite TileBuilt;
    public Sprite TileAdd;

    public Sprite RoomElevator;
    public Sprite RoomOffice1;
    public Sprite RoomOffice2;

    //Prefabs
    [Header("Prefabs")]
    public GameObject PF_Floor;
    public GameObject PF_Tile;

    public GameObject PF_RoomElevator;
    public GameObject PF_RoomOffice1;
    public GameObject PF_RoomOffice2;

    public GameObject PF_BuildableRoomButton;

    private void Awake()
    {
        obj = this;
        BuildableRooms = new List<RoomData>();
        BuildableRooms.Add(new RoomData("Elevator", 3, RoomElevator, PF_RoomElevator));
        BuildableRooms.Add(new RoomData("Small OFfice", 5, RoomOffice1, PF_RoomOffice1));
        BuildableRooms.Add(new RoomData("Medium Office", 8, RoomOffice2, PF_RoomOffice2));
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < BuildableRooms.Count; i++)
        {
            RoomData roomData = BuildableRooms[i];
            GameObject go = Instantiate(PF_BuildableRoomButton);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.SetParent(BuildableRoomButtonsAnchor);
            rt.localScale = Vector3.one;
            rt.localRotation = Quaternion.identity;
            rt.anchoredPosition = new Vector2(0, i * -BUILDABLE_ROOM_BUTTON_SPACING);
            RoomSelectButton roomSelectButton = go.GetComponent<RoomSelectButton>();
            roomSelectButton.SetUp(roomData);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}