using Race_Day.Models;
using System.Net;
using System.Net.Http.Json;

namespace Race_Day3.Services
{
    public class RaceEventApiService
    {
        private readonly HttpClient _httpClient;

        public RaceEventApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var req = new { Email = email, Password = password };
            var res = await _httpClient.PostAsJsonAsync("api/auth/login", req);
            return res.IsSuccessStatusCode;
        }

        public async Task<User?> RegisterUserAsync(User user)
        {
            var res = await _httpClient.PostAsJsonAsync("api/auth/register", user);
            if (res.IsSuccessStatusCode)
                return await res.Content.ReadFromJsonAsync<User>();
            return null;
        }

        public async Task<List<User>> GetAllUsersAsync() =>
            await _httpClient.GetFromJsonAsync<List<User>>("api/users") ?? new();

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<User>($"api/users/{id}");

        public async Task<bool> UpdateUserAsync(int id, User user) =>
            (await _httpClient.PutAsJsonAsync($"api/users/{id}", user)).IsSuccessStatusCode;

        public async Task<bool> DeleteUserAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/users/{id}")).IsSuccessStatusCode;

       
        public async Task<List<Event>> GetAllEventsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Event>>("api/events") ?? new();

        public async Task<Event?> GetEventByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Event>($"api/events/{id}");

        public async Task<bool> CreateEventAsync(Event ev) =>
            (await _httpClient.PostAsJsonAsync("api/admin/events", ev)).IsSuccessStatusCode;

        public async Task<bool> UpdateEventAsync(int id, Event ev) =>
            (await _httpClient.PutAsJsonAsync($"api/admin/events/{id}", ev)).IsSuccessStatusCode;

        public async Task<bool> DeleteEventAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/admin/events/{id}")).IsSuccessStatusCode;

        public async Task<List<Enrolment>> GetAllEnrolmentsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Enrolment>>("api/enrolments") ?? new();

        public async Task<List<Enrolment>> GetMyEnrolmentsAsync(int userId) =>
            await _httpClient.GetFromJsonAsync<List<Enrolment>>($"api/enrolments/user/{userId}") ?? new();

        public async Task<bool> EnrolAsync(int userId, int eventId)
        {
            var req = new { UserId = userId, EventId = eventId };
            return (await _httpClient.PostAsJsonAsync("api/enrolments", req)).IsSuccessStatusCode;
        }

        public async Task<bool> CancelEnrolmentAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/enrolments/{id}")).IsSuccessStatusCode;

        public async Task<List<Feedback>> GetAllFeedbackAsync() =>
            await _httpClient.GetFromJsonAsync<List<Feedback>>("api/feedback") ?? new();

        public async Task<List<Feedback>> GetFeedbackForEventAsync(int eventId) =>
            await _httpClient.GetFromJsonAsync<List<Feedback>>($"api/feedback/event/{eventId}") ?? new();

        public async Task<bool> SubmitFeedbackAsync(int eventId, int userId, string comment, int rating)
        {
            var req = new { UserId = userId, Comment = comment, Rating = rating };
            return (await _httpClient.PostAsJsonAsync($"api/feedback/event/{eventId}", req)).IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFeedbackAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/feedback/{id}")).IsSuccessStatusCode;

        public async Task<List<Weather>> GetAllWeatherAsync() =>
            await _httpClient.GetFromJsonAsync<List<Weather>>("api/weather") ?? new();

        public async Task<bool> CreateWeatherAsync(Weather weather) =>
            (await _httpClient.PostAsJsonAsync("api/weather", weather)).IsSuccessStatusCode;

        public async Task<bool> DeleteWeatherAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/weather/{id}")).IsSuccessStatusCode;

        public async Task<List<Result>> GetAllResultsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Result>>("api/results") ?? new();

        public async Task<bool> CreateResultAsync(Result result) =>
            (await _httpClient.PostAsJsonAsync("api/results", result)).IsSuccessStatusCode;

        public async Task<bool> DeleteResultAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/results/{id}")).IsSuccessStatusCode;

        public async Task<List<Participant>> GetAllParticipantsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Participant>>("api/participants") ?? new();

        public async Task<bool> CreateParticipantAsync(Participant participant) =>
            (await _httpClient.PostAsJsonAsync("api/participants", participant)).IsSuccessStatusCode;

        public async Task<bool> DeleteParticipantAsync(int id) =>
            (await _httpClient.DeleteAsync($"api/participants/{id}")).IsSuccessStatusCode;
    }
}