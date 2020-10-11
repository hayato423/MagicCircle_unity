using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Socket
{
    public class UDP
    {
        private UdpClient udpForSend;   //送信用クライアント
        private string remoteHost = "127.0.0.1"; //送信用IPアドレス
        private int remotePort;  //送信先のポート

        private UdpClient udpForReceive;    //受信用クライアント
        public string rcvMsg = "ini";   //受信メッセージ格納
        private System.Threading.Thread rcvThread;  //受信用スレッド

        public UDP()
        {

        }

        public bool init(int port_snd, int port_to, int port_rcv)  //UDP設定(送受信用ポートを開きつつ受信用スレッドを生成)
        {
            try
            {
                udpForSend = new UdpClient(port_snd);  //送信用ポート
                remotePort = port_to;
                udpForReceive = new UdpClient(port_rcv);  //受信用ポート
                rcvThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void send(string sendMsg)  //文字列を送受信用ポートから送信先ポートに送信
        {
            try
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendMsg);
                udpForSend.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
            }
            catch { }
        }

        public void receive()  //受信スレッドで実行される関数
        {
            IPEndPoint remoteEP = null;  //任意の送信元からのデータを受信;
            while (true)
            {
                try
                {
                    byte[] rcvBytes = udpForReceive.Receive(ref remoteEP);
                    Interlocked.Exchange(ref rcvMsg, Encoding.ASCII.GetString(rcvBytes));
                }
                catch { }
            }
        }

        public void start_receive()
        {
            try
            {
                rcvThread.Start();
            }
            catch { }
        }

        public void stop_receive()
        {
            try
            {
                rcvThread.Interrupt();
            }
            catch { }
        }

        public void end()
        {
            try
            {
                udpForReceive.Close();
                udpForSend.Close();
                rcvThread.Abort();
            }
            catch { }
        }


    }
}