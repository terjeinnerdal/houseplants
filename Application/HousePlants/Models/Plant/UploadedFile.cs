using System;

namespace HousePlants.Models.Plant
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public byte[] Data { get; set; }

        public UploadedFile(Guid id, byte[] data)
        {
            Id = id;
            Data = data;
        }
    }
}