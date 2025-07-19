

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Application.Dtos.insurance.NetworkType;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;
using SGM.Persistence.Context;
using System.Linq.Expressions;

namespace SGM.Persistence.Repositories
{
    public class NetworkTypeRepository : INetworkTypeRepository
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NetworkTypeRepository> _logger;
        private readonly HealtSyncContext _context;

        public NetworkTypeRepository(IConfiguration configuration,
                                     ILogger<NetworkTypeRepository> logger, 
                                     HealtSyncContext context)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:HealtSyncConnection"];
            _logger = logger;
            _context = context;
        }

        public async Task<OperationResult> AddAsync(CreateNetworkTypeDto createNetworkTypeDto)
        {
            OperationResult presult = new OperationResult();
            try
            {
                _logger.LogInformation("Adding a new network type with name: {Name}", createNetworkTypeDto.Name);

                //validaciones de campos

                if (createNetworkTypeDto is null)
                {
                    presult.IsSuccess = false;
                    presult.Message = "CreateNetworkTypeDto cannot be null.";
                    return presult;
                }

                if (string.IsNullOrEmpty(createNetworkTypeDto.Name))
                {
                    presult.IsSuccess = false;
                    presult.Message = "Name cannot be null or empty.";
                    return presult;
                }

                if(createNetworkTypeDto.Name.Length > 50)
                {
                    presult.IsSuccess = false;
                    presult.Message = "Name cannot exceed 50 characters.";
                    return presult;
                }

                if (string.IsNullOrEmpty(createNetworkTypeDto.Description))
                {
                    presult.IsSuccess = false;
                    presult.Message = "Description cannot be null or empty.";
                    return presult;
                }


                if (createNetworkTypeDto.Description.Length > 255)
                {
                    presult.IsSuccess = false;
                    presult.Message = "Description cannot exceed 255 characters.";
                    return presult;
                }

                using (var context = new SqlConnection(_connectionString))
                {
                   using (var command = new SqlCommand("insurance.CreateNetworkType", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", createNetworkTypeDto.Name);
                        command.Parameters.AddWithValue("@Description", createNetworkTypeDto.Description);
                        command.Parameters.AddWithValue("@CreateAt", createNetworkTypeDto.CreateAt);


                        SqlParameter p_result = new SqlParameter("@Presult", System.Data.SqlDbType.VarChar)
                        {
                            Size = 1000,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        command.Parameters.Add(p_result);

                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();


                        if (result > 0)
                        {
                            presult.IsSuccess = true;
                            presult.Message = "Network type added successfully.";
                            _logger.LogInformation("Network type added successfully with result: {Result}", result);

                        }
                        else
                        {
                            presult.IsSuccess = false;
                            presult.Message = "Failed to add network type.";
                            _logger.LogError("Failed to add network type. No rows affected.");

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                presult.IsSuccess = false;
                presult.Message = $"An error occurred while adding the network type: {ex.Message}";
                _logger.LogError(ex, "An error occurred while adding the network type: {Message}", ex.Message);
            }
           
            return presult;
        }

        public Task<OperationResult> DeleteAsync(DisableNetworkTypeDto disableNetworkTypeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetAllAsync()
        {
            OperationResult presult = new OperationResult();    
            try
            {

                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    using (SqlCommand command = new SqlCommand("insurance.GetNetworkTypes", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<GetNetworkTypeDto> networkTypes = new List<GetNetworkTypeDto>();
                            while (await reader.ReadAsync())
                            {
                                GetNetworkTypeDto networkType = new GetNetworkTypeDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                };

                                networkTypes.Add(networkType);
                            }
                            presult.IsSuccess = true;
                            presult.Data = networkTypes;
                            presult.Message = "Network types retrieved successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                presult.IsSuccess = false;
                presult.Message = $"An error occurred while retrieving network types: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving network types: {Message}", ex.Message);
            }
            return presult;
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            OperationResult presult = null;
            try
            {
                var networkType = await _context.NetworkTypes.FindAsync(id);

                if (networkType is null)
                {
                    presult = new OperationResult
                    {
                        IsSuccess = false,
                        Message = "Network type not found."
                    };
                    _logger.LogWarning("Network type with ID {Id} not found.", id);
                    return presult;
                }


                return OperationResult.Success("Network type retrieved successfully.", new GetNetworkTypeDto
                {
                    Id = networkType.NetworkTypeId,
                    Name = networkType.Name,
                    Description = networkType.Description,
                    CreationDate = networkType.CreatedAt
                });

                
            }
            catch (Exception ex)
            {

                presult.IsSuccess = false;
                presult.Message = $"An error occurred while retrieving network types: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving network types: {Message}", ex.Message);

            }
            return presult;
        }

        public async Task<OperationResult> UpdateAsync(ModifyNetworkTypeDto modifyNetworkTypeDto)
        {
            OperationResult presult = new OperationResult();
            try
            {
                _logger.LogInformation("updating a new network type with name: {Name}", modifyNetworkTypeDto.Name);

                //validaciones de campos

                if (modifyNetworkTypeDto is null)
                {
                    presult.IsSuccess = false;
                    presult.Message = "CreateNetworkTypeDto cannot be null.";
                    return presult;
                }

                if (string.IsNullOrEmpty(modifyNetworkTypeDto.Name))
                {

                    presult.IsSuccess = false;
                    presult.Message = "Name cannot be null or empty.";
                    return presult;
                }

                if (modifyNetworkTypeDto.Name.Length > 50)
                {
                    presult.IsSuccess = false;
                    presult.Message = "Name cannot exceed 50 characters.";
                    return presult;
                }

                if (string.IsNullOrEmpty(modifyNetworkTypeDto.Description))
                {
                    presult.IsSuccess = false;
                    presult.Message = "Description cannot be null or empty.";
                    return presult;
                }


                if (modifyNetworkTypeDto.Description.Length > 255)
                {
                    presult.IsSuccess = false;
                    presult.Message = "Description cannot exceed 255 characters.";
                    return presult;
                }

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("insurance.ModidyNetworkType", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NetworkTypeId", modifyNetworkTypeDto.Id);
                        command.Parameters.AddWithValue("@Name", modifyNetworkTypeDto.Name);
                        command.Parameters.AddWithValue("@Description", modifyNetworkTypeDto.Description);
                        command.Parameters.AddWithValue("@UpdateAt", modifyNetworkTypeDto.UpdateAt);

                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();


                        if (result > 0)
                        {
                            presult.IsSuccess = true;
                            presult.Message = "Network type modified successfully.";
                            _logger.LogInformation("Network type modified successfully with result: {Result}", result);

                        }
                        else
                        {
                            presult.IsSuccess = false;
                            presult.Message = "Failed to add network type.";
                            _logger.LogError("Failed to add network type. No rows affected.");

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                presult.IsSuccess = false;
                presult.Message = $"An error occurred while adding the network type: {ex.Message}";
                _logger.LogError(ex, "An error occurred while adding the network type: {Message}", ex.Message);
            }
            return presult;
        }
    }

}
