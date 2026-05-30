using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<ClienteDTO> GetCliente(string ci);
        Task<List<ClienteDTO>> GetCliente();
        Task<List<ClienteDTO>> GetClienteBorrados();
        Task<ClienteDTO> PostCliente(string ci, string pwd);
        Task<ClienteDTO> PutCliente(string ci, string pwd, string nuevo_pwd);
        Task<ClienteDTO> DeleteCliente(string ci);
        Task<ClienteDTO?> HabilitarCliente(string ci);
        Task<bool> LoginCliente(string ci, string pwd);
    }
}