using ParseOrderFileAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ParseOrderFileAPI.Controllers
{
    public class OrderController : ApiController
    {
        private List<Order> OrderList;

        public OrderController()
        {
            OrderList = new List<Order>();
            GetOrderDetailsFromFiles();
            GenerateXMLFromOrderList();
        }

        // GET api/order/5
        public List<Order> Get(string id)
        {
            //Fetch orders.xml from c drive.
            XDocument xdoc = XDocument.Load("C:\\orders.xml");
            List<Order> FetchedOrders = (from orderItem in xdoc.Descendants("Order")
                                  select new Order
                                  {
                                      OrderNumber = orderItem.Element("OrderNumber").Value,
                                      OrderLineNumber = orderItem.Element("OrderLineNumber").Value,
                                      ProductNumber = orderItem.Element("ProductNumber").Value,
                                      Quantity = Convert.ToInt32(orderItem.Element("Quantity").Value),
                                      Name = orderItem.Element("Name").Value,
                                      Description = orderItem.Element("Description").Value,
                                      Price = Convert.ToDouble(orderItem.Element("Price").Value),
                                      ProductGroup = orderItem.Element("ProductGroup").Value,
                                      OrderDate = orderItem.Element("OrderDate").Value,
                                      CustomerName = orderItem.Element("CustomerName").Value,
                                      CustomerNumber = orderItem.Element("CustomerNumber").Value
                                  }).ToList();
            return FetchedOrders.Where(x => x.OrderNumber == id).ToList();

        }

        private void GetOrderDetailsFromFiles()
        {
            string[] OrderFiles = { "Order1.txt", "Order2.txt", "Order3.txt" };
            for (int i=0; i<OrderFiles.Length; i++)
            {
                var path = System.Web.HttpContext.Current.Server.MapPath("~/Data/" + OrderFiles[i]);
                string[] lines = File.ReadAllLines(path);
                for (int j = 1; j < lines.Length; j++)
                {
                    var data = lines[j].ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                    Order newOrder = new Order();
                    newOrder.OrderNumber = data[1];
                    newOrder.OrderLineNumber = data[2];
                    newOrder.ProductNumber = data[3];
                    newOrder.Quantity = Convert.ToInt32(data[4]);
                    newOrder.Name = data[5];
                    newOrder.Description = data[6];
                    newOrder.Price = Convert.ToDouble(data[7]);
                    newOrder.ProductGroup = data[8];
                    newOrder.OrderDate = data[9];
                    newOrder.CustomerName = data[10];
                    newOrder.CustomerNumber = data[11];
                    OrderList.Add(newOrder);
                }
            }


        }

        private void GenerateXMLFromOrderList()
        {
            //Create orders.xml on c drive.
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Order>));
            TextWriter filestream = new StreamWriter(@"C:\orders.xml");
            serialiser.Serialize(filestream, OrderList);
            filestream.Close();
        }


    }
}
