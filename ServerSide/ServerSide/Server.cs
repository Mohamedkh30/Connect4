﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ServerSide
{

    internal class Server
    {
        private List<Player> _players;
        private IPAddress _IP = IPAddress.Parse("127.0.0.1");                   
        private int _PORT = 5500;                                               
        private TcpListener _tcpListener;
        private string _gameHistoryPath;
        private string _userDataPath;

        public event Action<object, string> _playerConnectedEvent;
        public Server()
        {
            _tcpListener = new TcpListener(_IP, _PORT);
            _players = new List<Player>();
        }
        public Server(string ip, int port)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            _players = new List<Player>();
            
        }

        public async void Start()       //Starts listening for incoming connections
        {
            _tcpListener.Start();
            
                while (true)
                {
                    try { 
                        TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
                        Player newPlayer = new Player(tcpClient);                               //create new player from the connected tcpClient
                        
                        newPlayer._recievedMessageEvent += ClientRecievedMessageHandler;        //subscribe into client recieved message event
                        newPlayer._PlayerDisconnectedEvent += PlayerDisconnectedMessageHandler; //subscribe into client disconnect event
                        _players.Add(newPlayer);                                                //add the connected player to the list

                        
                        if (_playerConnectedEvent != null)                                      //fires event when player is Connect
                    {
                        _playerConnectedEvent(this, newPlayer._userName);
                        }
                }
                    catch (ObjectDisposedException e)                                           //on server stop
                    {
                        break;
                    }
                }
        }
        public void Stop()                               
        {
            foreach (Player player in _players)
            {
                player._session._streamWriter.WriteLine("!DIS");        //send disconnect Formatted msg to all player
                player.EndClient();                                     //close the players sessions
            }
            _tcpListener.Stop();
            _players.Clear();                                           //clear
        }
        public void Broadcast(string msg)                               //This method sends a message to all connected clients
        {
            foreach (Player player in _players)
            {
                player._session._streamWriter.WriteLine(msg);
            }
        }
        //-------------------------------------------------------
        private void PlayerDisconnectedMessageHandler(Player sender)                //on player disconnect
        {
            _players.Remove(sender);
        }
        public void ClientRecievedMessageHandler(object sender, string message)     //on reciving message form player
        {
            MessageBox.Show(message);       //to be removed

            //parse message
            /*
             switch(format)
                case 1: action1
                case 2: action2
                default: action3
             */
        }
        


    }
}
