using Minio;
using Minio.DataModel.Args;
using StudentProjects.Domain.Entities;

namespace StudentProjects.DataLayer.Repositories;

public class S3Repository(IMinioClient client)
{
    public async Task CreateBucketAsync(string bucketName)
    {
        var request = new MakeBucketArgs().WithBucket(bucketName);
        await client.MakeBucketAsync(request);
    }

    public async Task DeleteBucketAsync(string bucketName)
    {
        var request = new RemoveBucketArgs().WithBucket(bucketName);
        await client.RemoveBucketAsync(request);
    }

    public async Task UploadFileAsync(string bucketName, string fileName, Stream content)
    {

        var request = new PutObjectArgs().WithBucket(bucketName).WithFileName(fileName).WithStreamData(content);
        await client.PutObjectAsync(request);
    }

    public async Task DeleteFileAsync(string bucketName, string fileName)
    {
        var request = new RemoveObjectArgs().WithBucket(bucketNaмme).WithObject(fileName);
        await client.RemoveObjectAsync(request);
    }

    public async Task<FileObject> GetFileAsync(string bucketName, string key)
    {
        var request = new GetObjectArgs().WithBucket(bucketName).WithObject(key);
        var response = await client.GetObjectAsync(request);
        return new FileObject
        {
            IsDirectory = false,
            Key = response.ObjectName,
            Size = (ulong)response.Size // какой .... из minio придумал делать ulong в одной модели, и long в другой
        };
    }

    public async Task<List<FileObject>> GetObjectsListAsync(string bucketName, string prefix = "")
    {
        var request = new ListObjectsArgs().WithBucket(bucketName).WithPrefix(prefix);
        var items = new List<FileObject>();
        await foreach(var item in client.ListObjectsEnumAsync(request))
        {
            items.Add(new FileObject
            {
                IsDirectory = item.IsDir,
                Name = item.Key,
                Size = item.Size,
                Key = prefix + item.Key
            });
        }
        return items;
    }
}