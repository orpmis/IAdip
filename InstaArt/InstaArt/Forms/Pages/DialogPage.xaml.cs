using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstaArt.DataBaseControlClasses;
using InstaArt.DbModel;

namespace InstaArt.Forms.Pages
{
    /// <summary>
    /// Логика взаимодействия для DialogPage.xaml
    /// </summary>
    public partial class DialogPage : Page
    {
        conversation currentConversation;
        List<messages> currentMessages;
        List<messages> selectedMessages = new List<messages>();
        string inputTextBuffer = string.Empty;
        public DialogPage(conversation selectedConversation)
        {
            InitializeComponent();

            currentConversation = selectedConversation;
            currentMessages = currentConversation.messages.ToList();

            ShowAllMessages(currentMessages);
        }

        public async Task<bool> RefreshMessages()
        {
            try
            {
                currentMessages = await MessagerManager.GetAllConversationMessages(currentConversation);
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public void ShowAllMessages(List<messages> showing)
        {
            Messages.Children.Clear();

            bool isFirst = true;

            for (int i = 0; i < showing.Count; i++)
            {
                ShowMessage(showing[i], isFirst);

                if (i == showing.Count - 1)
                {
                    break;
                }
                else
                {
                    if (showing[i + 1].id_sender == showing[i].id_sender)
                        isFirst = false;
                    else isFirst = true;
                }
            }
        }

        public void ShowMessage(messages message, bool isFirst)
        {
            Grid msg = GUI.ViewMessage(message, isFirst);

            msg.MouseDown += (s, e) =>
            {
                if (selectedMessages.Contains(message))
                {
                    selectedMessages.Remove(message);
                }
                else
                {
                    if (message.id_sender == SessionManager.currentUser.id)
                        selectedMessages.Add(message);
                }

                if (selectedMessages.Count == 0)
                {
                    HideFuncs();
                }
                else
                {
                    ShowFuncs();
                }

                if (selectedMessages.Count > 1)
                    RedactMessage.IsEnabled = false;
                else RedactMessage.IsEnabled = true;
            };

            Messages.Children.Add(msg);
        }

        private void SetSendMode_Click(object sender, RoutedEventArgs e)
        {
            SetSendMode.Visibility = Visibility.Collapsed;

            selectedMessages.Clear();
            HideFuncs();

            MessageUpload.MouseDown -= RedactedMessageUpload_Click;
            MessageUpload.MouseDown += MessageUpload_Click;
            MessageInputBox.TextInput += MessageInputBox_TextInput;

            MessageInputBox.Text = inputTextBuffer;
        }

        private async void MessageUpload_Click(object sender, RoutedEventArgs e)
        {
            
            bool isFirst =  (currentMessages.Count > 0) ? currentMessages[currentMessages.Count - 1].id_sender != SessionManager.currentUser.id : true;
            
            messages newMessage = new messages
            {
                id_conversation = currentConversation.id,
                id_sender = SessionManager.currentUser.id,
                message = MessageInputBox.Text,
                send_time = DateTime.Now
            };

            newMessage = await MessagerManager.UploadNewMessage(newMessage);;

            if (newMessage != null)
            {
                currentMessages.Add(newMessage);
                ShowMessage(newMessage, isFirst);

                MessageInputBox.Text = string.Empty;
            }
            else MessageBox.Show("Ошибка при отправке сообщения");

        }

        private async void DeleteMEssage_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите удалить выбранные сообщения?", "Удаленеи сообщения", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                
                await MessagerManager.DeleteMessage(selectedMessages);

                if (await RefreshMessages())
                {
                    SetSendMode_Click(null, null);

                    ShowAllMessages(currentMessages);
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так, перезапустите страницу");
                }
            }
        }

        private void RedactMessage_Click(object sender, RoutedEventArgs e)
        {
            SetSendMode.Visibility = Visibility.Visible;

            MessageUpload.MouseDown -= MessageUpload_Click; 
            MessageUpload.MouseDown += RedactedMessageUpload_Click;
            MessageInputBox.TextInput -= MessageInputBox_TextInput;

            MessageInputBox.Text = selectedMessages[0].message;
        }

        private async void RedactedMessageUpload_Click(object sender, RoutedEventArgs e)
        {
            await MessagerManager.RedactMessage(selectedMessages[0], MessageInputBox.Text);
            if (await RefreshMessages())
            {
                SetSendMode_Click(null, null);

                ShowAllMessages(currentMessages);
            }

            else
            {
                MessageBox.Show("Что-то пошло не так, перезапустите страницу");
            }
        }

        private void MessageInputBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            inputTextBuffer += e.Text;
        }

        public void HideFuncs()
        {
            MainGrid.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Pixel);
        }

        public void ShowFuncs()
        {
            MainGrid.RowDefinitions[0].Height = new GridLength(80, GridUnitType.Pixel);
        }
    }
}
