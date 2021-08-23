using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
#nullable enable
namespace HousePlants.Models.Interfaces
{
    public interface IIntEntity
    {
         int Id { get; }
    }
    public interface IGuidEntity
    {
        Guid Id { get; }
    }
    public interface ITitledEntity
    {
        string Title { get; }
    }
    public interface IDescibedEntity
    {
        string Description { get; }
    }
    public interface INodaTimeEntity
    {
        Instant Created { get; set; }
        Instant Modified { get; set; }
    }
    public interface ISecretEntity
    {
        void Encrypt();
        public void Decrypt();
    }
    public interface IImageEntity
    {
        byte[] Image { get; set; }
    }
    public interface IGdprEntity
    {
        bool Anonymize();
        bool Delete();
        string GetRegisteredGdprData();
    }

    public interface IUserOwnedEntity<T>
    {
        T Owner { get; set; }
    }

    public abstract class HousePlantsEntityBase : IGuidEntity, ITitledEntity, IDescibedEntity, INodaTimeEntity, IUserOwnedEntity<Guid>
    {
        [Key, HiddenInput]
        public Guid Id { get; set; }


        [StringLength(128)] 
        public virtual string Title { get; } = default!;

        [StringLength(20000), DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [HiddenInput, ScaffoldColumn(false)]
        public Instant Created { get; set; }
        [HiddenInput, ScaffoldColumn(false)]
        public Instant Modified { get; set; }

        public Guid Owner { get; set; }
    }
}