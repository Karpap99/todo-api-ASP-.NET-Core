using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.UserDtos;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserResponseDTO?> GetOneByID(Guid id)
        {
            var user = await _repo.GetOneByID(id);
            return new UserResponseDTO(user);
        }

        public async Task<List<UserResponseDTO>> GetAllAsync() {
            var users = await _repo.GetAllAsync();
            return [.. users.Select(u => new UserResponseDTO(u))];
        }

        public async Task<UserResponseDTO?> GetByEmailAsync(string email) {
            var user = await _repo.GetByEmailAsync(email);
            return new UserResponseDTO(user);
        }

        public async Task<PaginatedResponse<UserResponseDTO>> GetPaginatedAsync(PaginationParams paginated) {
            var totalEntries = await _repo.GetTotalCountAsync();
            var users = await _repo.GetPaginatedAsync(paginated, x => x.Id);

            return new PaginatedResponse<UserResponseDTO>
            {
                Items = [.. users.Select(u => new UserResponseDTO(u))],
                TotalItems = totalEntries,
                TotalPages = (int)Math.Ceiling((double)totalEntries / paginated.Limit)
            };
        }

        public async Task<UserResponseDTO> AddAsync(CreateUserDTO createUserDTO) {
            var existingUser = await _repo.GetByEmailAsync(createUserDTO.Email);

            if (existingUser != null)
             throw new InvalidOperationException("user exist");

            var user = new User
            {
                Email = createUserDTO.Email,
                PasswordHash = createUserDTO.Password
            };
            
            var createdUser = await _repo.AddAsync(user);

            return new UserResponseDTO(createdUser);
        }

        public async Task<UserResponseDTO> UpdateAsync(UpdateUserDTO updateUserDTO) {
            var user = await _repo.GetByEmailAsync(updateUserDTO.Email);

            return new UserResponseDTO(user);

        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);
    }
}
