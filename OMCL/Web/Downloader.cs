using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Web
{
    public class Downloader
    {
        public static byte[] DownloadSingleFile(string url) => HttpDownloadFile(url);
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static byte[] HttpDownloadFile(string url)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            MemoryStream stream = new MemoryStream();
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            byte[] bytes = stream.GetBuffer();
            stream.Close();
            responseStream.Close();
            return bytes;
        }
    }
}
