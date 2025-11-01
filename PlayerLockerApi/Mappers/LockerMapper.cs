using PlayerLockerApi.Infrastructure.Documents;
using PlayerLockerApi.Models;

namespace PlayerLockerApi.Mappers;

public static class LockerMapper
{

    public static Locker ToResponse(this Locker locker)
    {
        return new Locker
        {
            Id = locker.Id,
            Name = locker.Name,
            Skin = locker.Skin,
            Backblings = locker.Backblings,
            Pickaxe = locker.Pickaxe,
            Glider = locker.Glider,
            Contrail = locker.Contrail,
            Emote = locker.Emote
        };
    }

    public static Locker ToModel(this CreateLockerRequest request)
    {
        return new Locker
        {
            Name = request.Name,
            Skin = request.Skin,
            Backblings = request.Backblings,
            Pickaxe = request.Pickaxe,
            Glider = request.Glider,
            Contrail = request.Contrail,
            Emote = request.Emote
        };
    }
    public static LockerDocument ToDocument(this Locker locker)
    {
        return new LockerDocument
        {
            Name = locker.Name,
            Skin = locker.Skin,
            Backblings = locker.Backblings,
            Pickaxe = locker.Pickaxe,
            Glider = locker.Glider,
            Contrail = locker.Contrail,
            Emote = locker.Emote
        };
    }
    public static Locker ToDomain(this LockerDocument document)
    {
        return new Locker
        {
            Id = document.Id,
            Name = document.Name,
            Skin = document.Skin,
            Backblings = document.Backblings,
            Pickaxe = document.Pickaxe,
            Glider = document.Glider,
            Contrail = document.Contrail,
            Emote = document.Emote
        };
    }

}
