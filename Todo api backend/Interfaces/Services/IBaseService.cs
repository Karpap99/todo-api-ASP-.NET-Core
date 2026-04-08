using Todo_api_backend.DTOs;

namespace Todo_api_backend.Interfaces.Services
{
    public interface IBaseService<TResponse, TCreateDto, TUpdateDto>
    {
        public Task<TResponse?> GetOneByID(Guid id);
        public Task<List<TResponse>> GetAllAsync();
        public Task<PaginatedResponse<TResponse>> GetPaginatedAsync(PaginationParams pagination);
        public Task<TResponse> AddAsync(TCreateDto createDto);
        public Task<TResponse> UpdateAsync(TUpdateDto updateDto);
        public Task DeleteAsync(Guid id);
    }
}
