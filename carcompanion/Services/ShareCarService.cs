using System;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Contract.V1.Responses.ShareCar;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;
using carcompanion.Services.Interfaces;

namespace carcompanion.Services
{
    public class ShareCarService : IShareCarService
    {
        private readonly IShareKeyRepository _shareKeyRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserCarRepository _userCarRepository;
        
        public ShareCarService(IShareKeyRepository shareKeyRepository, ICarRepository carRepository, IUserCarRepository userCarRepository) 
        {
            _shareKeyRepository = shareKeyRepository;   
            _carRepository = carRepository;                     
            _userCarRepository = userCarRepository;
        }

        public async Task<ServiceResult> CreateShareKeyAsync(Guid carId, Guid userId, string coOwnerRole)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)
                return FailedResult("Car doesn't exist", 404);

            if(!CheckPermissions(car, userId, coOwnerRole))
                return FailedResult("User can't do it with this car", 401);

            var shareKey = new ShareKey { IssuerId = userId, CarId = carId, UserCarRoleId = coOwnerRole, Used = false };

            if(!await _shareKeyRepository.CreateShareKeyAsync(shareKey))
                return FailedResult("Something went wrong", 500);
            
            var respone = new CreateShareKeyResponse { Success = true, ShareKey = shareKey.ShareKeyId };
            return SuccessResult(respone, 201);
        }

        public async Task<ServiceResult> UseShareKeyAsync(Guid userId, Guid shareKeyId)
        {
            var shareKey = await _shareKeyRepository.GetShareKeyByIdAsync(shareKeyId);

            if(shareKey == null)
                return FailedResult("Invalid sharekey", 400);
            
            if(shareKey.Used == true)
                 return FailedResult("This sharekey has been used", 400);

            if(await UserHasCar(userId, shareKey.CarId, shareKey.UserCarRoleId))
                return FailedResult("User already has permissions to this car", 400);

            var userCar = new UserCar{ UserCarRoleId = shareKey.UserCarRoleId, UserId = userId, CarId = shareKey.CarId};

            if(!await _userCarRepository.CreateUserCarAsync(userCar))
                return FailedResult("Something went wrong!", 500);

            if(!await MakeShareKeyUsed(shareKey))
                return FailedResult("Something went wrong!", 500);

            var response = new UseShareKeyResponse { Success = true, CarId = shareKey.CarId };
            return SuccessResult(response, 200);
        }

        private async Task<bool> MakeShareKeyUsed(ShareKey shareKey)
        {
            shareKey.Used = true;
            return await _shareKeyRepository.UpdateShareKeyAsync(shareKey);
        }

        private async Task<bool> UserHasCar(Guid userId, Guid carId, string userCarRoleId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);            
            var userCar = car.UserCars.FirstOrDefault(u => u.UserId == userId);          
            
            if(userCar == null)
                return false;

            if(!userCar.UserCarRoleId.Equals(userCarRoleId))
                return true;
            
            return false;
        }


        //TODO: get rid of this hard code
        private bool CheckPermissions(Car car, Guid userId, string roleToGrand)
        {
            var userCar = car.UserCars.FirstOrDefault(u => u.UserId == userId);

            if(userCar == null)
                return false;

            var role = userCar.UserCarRoleId;
                        
            if(role.Equals("editor") || role.Equals("owner"))
            {
                if(roleToGrand.Equals("editor") || roleToGrand.Equals("viewer"))
                    return true;
            }
            
            if(role.Equals("viewer"))
            {
                if(roleToGrand.Equals("viewer"))
                    return true;
            }

            return false;
        }

        private ServiceResult FailedResult(string errorMessage, int? statusCode)
        {
            return new ServiceResult { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode != null ? (int)statusCode : 400};
        }
        
        private ServiceResult SuccessResult(IResponseData response, int? statusCode)
        {
            return new ServiceResult { Success = true, ResponseData = response, StatusCode = statusCode != null ? (int)statusCode : 200};
        }
    }
}