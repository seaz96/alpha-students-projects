using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using StudentProjects.DataLayer.Repositories;
using StudentProjects.Domain.Entities;
using StudentProjects.Models.Converters;
using StudentProjects.Models.Exceptions;

namespace StudentProjects.Application.Services;

public class FilesService(IAmazonS3 s3Client, FilesRepository repository, TeamsService teamsService)
{
    public async Task<string> GeneratePutPresignedUrl(Guid teamId, string name)
    {
        var request = new GetPreSignedUrlRequest
        {
            Key = $"{teamId}/{name}",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddHours(1),
            BucketName = "teams",
            Protocol = Protocol.HTTP
        };

        return await s3Client.GetPreSignedURLAsync(request);
    }

    public async Task<string> GenerateGetPresignedUrl(Guid teamId, string name)
    {
        var request = new GetPreSignedUrlRequest
        {
            Key = $"{teamId}/{name}",
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddHours(1),
            BucketName = "teams",
            Protocol = Protocol.HTTP
        };

        return await s3Client.GetPreSignedURLAsync(request);
    }

    public async Task<Models.Response.FileObject> CreateFileAsync(Guid teamId, string name)
    {
        var checkRequest = new GetObjectMetadataRequest()
        {
            Key = $"{teamId}/{name}",
            BucketName = "teams"
        };
        var metaInfo = await s3Client.GetObjectMetadataAsync(checkRequest);
        if (metaInfo is null)
            throw new FileObjectNotUploaded();

        var file = new FileObject
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            Name = name,
            Size = metaInfo.ContentLength
        };
        await repository.AddAsync(file);

        return file.ToClientModel();
    }

    public async Task<Models.Response.FileObject> DeleteFileAsync(Guid fileId)
    {
        var file = await repository.FindTrackedAsync(fileId);

        if (file is null)
            throw new Models.Exceptions.FileNotFoundException();
        
        var request = new DeleteObjectRequest
        {
            Key = $"{file.TeamId}/{file.Name}",
            BucketName = "teams",
        };
        await s3Client.DeleteObjectAsync(request);
        await repository.DeleteAsync(file);

        return file.ToClientModel();
    }

    public async Task<IEnumerable<Models.Response.FileObject>> GetFilesAsync(Guid teamId)
    {
        var files = await repository.GetFilesByTeamAsync(teamId);
        return files.Select(f => f.ToClientModel());
    }
}