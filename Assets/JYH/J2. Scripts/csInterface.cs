﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamInterface
{
    public class Block
    {
        public Enum_CubeType type;//어떤블럭이냐
        public bool vis;//보여주냐 안보여주냐
        public GameObject obj;
        public bool top;
        public bool haveChild = false;

        public Block(Enum_CubeType t, bool v, GameObject obj, bool top)
        {
            type = t;
            vis = v;
            this.obj = obj;
            this.top = top;
        }
    }

    public enum Enum_ObjectType { NONE=0, GRASS, TREE, ROCK, FLOWER}
    public enum Enum_PreViewType { NONE=0, FIRE, TENT};
    public enum Enum_CubeType { DARKSOIL= 0, STON, GRASS, SOIL, SEND, WATER }
    public enum Enum_CubeState { NONE = 0 , GRASS1, GRASS2, TREE1, TREE2, ROCK1};
    public enum Enum_AnimState { IDLE=0, JUMP,WALK, ATTACK, TRACE, DIE};
    public enum Enum_DropItemType { NONE=0, STON,WOOD, FRUIT, WEAPON_SWORD};
    public enum Enum_PlayerUseItemType { HAND=0, BLUEPRINT, SHOVEL, AXE, PICKAXE, HOE, BLOCKSOIL, BLOCKWATER};
    public enum Enum_ObjectGrowthLevel { ZERO =0, ONE, TWO, THREE, FOUR, FIVE}

    public interface IPreViewBase
    {
        bool CanBuild { get; set; }
        bool ShowPreViewCheck { get; set; }
        int SizeX { get; set; }
        int SizeZ { get; set; }

        Vector3 TargetPos { get; set; }

        Enum_PreViewType PreViewType { get; set; }
        GameObject PreViewObj { get; set; }
        Material PreViewGreen { get; set; }
        Material PreViewRed { get; set; }
        GameObject BuildObj { get; set; }

        void ShowPreView(Vector3 pos, bool groundCheck);
        void HiedPreView();

        void CreateBuilding();
    }

    public interface ICubeInfo
    {
        Block CubeInfo { get; set; }
        Enum_CubeState CubeState { get; set; }
        
    }

    public interface IHighlighter
    {
        void OnHighlighter();
        void OffHighlighter();

        void StartAction(float dmg, Enum_PlayerUseItemType useItemType);
    }

    public interface IObjectStatus
    {
        float Hp { get; set; }
        float Stamina { get; set; }

        float HpFill();

        void SetHpDamaged(float dmg,Enum_PlayerUseItemType useItemType);
    }

    public interface IDropItem
    {
        GameObject[] DropItems { get; set; }

        void DropItemFct();
    }

    public interface IPhotonCallBack
    {
        void OnConnectedToPhoton();//포톤에 접속되었을 때
        void OnLeftRoom();//방에서 나갔을 때
        void OnMasterClientSwitched();//마스터클라이언트가 바뀌었을 때
        void OnPhotonCreateRoomFailed();//방만들기 실패
        void OnPhotonJoinRoomFailed();//방에 들어가기 실패
        void OnCreatedRoom();//방이 만들어 졌을 때
        void OnJoinedLobby();//로비에 접속했을 때
        void OnLeftLobby();//로비에서 나갔을 때
        void OnDisconnectedFromPhoton();//포톤 접속 종료
        void OnConnectionFail();//연결실패 void OnConnectionFail(DisconnectCause cause) { ... }
        void OnFailedToConnectToPhoton();// 포톤에 연결 실패 시  void OnFailedToConnectToPhoton(DisconnectCause cause) { ... }
        void OnReceivedRoomList();// 방목록 수신시 
        void OnReceivedRoomListUpdate();// 방목록 업데이트 수신시
        void OnJoinedRoom();// 방에 들어갔을 때
        void OnPhotonPlayerConnected();// 다른 플레이어가 방에 접속했을 때 void OnPhotonPlayerConnected(PhotonPlayer newPlayer) { ... }
        void OnPhotonPlayerDisconnected();// 다른 플레이어가 방에서 접속 종료시 void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) { ... }
        void OnPhotonRandomJoinFailed();// 렌덤하게 방으로 입장하는게 실패했을 때
        void OnConnectedToMaster();// 마스터로 접속했을 때   "Master_Join"
        void OnPhotonSerializeView();// 네트워크싱크시 void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { ...}
        void OnPhotonInstantiate();//네트워크 오브젝트 생성시 void OnPhotonInstantiate(PhotonMessageInfo info){ ... } "NetworkObj"
    }

    public interface IPhotonBase
    {
        //PhotonView 컴포넌트를 할당할 레퍼런스 
        PhotonView pv { get; set; }        

        //위치 정보를 송수신할 때 사용할 변수 선언 및 초기값 설정 
        Vector3 currPos { get; set; }
        Quaternion currRot {get; set;}
    }

    public interface IPhotonInTheRoomCallBackFct//[PunRPC]
    {
        /*
      PhotonView 컴포넌트의 Observe 속성이 스크립트 컴포넌트로 지정되면 PhotonView
      컴포넌트는 데이터를 송수신할 때, 해당 스크립트의 OnPhotonSerializeView 콜백 함수를 호출한다. 
     */
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info);

        void OnPhotonInstantiate(PhotonMessageInfo info);// 네트워크 객체 생성 완료시 자동 호출되는 함수

        void OnMasterClientSwitched(PhotonPlayer newMasterClient);// 마스터 클라이언트가 변경되면 호출

    }

    public interface IInventoryBase
    {       
        void CollectItem(Enum_DropItemType dropItemType, Item _item, int _count = 1);
    }

    public interface IGrowth
    {
        void GrowthDay();
        void GrowthRain();
    }

}
