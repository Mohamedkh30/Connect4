﻿using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    /// <summary>
    ///     this class contains all message handlers
    /// </summary>
    internal partial class Client
    {

        // HANDLERS
        public static void SignUpResponseHandler(string recievedMessage)
        {
            SignUpResponseMessageContainer ResponseObj;
            ResponseObj = JsonConvert.DeserializeObject<SignUpResponseMessageContainer>(recievedMessage);

            string msg = ResponseObj.ToPlayerResponseMessage;
            string title = ResponseObj.ToPlayerMsgBoxTitle;
            if (ResponseObj.SignUpResponseCode == ResponseCode.Success)
            {
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //SignedInSuccessfullyEvent(new SignInResponseMessageContainer(ResponseCode.Success, "Signed Up Successfully ...Signing in for you", "Success"));
            }
            else if(ResponseObj.SignUpResponseCode == ResponseCode.Failed)
                MessageBox.Show(msg, title,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        public static void SignInResponseHandler(string recievedMessage)
        {
            SignInResponseMessageContainer ResponseObj;
            ResponseObj = JsonConvert.DeserializeObject<SignInResponseMessageContainer>(recievedMessage);

            string msg = ResponseObj.ToPlayerResponseMessage;
            string title = ResponseObj.ToPlayerMsgBoxTitle;
            if (ResponseObj.SignInResponseCode == ResponseCode.Success)
            {
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //fire event that signed in successfully
               SignedInSuccessfullyEvent(ResponseObj);
            }
            else if (ResponseObj.SignInResponseCode == ResponseCode.Failed)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // TODO:

        /// <summary>
        ///     TO BE REMOVED
        /// </summary>
        /// <param name="recievedMessage"></param>
        //public static void RoomStatusUpdate(string recievedMessage)
        //{
        //    RoomStatusUpdateMessageContainer RoomStatus;
        //    RoomStatus = JsonConvert.DeserializeObject<RoomStatusUpdateMessageContainer>(recievedMessage);

        //    // raise event of room status update
        //    if (RoomUpdateEvent != null)      //firing event when room update notification for ui to handle
        //    {
        //        RoomUpdateEvent(RoomStatus);
        //    }
        //}
        

        public static void CreateRoomHandler(string recievedMessage)
        {
            CreateRoomV2MessageContainer RoomStatus;
            RoomStatus = JsonConvert.DeserializeObject<CreateRoomV2MessageContainer>(recievedMessage);

            // raise event of room creation
            if (CreateRoomEvent != null)      //firing event when room create notification for ui to handle
            {
                CreateRoomEvent(RoomStatus);
            }
        }

        public static void OtherPlayerMoveHandler(string recievedMessage)
        {
            OtherPlayerMoveMessageContainer OtherPlayerMove;
            OtherPlayerMove = JsonConvert.DeserializeObject<OtherPlayerMoveMessageContainer>(recievedMessage);
            if(OtherPlayerMoveEvent!=null)
            {
                OtherPlayerMoveEvent(OtherPlayerMove);
            }
        }

        

        public static void PlayerJoinedRoomHandler(string recievedMessage)
        {
            JoinRoomMessageContainer RecievedObj;
            RecievedObj = JsonConvert.DeserializeObject<JoinRoomMessageContainer>(recievedMessage);

            // raise event of player joined room
            if(PlayerJoinedRoomEvent != null)
            {
                PlayerJoinedRoomEvent(RecievedObj);
            }
        }

        public static void PlayerLeftRoomHandler(string recievedMessage)
        {
            LeaveRoomMessageContainer RecievedObj;
            RecievedObj = JsonConvert.DeserializeObject<LeaveRoomMessageContainer>(recievedMessage);

            // raise event of player joined room
            if (PlayerLeftRoomEvent != null)
            {
                PlayerLeftRoomEvent(RecievedObj);
            }
        }

        public static void OpenRoomForJoinedPlayerHandler(string recievedMessage)
        {
            OpenRoomForJoinedPlayerMessageContainer RecievedObj;
            RecievedObj = JsonConvert.DeserializeObject<OpenRoomForJoinedPlayerMessageContainer>(recievedMessage);

            if (CanJoinRoomEvent != null)
            {
                CanJoinRoomEvent(RecievedObj);
            }
        }

        public static void StartGameHandler(string recievedMessage)
        {


            StartGameContainer RecievedObj;
            RecievedObj = JsonConvert.DeserializeObject<StartGameContainer>(recievedMessage);

            if(startgameEvent!= null)
            {
                startgameEvent(RecievedObj);
            }
            /*roomForm.Close();
            MessageBox.Show("other side");
            int size = RecievedObj.size;
            Color color = RecievedObj.color;
            Game game = new Game(size, color);
            
            game.Show();*/
        }

        public static void SendRoomToRoomListHandler(string recievedMessage)
        {
            SendRoomToRoomListMessageContainer RecievedObj;
            RecievedObj = JsonConvert.DeserializeObject<SendRoomToRoomListMessageContainer>(recievedMessage);

            if (RefreshRoomListEvent != null)
            {
                RefreshRoomListEvent(RecievedObj);
            }
        }

    }
}
