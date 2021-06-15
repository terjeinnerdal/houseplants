using System;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Infrastructure.ViewModels
{
    public class DocumentViewmodel
    {      
        public int Id { get; set; }
        [MaxLength(250)]
        public string FileName { get; set; }
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength]
        public byte[] FileData { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}