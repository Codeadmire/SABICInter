using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using InterviewTask.Models;
using InterviewTask.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace InterviewTask.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    public class ChatRoomPageViewModel:BaseViewModel
	{
        private string _userName;
        private string _message;
        private ObservableCollection<MessageModel> _messagesList;
        private bool _isConnected;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public ListView myListView;


        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ObservableCollection<MessageModel> MessageList
        {
            get
            {
                return _messagesList;
            }
            set
            {
                _messagesList = value;
                OnPropertyChanged(nameof(MessageList));
            }
        }
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        private HubConnection hubConnection;

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        public ChatRoomPageViewModel()
        {
            MessageList = new ObservableCollection<MessageModel>();
            SendMessageCommand = new Command(async () => { await SendMessage(UserName, Message); });
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());

            IsConnected = false;

            hubConnection = new HubConnectionBuilder()
         .WithUrl($"http://127.0.0.1:5052/chathub")
         .Build();


            hubConnection.On<string>("JoinChat", (user) =>
            {
                MessageList.Add(new MessageModel() { UserName = UserName, Message = $"{user} has joined the chat", IsSystemMessage = true });
            });

            hubConnection.On<string>("LeaveChat", (user) =>
            {
                MessageList.Add(new MessageModel() { UserName = UserName, Message = $"{user} has left the chat", IsSystemMessage = true });
            });

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                MessageList.Add(new MessageModel() { UserName = UserName, Message = message, IsSystemMessage = true, IsOwnerMessage = false});
                Message = string.Empty;
            });

            hubConnection.On<string, QueryModel>("QueryResponse", (user, message) =>
            {
                MessageList.Add(new MessageModel() { UserName = UserName, QueryResponse = message, IsSystemMessage = false, IsOwnerMessage = false,IsSystemQuery =true });
                Message = string.Empty;
            });


        }
        

        async Task Connect()
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("JoinChat", UserName);

            IsConnected = true;
        }

        async Task SendMessage(string user, string message)
        {
            MessageList.Add(new MessageModel() { UserName = UserName, Message = message, IsSystemMessage = false, IsOwnerMessage = true });
            Message = string.Empty;
            await hubConnection.InvokeAsync("QueryMessage", user, message);
        }

        async Task Disconnect()
        {
            await hubConnection.InvokeAsync("LeaveChat", UserName);
            await hubConnection.StopAsync();

            IsConnected = false;
        }

        
    }
}
