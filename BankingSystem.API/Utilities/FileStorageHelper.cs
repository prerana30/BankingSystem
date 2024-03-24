//using Firebase.Storage;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.IO;

namespace BankingSystem.API.Utils
{
    public class FileStorageHelper
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AWSCredentials _awsCredental;

        public FileStorageHelper()
        {
            _awsCredental = new BasicAWSCredentials("AKIA6GBMGR4YXZTIBI62", "8kAGI7td9PCRNm/S9HWyQVKO7G6XGPMwHlCWX056");
            _s3Client = new AmazonS3Client(_awsCredental, RegionEndpoint.USEast1);
        }

        public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
        {
            try
            {
                // Specify S3 bucket name and file key
                string bucketName = "bankingsystemstorage";
                string fileKey = "uploads/" + fileName; // Construct a valid file key

                var bucketRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileKey,
                    InputStream = fileStream // Set the stream to be uploaded
                };

                PutObjectResponse response = await _s3Client.PutObjectAsync(bucketRequest);

                return $"https://{bucketName}.s3.amazonaws.com/{fileKey}"; ;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                return null;
            }
        }

    }
}
