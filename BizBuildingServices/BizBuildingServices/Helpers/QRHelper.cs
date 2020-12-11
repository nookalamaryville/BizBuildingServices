using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace BizBuildingServices.Helpers
{
    public class QRHelper
    {
        public static string BuildQRCode(string imgName, string url, string alt = "QR code", int height = 500, int width = 500, int margin = 0)
        {
            try
            {
                var folderName = "Uploads/TempImages";
                string QRCodeFolderPath = HttpContext.Current.Server.MapPath("~/" + folderName);
                QRCodeFolderPath = QRCodeFolderPath + "\\" + imgName + ".png";
                string qrURL = ConfigurationManager.AppSettings["QRCodeURL"] + "?PropId=" + imgName;
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrURL, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                qrCodeImage.Save(QRCodeFolderPath);
                return QRCodeFolderPath;
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public static void UploadFileToS3(string file)
        {
            string awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            string bucketname = ConfigurationManager.AppSettings["QRCodeBucket"];
            try
            {
                string filePath = BuildQRCode(file, "", "QR Store", 150, 150);
                if (filePath != "")
                {
                    AmazonS3Config S3Config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.USEast1,
                    };
                    IAmazonS3 client;
                    using (client = new AmazonS3Client(awsAccessKey, awsSecretKey, S3Config))
                    {
                        var request = new PutObjectRequest()
                        {
                            BucketName = bucketname,
                            CannedACL = S3CannedACL.PublicRead,
                            Key = file + ".png",
                            FilePath = filePath
                        };
                        var resp = client.PutObject(request);
                    }
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    // throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }
    }
}