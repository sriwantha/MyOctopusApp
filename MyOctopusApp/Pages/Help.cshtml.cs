using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyOctopusApp.Pages
{
    public class HelpModel : PageModel
    {
        IAmazonS3 _s3Client;
        IAmazonSQS _sqsClient;
        public HelpModel(IAmazonS3 s3,IAmazonSQS sqs)
        {
            this._s3Client = s3;
            this._sqsClient = sqs;
        }
        public string Message { get; set; }

        public void OnGet()
        {
            StringBuilder sb = new StringBuilder();
          ListBucketsResponse  res1=  _s3Client.ListBucketsAsync().Result;

           foreach(var b in res1.Buckets)
            {
                Console.WriteLine(b.BucketName);
                sb.AppendLine($"<b>{b.BucketName}</br>");
            }
            ListQueuesRequest lqr = new ListQueuesRequest();
            lqr.QueueNamePrefix = "sri";
            ListQueuesResponse res2 = _sqsClient.ListQueuesAsync(lqr).Result;
            foreach (var q in res2.QueueUrls)
            {
                sb.AppendLine($"<b>{q}</br>");
                Console.WriteLine(q);
                Message = sb.ToString();
            }
        }
    }
}
