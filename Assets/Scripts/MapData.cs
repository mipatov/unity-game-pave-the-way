﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public int RotateOn;
    public int TotalBlockCount;
    public int MapWidth;
    public int MapHeight;
    public GameObject Map;
    public Vector3 PlayerPos;
    public GameObject PlayerPrafab;

    private GameObject MapPrefab;
    public GameObject player;
    private Camera cam;
    private CameraFollow camFollow;

    
    




    void Start()
    {   
        cam = Camera.main;
        camFollow = cam.GetComponent<CameraFollow>();
        
    }

void Update()
    {


        if (LevelManager.State != LevelManager.lvlState.Play) return;

        if(player.activeSelf==false){
            player.SetActive(true);
        }
        if(TotalBlockCount==GetComponentInChildren<BlockPaver>().DoneBlockCount){
            Destroy(player);
            // UpdateMap();
             if(LevelManager.State!= LevelManager.lvlState.Fin) {
                LevelManager.State= LevelManager.lvlState.Fin;
            }
        } 

        // if(fin&&Input.GetMouseButtonDown(0)){
        //         SceneManager.LoadScene("game");
        // }


    }

    
    public void LoadMap(){
        Debug.Log("Laod");
        MapPrefab = Resources.Load("maps/"+LevelManager.lvlNum) as GameObject;

        Map = Instantiate(MapPrefab, Vector3.zero,Quaternion.identity,transform);
 
       TotalBlockCount=0;
        var clearBlocks = new List<Transform>();

        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
           if(coll.gameObject.tag=="clear"){
               clearBlocks.Add(coll.transform);

           } 
        }
        TotalBlockCount = clearBlocks.Count;

        MapWidth=-1;
        MapHeight=-1;

        RaycastHit2D[] WidthRays = Physics2D.RaycastAll(new Vector3(0,0.5f,0), Vector2.right);
        foreach (var ray in WidthRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapWidth++;
                // Debug.DrawLine(new Vector3(0,0.5f,0), ray.point,Color.red,20);
            }    
        }
           
        RaycastHit2D[] HeightRays = Physics2D.RaycastAll(new Vector3(0.5f,0,0), Vector2.up);
        foreach (var ray in HeightRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapHeight++;
                // Debug.DrawLine(new Vector3(0.5f,0,0), ray.point,Color.red,20);
            }    
        }

        // if (RotateOn%2==0){
        //     int temp = MapHeight;
        //     MapHeight = MapWidth;
        //     MapWidth = temp;
        // }



        GetComponentInChildren<SetOutlineBorder>().UpdateBorder(this);
        camFollow.target = transform;
        transform.eulerAngles = new Vector3(0,0,90*RotateOn);
        Map.transform.localPosition = new Vector3(-MapWidth/2.0f,-MapHeight/2.0f,0);
        Map.transform.localEulerAngles = new Vector3(0,0,0);



        PlayerPos = clearBlocks[Random.Range(0, TotalBlockCount)].position  - new Vector3(0.5f,0.5f,0);

        player = Instantiate(PlayerPrafab,PlayerPos,Quaternion.identity,transform);
        player.SetActive(false);

    }

    public void DestroyMap(){
        Destroy(Map);
        Resources.UnloadAsset(MapPrefab);
    }
}
