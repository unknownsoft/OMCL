using HtmlAgilityPack;
using OMCL.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace OMCL.Web.News
{
    public class MCBBSNewsEngine : NewsEngine
    {
        public static Logger logger = new Logger("MCBBS Engine");
        public override string Name => "我的世界中文论坛(MCBBS)";

        public override Uri Site => new Uri("https://www.mcbbs.net");
        public static string DownloadString(string url)
        {
            return Encoding.UTF8.GetString(DownloadData(url));
        }
        public static byte[] DownloadData(string url)
        {
            logger.info("Download Data " + url);
            byte[] html = Downloader.DownloadSingleFile(url);
            logger.info("Download Data " + url + " Completed");
            return html;
        }
        public override List<New> InsideGetNews()
        {
            int count = 0;
            var result = new List<New>();
            HtmlDocument document = new HtmlDocument();
            string html = DownloadString("https://www.mcbbs.net/forum-news-1.html");
            document.LoadHtml(html);
            HtmlNode maintable = document.DocumentNode.SelectSingleNode("//table[@id='threadlisttableid']");
            foreach (HtmlNode node in maintable.ChildNodes)
            {
                if (count > 10) break;
                try
                {
                    if (node.Attributes["id"] != null)
                    {
                        if (node.Attributes["id"].Value.StartsWith("normalthread_"))
                        {
                            string inner = node.InnerHtml;
                            HtmlDocument indoc = new HtmlDocument();
                            indoc.LoadHtml(inner);
                            MCBBSNew mnew = new MCBBSNew();
                            HtmlNode titlend = indoc.DocumentNode.SelectSingleNode("//a[@class='s xst']");
                            mnew._Title = HttpUtility.HtmlDecode(titlend.InnerText);
                            mnew.Uri = Path.Combine("https://www.mcbbs.net/", titlend.Attributes["href"].Value);
                            string htmlin = MCBBSNewsEngine.DownloadString(mnew.Uri);
                            HtmlDocument indocument = new HtmlDocument();
                            indocument.LoadHtml(htmlin);
                            HtmlNode mainpost = indocument.DocumentNode.SelectSingleNode("//td[@class='t_f']");
                            mainpost.ChildNodes.RemoveAt(0);
                            mainpost.ChildNodes.RemoveAt(0);
                            mainpost.ChildNodes.RemoveAt(0);
                            string txta = mainpost.InnerText;
                            string[] s = txta.Split('\n');
                            StringBuilder txt = new StringBuilder("");
                            foreach(var str in s)
                            {
                                if (str.Replace("\r", "").Trim() != "")
                                {
                                    txt.Append(str).Append("");
                                }
                            }
                            mnew._Description = HttpUtility.HtmlDecode(txt.ToString());

                            result.Add(mnew);
                            count++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.error("无法加载或解析Node:" + node.InnerHtml);
                    logger.error(ex);
                }
            }
            return result;
        }
        public static string[] listimgs(HtmlNode node)
        {
            List<string> result = new List<string>();
            foreach(var child in node.ChildNodes)
            {
                if (child.Name == "img")
                {
                    if (child.Attributes["width"] != null && child.Attributes["height"] != null)
                    {
                        int width = int.Parse(child.Attributes["width"].Value);
                        int height = int.Parse(child.Attributes["height"].Value);
                        if (width < 200 && height < 150)
                            continue;
                    }
                    if (child.Attributes["src"] != null)
                    {
                        result.Add(child.Attributes["src"].Value);
                    }
                    else if (child.Attributes["file"] != null)
                    {
                        result.Add(child.Attributes["file"].Value);
                    }
                }
                else
                {
                    result.AddRange(listimgs(child));
                }
            }
            return result.ToArray();
        }
    }
    public class MCBBSNew : New
    {
        public Logger logger = new Logger("MCBBS Engine");
        public override string Title => _Title;
        internal string _Title { get; set; }
        public override string Description => _Description;
        internal string _Description { get; set; }
        public string Uri { get; internal set; }
        public override Image Background
        {
            get
            {
                string htmlin = MCBBSNewsEngine.DownloadString(Uri);
                HtmlDocument indocument = new HtmlDocument();
                indocument.LoadHtml(htmlin);
                HtmlNode mainpost = indocument.DocumentNode.SelectSingleNode("//td[@class='t_f']");
                var line = MCBBSNewsEngine.listimgs(mainpost);
                foreach (var url in line)
                {
                    try
                    {
                        byte[] bytelist = MCBBSNewsEngine.DownloadData(url);
                        MemoryStream ms1 = new MemoryStream(bytelist);
                        Bitmap bm = (Bitmap)Image.FromStream(ms1);
                        ms1.Close();
                        return bm;
                    }
                    catch (Exception ex)
                    {
                        logger.error(ex);
                    }
                }
                return null;
            }
        }


        public override void OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(Uri);
        }
    }

}
