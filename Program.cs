using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeDevPnP.Core;
using SP = Microsoft.SharePoint.Client;
using Microsoft.Online.SharePoint.TenantAdministration;
using System.Configuration;
using System.Net;
using System.Security;

namespace SharePointOnlineStuffs
{
    class Program
    {


        static void Main(string[] args)
        {
            string userName = ConfigurationManager.AppSettings["userName"];

            string siteUrl = ConfigurationManager.AppSettings["siteUrl"];
            string tenantAdminUrl = ConfigurationManager.AppSettings["adminUrl"];
            SecureString pass = GetSecurePassword();
            using (SP.ClientContext currentContext = new SP.ClientContext(siteUrl))
            {
                currentContext.Credentials = new SP.SharePointOnlineCredentials(userName, pass);
                SP.List list = currentContext.Web.Lists.GetByTitle("Artifacts");
                SP.ListItemCollection items = list.GetItems(CreateItemQuery());
                currentContext.Load(items);
                currentContext.ExecuteQuery();
                foreach (SP.ListItem item in items)
                {

                    currentContext.Load(item.ContentType);
                    currentContext.ExecuteQuery();
                    Console.WriteLine("Content Type is:" + item.ContentType.Name + " and Item Title is :" + item["Title"] + " and Item ID is :" + item["ID"]);
                }
                Console.ReadLine();
                //SP.CamlQuery query = new SP.CamlQuery();
                //SP.Folder folder = list.RootFolder;
                //SP.ListItem item=folder.

                #region Create Item
                //SP.ListItemCreationInformation info = new SP.ListItemCreationInformation();
                //for (int i = 4501; i < 5200; i++)
                //{
                //    SP.ListItem item = list.AddItem(info);
                //    item["Title"] = "A"+i;
                //    item["MakeOver"] = "Makeover"+i;
                //    item.Update();
                //}

                //currentContext.ExecuteQuery();
                //Console.WriteLine("Item Created");
                //Console.ReadLine();
                #endregion
                #region Code to get values


                //SP.ListItemCollectionPosition itempos = null;
                //while (true)
                //{
                //    SP.CamlQuery query = new SP.CamlQuery();
                //    query.ListItemCollectionPosition = itempos;
                //    query.ViewXml = @"<View><ViewFields><FieldRef Name='Title'/></ViewFields><RowLimit>1</RowLimit></View>";
                //    SP.ListItemCollection listItems = list.GetItems(query);
                //    currentContext.Load(listItems);
                //    currentContext.ExecuteQuery();
                //    itempos = listItems.ListItemCollectionPosition;
                //    foreach (SP.ListItem listItem in listItems)
                //        Console.WriteLine("Item Title:" + listItem["Title"]);
                //    if (itempos == null)
                //        break;
                //    Console.WriteLine(itempos.PagingInfo);
                //    Console.WriteLine();
                //}
                ////currentContext.Load(list);
                ////currentContext.ExecuteQuery();
                ////Console.WriteLine("List Title is:" + list.Title + " and Other Property is " + list.DefaultViewUrl);
                //Console.ReadLine();
            }
            #endregion

        }

        private static SP.CamlQuery CreateItemQuery()
        {

            return new SP.CamlQuery()
            {
                ViewXml = "<View ><Query><Where><Eq><FieldRef Name=\"FSObjType\" /><Value Type=\"Integer\">0</Value></Eq></Where></Query>\r\n</View>"
            };

        }

        private static SecureString GetSecurePassword()
        {
            string password = ConfigurationManager.AppSettings["userPassword"];
            var securePass = new SecureString();
            foreach (char c in password)
            {
                securePass.AppendChar(c);
            }
            return securePass;
        }
    }
}
