using Grpc.Core;
using PlayerLockerApi.Repositories;
using PlayerLockerApi.Mappers;
using Google.Protobuf.WellKnownTypes;


namespace PlayerLockerApi.Services;

public class LockerService : PlayerLockerApi.LockerService.LockerServiceBase
{
    private readonly ILockerRepository _lockerRepository;

    public LockerService(ILockerRepository lockerRepository)
    {
        _lockerRepository = lockerRepository;
    }

    public override async Task<CreateLockerResponse> CreateLockers(IAsyncStreamReader<CreateLockerRequest> requestStream, ServerCallContext context)
    {
        var createdLockers = new List<Locker>();
       
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            var request = requestStream.Current;
            var locker = request.ToModel();
            var lockerexists = await _lockerRepository.GetByNameAsync(locker.Name, context.CancellationToken);
            if (lockerexists.Any())
            {
                continue; 
            }
            var createdLocker = await _lockerRepository.CreateAsync(locker, context.CancellationToken);
            createdLockers.Add(createdLocker.ToResponse());
        }

        return new CreateLockerResponse
        {   
            Lockers = { createdLockers }
        };
    }
}