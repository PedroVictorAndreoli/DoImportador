using DoImportador.Utils;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace DoImportador.Services
{
    public class BackupService
    {
        private Form1 _form;

        string _db;

        public BackupService(Form1 form, string db)
        {
            _form = form;
            _db = db;
        }

        public async void GetBackup()
        {
            try
            {
                _form.OnSetLog($"Criando backup base: {_db}");


                _form.OnSetLog($"Logando na plataforma...");
                var token = SecurityUtil.OnLoginToken("999");
                _form.OnSetLog($"Concluido login...");

                var client = new HttpClient();
                var url = "";

                client.BaseAddress = new Uri("https://api.dataon.com.br/v2/");
                url = "api/dataOn/doUtil/PegarBackup?doID=" + _db.Trim();


                _form.OnSetLog($"Buscando Backup...");

                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = TimeSpan.FromSeconds(200);
                var authUserData = String.Format("{0}:{1}", "dataon", "DataOnAPI@#");
                var authHeaderVal = Convert.ToBase64String(Encoding.UTF8.GetBytes(authUserData));
                client.Timeout = TimeSpan.FromMilliseconds(480000);
                client.DefaultRequestHeaders.Add("DoToken", token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderVal);
                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    _form.OnSetLog($"Backup concluido...");

                    string result1 = await result.Content.ReadAsStringAsync();

                    if (System.IO.File.Exists("D:\\aa" + _db.Trim() + ".zip"))
                    {
                        System.IO.File.Delete("D:\\aa" + _db.Trim() + ".zip");
                    }

                    _form.OnSetLog($"Fazendo download do backup...");

                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);
                        wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);

                        wc.DownloadFileAsync(
                            new System.Uri("https://api.dataon.com.br/v2/static/temp/zip" + _db.Trim() + ".zip"),
                             "D:\\aa\\" + _db.Trim() + ".zip"
                        );
                    }

                }


            } catch(Exception ex) {
                _form.OnSetLog($"Ocorreu um erro ao restaurar o backup: {ex.Message}");
            }
        }


        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _form.OnSetLog($"Processo: {e.ProgressPercentage}");

        }


        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _form.OnSetLog($"Falha: {e.Error.Message}");
            }
            else
            {
                if (System.IO.File.Exists("D:\\aa\\" + _db.Trim() + ".bak"))
                {
                    System.IO.File.Delete("D:\\aa\\" + _db.Trim() + ".bak");
                }


                _form.OnSetLog($"Extraindo arquivo em D:/aa aguarde...");

                var zipFileName = @"D:\aa\" + _db.Trim() + ".zip";
                var targetDir = @"D:\aa";
                FastZip fastZip = new FastZip();
                string fileFilter = null;
                fastZip.Password = "DO1020" + _db.Trim();

                // Will always overwrite if target filenames already exist
                fastZip.ExtractZip(zipFileName, targetDir, fileFilter);



                _form.OnSetLog($"Extração concluida!!!");

                _form.OnSetLog($"Restaurando Backup...");

                string query = "";

                string connectionString = "Server=.\\MSSQLSERVER2022;User ID=atmusinf;Password=Atmus@#4080;database=master";

                query = "ALTER DATABASE [atmusinf_control-" + _db.Trim() + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE" +
                    " RESTORE DATABASE [atmusinf_control-" + _db.Trim() + "] FROM  DISK = N'D:\\aa\\" + _db.Trim() + ".bak' WITH  FILE = 1,  MOVE N'sisflexControl' TO N'D:\\SQLServer\\MSSQL16.MSSQLSERVER2022\\MSSQL\\DATA\\atmusinf_control-" + _db.Trim() + ".mdf',  MOVE N'sisflexControl_log' TO N'D:\\SQLServer\\MSSQL16.MSSQLSERVER2022\\MSSQL\\DATA\\atmusinf_control-" + _db.Trim() + "_log.ldf',  NOUNLOAD,  REPLACE,  STATS = 5" +
                    " ALTER DATABASE [atmusinf_control-" + _db.Trim() + "] SET MULTI_USER";

                var conn = new SqlConnection(connectionString);
                //SqlTransaction transaction;
                var command = new SqlCommand(query, conn);

                conn.Open();
                try
                {
                    command.ExecuteNonQuery();
                    if ((conn.State == ConnectionState.Open))
                    {
                        conn.Close();
                    }

                    connectionString = "Server=.\\MSSQLSERVER2022;User ID=atmusinf;Password=Atmus@#4080;database=atmusinf_control-" + _db.Trim();
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                    command = new SqlCommand("update pessoas_usuarios set Login = '2', Senha='2' where idpessoa = 0", conn);

                    command.ExecuteNonQuery();


                    _form.OnSetLog($"backup restaurado com sucesso.");
                }
                catch (Exception ex)
                {
                    _form.OnSetLog($"Falha ao restaurar backup: {ex.Message}");
                }
                finally
                {
                    if ((conn.State == ConnectionState.Open))
                    {
                        conn.Close();
                    }
                }

            }

        }

    }
}
