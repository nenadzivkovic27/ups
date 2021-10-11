using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using Ups.Nenad.DataTypes;

namespace Ups.Nenad.Services
{


    public class EmployeeRestService : IEmployeeDataService
    {
        protected RestClient _restClient;
        protected string _apiAddress;
        protected string _accessToken;

        JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public EmployeeRestService()
        {
            _apiAddress = ConfigurationManager.AppSettings["RestApiAddress"];
            _accessToken = ConfigurationManager.AppSettings["AccessToken"];
            _restClient = new(_apiAddress);
            _restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _accessToken));
        }

        public User GetEmployee(int userId)
        {
            User toRet = null;

            var request = new RestRequest("users/{userId}");
            request.AddUrlSegment("userId", userId);
            var response = _restClient.Get(request);
            var json = response.Content;

            var usersResponse = JsonSerializer.Deserialize<GetUsersResponse>(json, _options);
            toRet = usersResponse.Data.FirstOrDefault();

            return toRet;
        }

        public User AddEmployee(User user)
        {
            User toRet = user;

            var request = new RestRequest("users");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(user);
            var response = _restClient.Post(request);
            var json = response.Content;

            var usersResponse = JsonSerializer.Deserialize<GetUsersResponse>(json, _options);
            toRet = usersResponse.Data.FirstOrDefault();

            return toRet;
        }

        public void DeleteEmployee(User user)
        {
            DeleteEmployee(user.Id);
        }

        public void DeleteEmployee(int userId)
        {
            var request = new RestRequest("users/{userId}");
            request.AddUrlSegment("userId", userId);
            var response = _restClient.Delete(request);
            var json = response.Content;

            //TODO: check the response
        }

        public User EditEmployee(User user)
        {
            User toRet = user;

            var request = new RestRequest("users/{userId}");
            request.AddUrlSegment("userId", user.Id);
            request.AddJsonBody(user);
            var response = _restClient.Patch(request);
            var json = response.Content;
            var usersResponse = JsonSerializer.Deserialize<GetUserResponse>(json, _options);
            toRet = usersResponse.Data;

            return toRet;
        }


        public User SaveEmployee(User user)
        {
            if (user.Id > 0)
            {
                return EditEmployee(user);
            }
            else
            {
                return AddEmployee(user);
            }
        }

        public GetUsersResponse GetEmployees(int? page, string searchTerm = null)
        {

            var request = new RestRequest("users");
            if (page != null) request.AddParameter("page", page);
            if (searchTerm != null) request.AddParameter("first_name", searchTerm);
            var response = _restClient.Get(request);
           
            var json = response.Content;

            var usersResponse = JsonSerializer.Deserialize<GetUsersResponse>(json, _options);

            return usersResponse;
        }

    }
}
