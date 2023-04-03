using Identiy_API.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Identiy_API.Services.Caching
{
    public class RedisSaveBeforeConfirm : ITempSaveDataService
    {
        private readonly IDistributedCache distributedCache;

        public RedisSaveBeforeConfirm(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        private async Task<bool> FindUser(string key)
        {
            try
            {
                var userStr = await this.distributedCache.GetStringAsync(key);
                bool isHas = string.IsNullOrEmpty(userStr) ? false : true;
                return isHas;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<CreateManagerDTO> GetManager(string key)
        {
            try
            {

                var isFind = await FindUser(key);
                if (!isFind) throw new Exception("user isn't exist");
                var userStr = await distributedCache.GetStringAsync(key);
                var userObj = JsonConvert.DeserializeObject<CreateManagerDTO>(userStr);
                return userObj;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task SaveManger(CreateManagerDTO createManagerDTO, string key)
        {
            try
            {
                await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(createManagerDTO), default);

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
