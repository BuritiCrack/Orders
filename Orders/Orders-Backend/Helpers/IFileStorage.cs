﻿namespace Orders_Backend.Helpers
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(byte[] content, string extention, string containerName);

        Task RemoveFileAsync(string path, string containerName);

        public async Task<string> EditFileAsync(byte[] content, string extention, string containerName, string path)
        {
            if (path is not null)
            {
                await RemoveFileAsync(path, containerName);
            }

            return await SaveFileAsync(content, extention, containerName);
        }
    }
}
