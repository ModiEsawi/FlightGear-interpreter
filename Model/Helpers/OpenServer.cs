using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace proj12.Model.Helpers
{
    class OpenServer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<int, string> genericsTable;
        private Dictionary<string, double> varsTable;
        private int varsLen;
        public string IP { set; get; }
        public int Port { set; get; }
        public bool stop;


        [Obsolete]
        public OpenServer(Dictionary<int, string> genericsTable)
        {
            this.genericsTable = genericsTable;
            this.varsLen = genericsTable.Count;
            this.varsTable = new Dictionary<string, double>();
            this.stop = false;
            this.IP = "127.0.0.1";
            this.Port = int.Parse(ConfigurationSettings.AppSettings.Get("Host Port"));
            foreach (var i in genericsTable.Values)
                this.varsTable[i] = 0.0;
        }
        public void StartServer()
        {
            new Thread(delegate ()
            {
                TcpListener server = new TcpListener(IPAddress.Parse(IP), Port);
                server.Start();
                while (!stop)
                {
                    int timeOut = 5;
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream ns = client.GetStream();
                    while (client.Connected && !stop)
                    {
                        byte[] msg = new byte[2048];
                        int i;
                        var task = Task.Run(() => ns.Read(msg, 0, msg.Length));
                        if (task.Wait(TimeSpan.FromSeconds(timeOut)))
                            i = task.Result;
                        else
                            continue;
                        string asString = System.Text.Encoding.ASCII.GetString(msg, 0, i);
                        List<double> asList = Parser(asString);
                        if (asList == null)
                            continue;
                        UpdateValues(Parser(asString));
                        Thread.Sleep(100);
                    }
                    server.Stop();
                    break;

                }
            }).Start();
        }
        public List<double> Parser(string line)
        {
            List<string> stringsList = line.Split(',').ToList<string>();
            if (stringsList.Count != varsLen)
                return null;
            List<double> valuesList = new List<double>();
            foreach (string s in stringsList)
                valuesList.Add(double.Parse(s));
            return valuesList;

        }
        public void UpdateValues(List<double> values)
        {
            for (var i = 0; i < varsLen; i++)
                varsTable[genericsTable[i]] = values[i];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("varsTable"));
        }

        public double getValue(string varPath)
        {
            return this.varsTable[varPath];
        }
        public void Disconnect()
        {
            this.stop = true;
        }
        

    }
}
