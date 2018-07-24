using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using CheckContragent.Infrastructure.FNSNDSCAWS2;
using CheckContragent.Infrastructure.Consts;

namespace CheckContragent
{
    class Program
    {
        static string _INN;
        static string _KPP;
        static void Main(string[] args)
        {
            List<NdsRequest2NP> _listContragents = new List<NdsRequest2NP>();

            string connectionString = @"Data Source=storage;Initial Catalog=master;User Id=" + ConfigurationManager.AppSettings["db_login"] + ";Password=" + ConfigurationManager.AppSettings["db_pass"];
            string sqlExpression = @"SELECT DISTINCT
                                           [INN]
                                          ,[KPP]
                                    FROM [VKAbon].[dbo].[Abon_Details]
                                    WHERE
                                    DEnd >= '2079-01-01'
                                    AND Plat IS NOT NULL
                                    AND LEN(INN) != 12";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader.HasRows)
                    {
                        _INN = reader["INN"].ToString();
                        _KPP = reader["KPP"].ToString();
                        _listContragents.Add(
                            new NdsRequest2NP
                            {
                                INN = _INN,
                                KPP = _KPP,
                                DT = DateTime.Now.ToString()
                            });
                    }
                }
            }

            var srvFNSDSCAWS2 = new FNSNDSCAWS2_PortClient();

            int part = Consts.part;
            int chunk = 0;
            List<List<NdsRequest2NP>> itemParts = new List<List<NdsRequest2NP>>();
            for (int i = 0; i < (int)_listContragents.Count / part; i++)
            {
                itemParts.Add(_listContragents.GetRange(chunk, part));
                chunk = chunk + part;
            }
            itemParts.Add(_listContragents.GetRange(chunk, _listContragents.Count - ((int)_listContragents.Count / part) * part));

            foreach (var itemPart in itemParts)
            {
                var reqNdsRequest2 = new NdsRequest2Request
                {
                    NdsRequest2 = itemPart.ToArray()
                };

                var resFNSDSCAWS = srvFNSDSCAWS2.NdsRequest2(reqNdsRequest2);

                if (resFNSDSCAWS.NdsResponse2.errMsg == null)
                {
                    foreach (var itemFNSDSCAWS in resFNSDSCAWS.NdsResponse2.NP)
                    {
                        string sw = itemFNSDSCAWS.State.ToString();
                        string res;
                        switch (sw)
                        {
                            case "Item0":
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item0);
                                res = Consts.Item0;
                                break;
                            case "Item1":
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item1);
                                res = Consts.Item1;
                                break;
                            case "Item2":
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item2);
                                res = Consts.Item2;
                                break;
                            case "Item3":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item3);
                                res = Consts.Item3;
                                Console.ResetColor();
                                break;
                            case "Item4":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item4);
                                res = Consts.Item4;
                                Console.ResetColor();
                                break;
                            case "Item5":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item5);
                                res = Consts.Item5;
                                Console.ResetColor();
                                break;
                            case "Item6":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item6);
                                res = Consts.Item6;
                                Console.ResetColor();
                                break;
                            case "Item7":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item7);
                                res = Consts.Item7;
                                Console.ResetColor();
                                break;
                            case "Item8":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item8);
                                res = Consts.Item8;
                                Console.ResetColor();
                                break;
                            case "Item9":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item9);
                                res = Consts.Item9;
                                Console.ResetColor();
                                break;
                            case "Item10":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item10);
                                res = Consts.Item10;
                                Console.ResetColor();
                                break;
                            case "Item11":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item11);
                                res = Consts.Item11;
                                Console.ResetColor();
                                break;
                            case "Item12":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} - {1}", itemFNSDSCAWS.INN, Consts.Item12);
                                res = Consts.Item12;
                                Console.ResetColor();
                                break;
                            default:
                                res = "status is empty";
                                break;
                        }

                        File.AppendAllText(@"LogCheckContragent.csv", DateTime.Now + ";" + itemFNSDSCAWS.INN + ";" + res + ";" + Environment.NewLine, Encoding.Default);
                    }
                }
                else
                {
                    Console.WriteLine("Error: {0}", resFNSDSCAWS.NdsResponse2.errMsg);
                }
            }

            Console.WriteLine("Check contragent complete");
            Console.Read();
        }
    }
}
