using Newtonsoft.Json;
using NSwag.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
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

namespace BNBWebSockets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Proposal> bids = new ObservableCollection<Proposal>();
        ObservableCollection<Proposal> asks = new ObservableCollection<Proposal>();

        List<Proposal> b_buffer = new List<Proposal>();
        List<Proposal> a_buffer = new List<Proposal>();

        public MainWindow()
        {
            InitializeComponent();

            BidsList.Items.Clear();
            BidsList.ItemsSource = bids;

            AsksList.Items.Clear();
            AsksList.ItemsSource = asks;

            GetData();
        }

        async void GetData()
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://api.binance.com/api/v3/depth?symbol=BNBBTC&limit=1000");
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string response = sr.ReadToEnd();
            sr.Close();

            Response snapshot = JsonConvert.DeserializeObject<Response>(response);

            foreach (var bid in snapshot.bids)
            {
                var prop = new Proposal { Price = bid[0], Quantity = bid[1] };
                bids.Add(prop);
            }

            foreach (var ask in snapshot.asks)
            {
                var prop = new Proposal { Price = ask[0], Quantity = ask[1] };
                asks.Add(prop);
            }


            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri("wss://stream.binance.com:9443/ws/bnbbtc@depth"), CancellationToken.None);

            while (true)
            {
                b_buffer.Clear();
                a_buffer.Clear();

                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[4096]);
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                var st_resp = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);

                var st_response = JsonConvert.DeserializeObject<StreamResp>(st_resp);

                foreach(var bid in st_response.b)
                {
                    var prop = new Proposal { Price = bid[0], Quantity = bid[1] };
                    b_buffer.Add(prop);
                }

                foreach (var ask in st_response.a)
                {
                    var prop = new Proposal { Price = ask[0], Quantity = ask[1] };
                    a_buffer.Add(prop);
                }

                UpdateProps(bids, b_buffer, Sort);
                UpdateProps(asks, a_buffer, SortDesc);

                BidsList.Items.Refresh();
                AsksList.Items.Refresh();
            }
        }

        
        private void UpdateProps(ObservableCollection<Proposal> props, List<Proposal> p_buffer, Func<decimal, decimal, bool> MyMethod)
        {
            foreach (var prop in p_buffer)
            {
                try
                {
                    var snap_item = props.Single(x => x.Price == prop.Price);

                    if (prop.Quantity == 0)
                    {
                        props.Remove(snap_item);
                    
                    }
                    else
                    {
                        snap_item.Quantity = prop.Quantity;
                    }
                }
                catch (Exception ex)
                {
                    if (!(prop.Quantity == 0))
                        for (int i = 0; i < props.Count; i++)
                        {
                            if (MyMethod(props[i].Price, prop.Price))
                            {
                                props.Insert(i, new Proposal { Price = prop.Price, Quantity = prop.Quantity });
                                break;
                            }
                        }
                }
            }
        }

        private bool Sort(decimal a, decimal b)
        {
            if(a < b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SortDesc(decimal a, decimal b)
        {
            if (a > b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }

    class Proposal
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }

    class Response
    {
        public List<decimal[]> asks { get; set; }
        public List<decimal[]> bids { get; set; }
    }

    class StreamResp
    {
        public List<decimal[]> a { get; set; }
        public List<decimal[]> b { get; set; }
    }
}
