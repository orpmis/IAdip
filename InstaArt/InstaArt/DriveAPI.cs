using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2.Flows;
using Newtonsoft.Json;
using System.Windows;
using Google.Apis.Requests;
using Google.Apis.Drive.v3.Data;

namespace InstaArt
{
     class DriveAPI
    {
        private static string[] Scopes = { DriveService.Scope.Drive};
        private static string AppName = "InstaArt";
        private static UserCredential credential;
        private static DriveService drive;

        public static string Upload(string filePath, string filename, string contentType, string parent)
        {
            try
            {
                Google.Apis.Drive.v3.Data.File newFile = new Google.Apis.Drive.v3.Data.File();
                newFile.Name = filename;

                newFile.Parents = new List<string> { GetFolderId(parent) };

                FilesResource.CreateMediaUpload UploadRequest;

                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))//Загружаем файл
                {
                    UploadRequest = drive.Files.Create(newFile, stream, contentType);
                    UploadRequest.Upload();
                }

                newFile = GetFileByName(filename); //Получаем только что загруженный файл

                Permission newFilePermission = new Permission { Type = "anyone", Role = "reader" };//Задаем права дсотупа
                drive.Permissions.Create(newFilePermission, newFile.Id).Execute();

                string newFileUri = "https://drive.google.com/uc?export=view&id=" + newFile.Id; //Создаем прямую ссылку на изображение

                return newFileUri;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка обращения в Google Drive " + ex.ToString());
                return null;
            }
        }

        public static bool Delete(string fileID)
        {
            try
            {
                FilesResource.DeleteRequest deleteRequest;

                deleteRequest = drive.Files.Delete(fileID);

                deleteRequest.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка обращения в Google Drive " + ex.ToString());
                return false;
            }
        }

        private static void IsRootFolderExist()
        {
            FilesResource.ListRequest request = drive.Files.List();
            request.Q = "name='InstaArtPhotos'";
            if (request.Execute().Files.Count == 0)
            {
                CreateRootFolder("InstaArtPhotos");
                CreateFolder("GroupsPhotoHeap", string.Empty);
            }
            else return;
        }
       
        public static void CreateRootFolder(string FolderName)
        {
            Google.Apis.Drive.v3.Data.File newFolder = new Google.Apis.Drive.v3.Data.File();
            newFolder.Name = FolderName;
            newFolder.MimeType = "application/vnd.google-apps.folder";

            FilesResource.CreateRequest creating = drive.Files.Create(newFolder);
            creating.Execute();
        }

        public static void CreateFolder(string FolderName, string parent)
        {
            Google.Apis.Drive.v3.Data.File newFolder = new Google.Apis.Drive.v3.Data.File();
            newFolder.Name = FolderName;
            newFolder.MimeType = "application/vnd.google-apps.folder";

            newFolder.Parents = new List<string> { GetFolderId(parent) };

            FilesResource.CreateRequest creating = drive.Files.Create(newFolder);
            creating.Execute();
        }

        public static void InitializeUsersDrive(int id)
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            credPath = Path.Combine(credPath, "InstaArtAuthSettings", "drive-credentials.json", "User" + id.ToString());
            
            if (!Directory.Exists(credPath))
            {
                MessageBox.Show("Похоже вы пытаетесь зайти с другого устройства, войдите на свой гугл аккаунт на этом устройстве и разрешите доступ для приложения");
                //return;
            }
            credential = RegistrateCredential(id);
            drive = GetService(credential);

            IsRootFolderExist();
        }
        public static UserCredential RegistrateCredential(int id) //При регистрации нового пользователя(или авторизации с нового устройства) запрашиваем у него права на управление диском и сохраняем в папку с путем, содержащим его идентификатор
        {
          
            using (FileStream stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                credPath = Path.Combine(credPath, "InstaArtAuthSettings", "drive-credentials.json", "User"+id.ToString());

                
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "User"+id.ToString(),
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                    ).Result;
            } 
        }

        private static DriveService GetService(UserCredential credential)
        {
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName
            }
            );
        }

        private static Google.Apis.Drive.v3.Data.File GetFileByName(string fileName)
        {
            FilesResource.ListRequest request = drive.Files.List();
            request.Q = "name='" + fileName + "'";
            FileList files = request.Execute();
            if (files.Files.Count != 0)
            {
                return files.Files[0];
            }
            else return null;
        }

        private static string GetFolderId(string FolderName)
        {
            if (FolderName == null || FolderName == string.Empty) FolderName = "InstaArtPhotos";

            if (GetFileByName(FolderName) == null)
            { 
                MessageBox.Show("File finding error: File does not exist");
                return null;
            }
            else return GetFileByName(FolderName).Id;
        }

        
    }
}
