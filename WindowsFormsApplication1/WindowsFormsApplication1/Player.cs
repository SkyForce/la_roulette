using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roulette
{
    class Player
    {
        Database database;
        bool isLogged = false, isDemo = false;
        int lastNum = 0, id;
        string last, login;
        WebSocketSession session;

        public Player(Database db, WebSocketSession s)
        {
            database = db;
            session = s;
        }

        public void Create(string log, string pass)
        {
            login = log;
            id = database.Create(log, pass);
            isLogged = true;

            int cur = database.Update(id, 0, isDemo);
            int newNum = RouletteWheel.TurnWheel();
            string salt = RouletteWheel.Salt();
            session.Send(String.Format("l_{0}_{1}_{2}", cur, RouletteWheel.md5(newNum + salt), ""));
            last = newNum + salt;
            lastNum = newNum;
        }

        public String GetLogin()
        {
            return login;
        }

        public bool IsLogged()
        {
            return isLogged;
        }

        public bool Auth(string log, string pass)
        {
            if ((id = database.Auth(log, pass)) > 0)
            {
                login = log;
                isLogged = true;

                int cur = database.Update(id, 0, isDemo);
                int newNum = RouletteWheel.TurnWheel();
                string salt = RouletteWheel.Salt();
                session.Send(String.Format("l_{0}_{1}_{2}", cur, RouletteWheel.md5(newNum + salt), ""));
                last = newNum + salt;
                lastNum = newNum;
            }
            else
            {
                isLogged = false;
            }
            return isLogged;
        }

        public void SwitchDemo()
        {
            if (!isLogged) return;
            isDemo = !isDemo;
            int cur = database.Update(id, 0, isDemo);
            session.Send(String.Format("d_{0}", cur));
        }

        public void Bet(int[] bet)
        {
            if (!isLogged) return;
            int receive = 0, lost = 0;
            int[] reds = {1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36};

            for (int i = 0; i <= 36; i++)
            {
                if (lastNum == i) receive = 36 * bet[i];
                else lost += bet[i];
            }
            for (int i = 37; i <= 39; i++)
            {
                if (lastNum == 0) lost += bet[i] / 2;
                else if (lastNum % 3 == ((i - 36) % 3)) receive += 2 * bet[i];
                else lost += bet[i];
            }
            for (int i = 40; i <= 42; i++)
            {
                if (lastNum == 0) lost += bet[i] / 2;
                else if (lastNum > (i - 40) * 12 && lastNum <= (i - 39) * 12) receive += 2 * bet[i];
                else lost += bet[i];
            }

            if (lastNum == 0) lost += bet[43] / 2;
            else if (lastNum >= 1 && lastNum <= 18) receive += 1 * bet[43];
            else lost += bet[43];

            if (lastNum == 0) lost += bet[48] / 2;
            else if (lastNum >= 19 && lastNum <= 36) receive += 1 * bet[48];
            else lost += bet[48];

            if (lastNum == 0) lost += bet[44] / 2;
            else if (lastNum > 0 && lastNum % 2 == 0) receive += 1 * bet[44];
            else lost += bet[44];

            if (lastNum == 0) lost += bet[47] / 2;
            else if (lastNum % 2 == 1) receive += 1 * bet[47];
            else lost += bet[47];

            if (lastNum == 0) lost += bet[45] / 2;
            else if (reds.Contains<int>(lastNum)) receive += 1 * bet[45];
            else lost += bet[45];

            if (lastNum == 0) lost += bet[46] / 2;
            else if (!reds.Contains<int>(lastNum)) receive += 1 * bet[46];
            else lost += bet[46];

            int cur = database.Update(id, receive - lost, isDemo);

            int newNum = RouletteWheel.TurnWheel();
            string salt = RouletteWheel.Salt();
            session.Send(String.Format("s_{0}_{1}_{2}", cur, RouletteWheel.md5(newNum + salt), last));
            last = newNum + salt;
            lastNum = newNum;
        }

        public void Sweep()
        {
            database = null;
            isLogged = false;
            isDemo = false;
            lastNum = 0;
            id = 0;
            last = login = "";
            session = null;
        }
    }
}
