//using Firebase.Storage;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace BankingSystem.API.Utilities
{
    public class FileStorageHelper
    {
        private readonly IConfiguration _configuration;

        public FileStorageHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
        {
            try
            {
                // Specify S3 bucket name and file key
                string bucketName = _configuration["AWS:BucketName"];
                string fileKey = "uploads/" + fileName; // Construct a valid file key

                var _awsCredental = new BasicAWSCredentials(_configuration["AWS:IAMAccessKey"], _configuration["AWS:IAMSecretKey"]);
                var _s3Client = new AmazonS3Client(_awsCredental, RegionEndpoint.USEast1);

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
