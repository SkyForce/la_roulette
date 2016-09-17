using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace roulette
{
	using System;
	using System.Windows.Forms;
	using SuperWebSocket;
    using roulette;
    using System.Threading;

	public partial class Form1 : Form
	{
		private WebSocketServer WebSocketServer;
		private List<WebSocketSession> WebSocketSessions;
        Dictionary<WebSocketSession, Player> players;
        private Database database;
        System.Timers.Timer timer;

		public Form1()
		{
			InitializeComponent();
			richtextbox_append("Welcome");
			this.WebSocketServer = new WebSocketServer();
			this.WebSocketServer.Setup(2012);
			this.WebSocketServer.NewDataReceived += WebSocketOnData;
			this.WebSocketServer.NewMessageReceived += WebSocketOnMessage;
			this.WebSocketServer.NewSessionConnected += WebSocketConnectionNew;
			this.WebSocketServer.SessionClosed += WebSocketConnectionClose;

			this.WebSocketSessions = new List<WebSocketSession>();
            players = new Dictionary<WebSocketSession, Player>();

			this.WebSocketServer.Start();
			this.richtextbox_append(String.Format("Server has started!"));
			buttonStop.Enabled = true;
			buttonRun.Enabled = false;
            buttonRestore.Enabled = false;
            timer = new System.Timers.Timer();
            timer.Elapsed += timer_Elapsed;

            database = new Database(@"D:\univer\sqlite-shell-win32-x86-3080900\roulette.db");

		}

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            database.SaveBase(String.Format("roulette-{0}-{1}-{2}-{3}-{4}-{5}.db", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
        }

		private void Form1_Load(object sender, EventArgs e)
		{ }

		delegate void SetTextCallback(string text);
		public void richtextbox_append(string msg)
		{
			if (this.richTextBox.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(richtextbox_append);
				this.Invoke(d, new object[] { msg });
			}
			else
			{
				richTextBox.AppendText("[");
				richTextBox.AppendText(System.DateTime.Now.ToShortTimeString());
				richTextBox.AppendText("] ");
				richTextBox.AppendText(msg);
				richTextBox.AppendText("\n");
				richTextBox.ScrollToCaret();
			}
		}

		delegate void SetListCallback();
		private void ListBoxRefresh()
		{
			if (this.richTextBox.InvokeRequired)
			{
				SetListCallback d = new SetListCallback(ListBoxRefresh);
				this.Invoke(d, new object[] { });
			}
			else
			{
				this.listBox1.Items.Clear();
				foreach (var session in WebSocketSessions)
					this.listBox1.Items.Add(session.SessionID);
			}
		}

		private void buttonRun_Click(object sender, EventArgs e)
		{
			this.WebSocketServer.Start();
			this.richtextbox_append("Server has started!");
			buttonStop.Enabled = true;
			buttonRun.Enabled = false;
            buttonRestore.Enabled = false;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			this.WebSocketServer.Stop();
			richtextbox_append("Server has stopped!");
			buttonStop.Enabled = false;
			buttonRun.Enabled = true;
            buttonRestore.Enabled = true;
		}

		private void buttonBackup_Click(object sender, EventArgs e)
		{
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "db";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                database.SaveBase(dlg.FileName);
                richTextBox.Text += dlg.FileName + " saved\n";
            }
        }

		private void buttonRestore_Click(object sender, EventArgs e)
		{
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "db";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                database = new Database(dlg.FileName);
                richTextBox.Text += dlg.FileName + " restored\n";
            }
        }

		#region Socket Events
		private void WebSocketOnData(WebSocketSession session, byte[] value)
		{ }

		private void WebSocketOnMessage(WebSocketSession session, string value)
		{
            Player player = players[session];
            try
            {
                if (value.StartsWith("n"))
                {
                    player.Create(value.Substring(2, value.IndexOf('_', 2) - 2), value.Substring(value.LastIndexOf('_') + 1));

                }
                else if (value.StartsWith("o"))
                {
                    foreach (KeyValuePair<WebSocketSession, Player> p in players)
                        if ((value.Substring(2, value.IndexOf('_', 2) - 2) == p.Value.GetLogin()) && p.Value.IsLogged())
                        {
                            session.Send("e_user is already online");
                            return;
                        }
                    player.Auth(value.Substring(2, value.IndexOf('_', 2) - 2), value.Substring(value.LastIndexOf('_') + 1));

                }
                else if (value == "d")
                {
                    player.SwitchDemo();
                }
                else if (value.StartsWith("b"))
                {
                    string[] s = value.Substring(2).Split('_');
                    int[] bet = new int[49];

                    for (int i = 0; i < 49; i++) bet[i] = int.Parse(s[i]);
                    player.Bet(bet);
                }
                else
                {
                    session.Send("e_incorrect message receive");
                }
            }
            catch(Exception e)
            {
                session.Send("e_" + e.Message);
                richtextbox_append(e.Message);
                Console.Beep();
            }
            
			this.richtextbox_append(string.Format("{0} said: {1}", session.SessionID, value));
			//session.Send("hello");
		}

		private void WebSocketConnectionNew(WebSocketSession session)
		{

			if (!this.WebSocketSessions.Contains(session))
				this.WebSocketSessions.Add(session);

			this.richtextbox_append(string.Format("{0} has connected!", session.SessionID));
			//session.Send("hello");

			this.ListBoxRefresh();

            players.Add(session, new Player(database, session));
		}

		private void WebSocketConnectionClose(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
		{
			if (this.WebSocketSessions.Contains(session))
				this.WebSocketSessions.Remove(session);

			this.richtextbox_append(string.Format("{0} has disconnected!", session.SessionID));

			this.ListBoxRefresh();

            if (players.ContainsKey(session))
            {
                players[session].Sweep();
                players.Remove(session);
            }

		}
		#endregion


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            switch (trackBar1.Value)
            {
                case 0:
                    timer.Stop();
                    break;
                case 1:
                    timer.Interval = 86400000;
                    timer.Start();
                    break;
                case 2:
                    timer.Interval = 3600000;
                    timer.Start();
                    break;
                case 3:
                    timer.Interval = 600000;
                    timer.Start();
                    break;
                case 4:
                    timer.Interval = 60000;
                    timer.Start();
                    break;
            }
        }

	}
}