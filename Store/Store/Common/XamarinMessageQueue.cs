using Store.Interface.Domain;
using System;
using Xamarin.Forms;

namespace Store.Ui.Common
{
    internal class XamarinMessageQueue : IMessageQueue
    {
        private const string RequiredEmptyMessage = "";

        public void Send<TSender>(TSender sender, string message) where TSender : class
        {
            MessagingCenter.Send(sender, message);
        }

        public void Send<TSender, TArgs>(TSender sender, TArgs args) where TSender : class
        {
            MessagingCenter.Send(sender, RequiredEmptyMessage, args);
        }

        public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            MessagingCenter.Send(sender, message, args);
        }

        public void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = null) where TSender : class
        {
            MessagingCenter.Subscribe(subscriber, message, callback, source);
        }

        public void Subscribe<TSender, TArgs>(object subscriber, Action<TSender, TArgs> callback) where TSender : class
        {
            MessagingCenter.Subscribe(subscriber, RequiredEmptyMessage, callback);
        }

        public void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender source = null) where TSender : class
        {
            MessagingCenter.Subscribe(subscriber, message, callback, source);
        }

        public void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            MessagingCenter.Unsubscribe<TSender>(subscriber, message);
        }

        public void Unsubscribe<TSender, TArgs>(object subscriber) where TSender : class
        {
            MessagingCenter.Unsubscribe<TSender, TArgs>(subscriber, RequiredEmptyMessage);
        }

        public void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            MessagingCenter.Unsubscribe<TSender, TArgs>(subscriber, message);
        }
    }
}
