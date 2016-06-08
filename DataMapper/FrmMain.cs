using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Common;
using Data;
using System.Threading.Tasks;
using Common.Forms;
using System.Diagnostics;

namespace DataMapper
{
    public partial class FrmMain : Form
    {
        SQLiteConnection cnn;
        public FrmMain()
        {
            InitializeComponent();
        }
        protected async override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblStatus.Text = "Initialitzing App";
            await InitializeApp();
            lblStatus.Text = "App Initialized";
        }

        private async Task<bool> InitializeApp()
        {
            bool initializeDb = false;

            if (!File.Exists("db.sqlite"))
            {
                await Task.Run(() =>
                {
                    lblStatus.Text = "Initialitzing DB...";
                    SQLiteConnection.CreateFile("db.sqlite");
                });
                initializeDb = true;
            }

            if (initializeDb)
            {
                lblStatus.Text = "Creating Tables...";
                await Task.Run(() => CreateTables());
                lblStatus.Text = "Feeding Tables...";
                await Task.Run(() => FeedDb());
                lblStatus.Text = "Completed!";
            }
            return true;
        }

        private void CreateTables()
        {
            using (var cnn = Globals.CreateConnection())
            {
                cnn.Open();

                string sql = "create table person(id int, name varchar(255),surname varchar(255),age int)";
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        private void TruncateTables()
        {
            using (var cnn = Globals.CreateConnection())
            {
                cnn.Open();

                string sql = "delete from person where 1=1";
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        private void FeedDb()
        {
            using (var cnn = Globals.CreateConnection())
            {
                cnn.Open();

                for (var i = 0; i < 1000; i++)
                {
                    var age = DateTime.Now.Millisecond;
                    var p = new Biz.Person();
                    p.Id = i;
                    p.Name = "Name-" + Common.StringExtensions.RandomString(255 - "Name-".Length);
                    p.Surname = "Surname-" + Common.StringExtensions.RandomString(255 - "Surname-".Length);
                    p.Age = age;
                    cnn.Insert(p);
                    this.SafeInvoke(() => lblInfo.Text = "Inserted Persons: " + i);
                }
            }
        }

        private async void cmdFeedDb_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Truncating Tables...";
            await Task.Run(() => TruncateTables());
            lblStatus.Text = "Feeding Tables...";
            await Task.Run(() => FeedDb());
            lblStatus.Text = "Completed!";
        }

        private async void cmdLoadData_Click(object sender, EventArgs e)
        {
            using (var cnn = Globals.CreateConnection())
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var data = await Task.Run(() =>
                {
                    var items = cnn.Query<Biz.Person>();
                    return new Data.BizObjectBindingList<Biz.Person>(items);
                });
                stopwatch.Stop();
                System.Diagnostics.Debug.WriteLine("Loading Elapsed: " + stopwatch.ElapsedMilliseconds);
                personBindingSource.SuspendBinding();
                personBindingSource.DataSource = data;
                personBindingSource.ResumeBinding();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            using (var cnn = Globals.CreateConnection())
            {
                var data = personBindingSource.DataSource as Data.BizObjectBindingList<Biz.Person>;
                if (data != null) {
                    var result=cnn.SaveBindingList(data);
                    if (result > 0)
                    {
                        lblStatus.Text = "Data Saved!";
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Truncating Data!";
            await Task.Run(() => TruncateTables());
            lblStatus.Text = "Data truncated!";
        }
    }
}
