using Todo_api_backend.DTOs;

namespace Todo_api_backend.Interfaces.Services
{
    public interface IBaseProtectedService<TResponse, TCreateDto, TUpdateDto>
    {
        public Task<TResponse> GetOneByID(Guid id, Guid userId);
        public Task<List<TResponse>> GetAllAsync(Guid userId);
        public Task<PaginatedResponse<TResponse>> GetPaginatedAsync(PaginationParams pagination, Guid userId);
        public Task<TResponse> AddAsync(TCreateDto createDto, Guid userId);
        public Task<TResponse> UpdateAsync(TUpdateDto updateDto, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
