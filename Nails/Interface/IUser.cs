using Microsoft.AspNetCore.Mvc;
using Nails.ActionClass.Account;
using Nails.ActionClass.HelperClass.DTO;
using Nails.Models;

namespace Nails.Interface
{
    public interface IUser
    {
        ActionResult<List<string>> AddAccount(AccountCreate clientData);
        ActionResult<List<string>> DeleteClient(int id);
        ActionResult<IEnumerable<ClientDTO>> GetClient(int id);
        ActionResult<List<string>> UpdateClient(int id, ClientDTO client);

        public class ClientClass : IUser
        {
            private readonly НогтиContext _context;
            public ClientClass(НогтиContext context)
            {
                _context = context;
            }

            public List<string> AddAccount(AccountCreate account)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(account.Name))
                        return new List<string> { "Поле с именем не заполнено" };
                    if (account.Name.Length > 50)
                        return new List<string> { "Имя не может содержать больше 50 символов" };

                    if (string.IsNullOrWhiteSpace(account.Phone))
                        return new List<string> { "Поле с номером телефона не заполнено" };
                    if (account.Phone.Length > 11)
                        return new List<string> { "Номер телефона не может быть меньше или больше 11 символов" };

                    if (string.IsNullOrWhiteSpace(account.Surname))
                        return new List<string> { "Поле с фамилией не заполнено" };
                    if (account.Surname.Length > 50)
                        return new List<string> { "Фамилия не может содержать больше 50 символов" };

                    if (string.IsNullOrWhiteSpace(account.Email))
                        return new List<string> { "Поле с почтой не заполнено" };
                    if (account.Email.Length > 50)
                        return new List<string> { "Почта не может содержать больше 50 символов" };

                    Client createClient = new Client()
                    {
                        Name = account.Name,
                        Phone = account.Phone,
                        Surname = account.Surname,
                        Email = account.Email
                    };

                    _context.Clients.Add(createClient);
                    _context.SaveChanges();

                    int ClientId = createClient.ClientId;

                    Results.Created();
                    return [$"Пользователь успешно создан, ID - {ClientId}"];
                }
                catch (Exception)
                {
                    Results.BadRequest(new List<string> { "Ошибка в выполнении запроса" });
                    throw;
                }
            }

            public List<string> DeleteClient(long id)
            {
                try
                {
                    var Client = _context.Clients.Find(id);

                    if (Client == null)
                    {
                        Results.NotFound(new List<string> { "Не удалось найти пользователя" });
                    }

                    var Order = _context.Orders.Where(order => order.ClientId == id).ToList();

                    if (Order.Any())
                    {
                        _context.RemoveRange(Order);
                        _context.SaveChanges();
                    }

                    _context.Clients.Remove(Client);
                    _context.SaveChanges();

                    Results.NoContent();
                    return ["Пользователь успешно удален!"];
                }
                catch (Exception)
                {
                    Results.BadRequest(new List<string> { "Ошибка в выполнении запроса" });
                    throw;
                }
            }

            public ActionResult<List<string>> DeleteClient(int id)
            {
                throw new NotImplementedException();
            }

            public List<ClientDTO> GetClient(int id)
            {
                try
                {
                    var client = _context.Clients.Find(id);
                    if (client == null)
                    {
                        Results.NotFound(new List<string> { "Пользователь с таким номером телефона не найден" });
                    }

                    var data = _context.Clients.Where(ph => ph.ClientId == id).Select(
                    x => new ClientDTO()
                    {
                        ClientId = x.ClientId,
                        Name = x.Name,
                        Phone = x.Phone,
                        Email = x.Email,
                        Surname = x.Surname
                    }
                        ).ToList();
                    return (List<ClientDTO>)data;
                }
                catch (Exception)
                {
                    Results.BadRequest();
                    throw;
                }
            }

            public List<string> UpdateClient(int id, ClientDTO client)
            {
                try
                {
                    var userData = _context.Clients.FirstOrDefault(x => x.ClientId == id);

                    if (userData == null)
                    {
                        Results.NoContent();
                        return [];
                    }

                    userData.Name = client.Name;
                    userData.Phone = client.Phone;
                    userData.Surname = client.Surname;
                    userData.Email = client.Email;

                    _context.Clients.Update(userData);
                    _context.SaveChanges();

                    Results.Ok();
                    return ["Данные пользователя успесно обновлены!"];
                }
                catch
                {
                    Results.BadRequest();
                    throw;
                }
            }
            ActionResult<List<string>> IUser.AddAccount(AccountCreate clientData)
            {
                throw new NotImplementedException();
            }

            ActionResult<IEnumerable<ClientDTO>> IUser.GetClient(int id)
            {
                throw new NotImplementedException();
            }

            ActionResult<List<string>> IUser.UpdateClient(int id, ClientDTO client)
            {
                throw new NotImplementedException();
            }
        }
    }
}
