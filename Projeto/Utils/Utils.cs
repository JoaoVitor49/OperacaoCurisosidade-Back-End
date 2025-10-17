using Projeto.Features.Client.Dtos;
using System.Text.RegularExpressions;

namespace Projeto.Utils
{
    public class Utils
    {
        public static IQueryable<ClientDashboardDTO> SortByQueryable(IQueryable<ClientDashboardDTO> clients, 
                                                         string sortBy, bool sortDescending)
        {
            var sortAscending = !sortDescending;
            return sortBy.ToLower() switch
            {
                "name" => sortAscending ? clients.OrderBy(c => c.Name)
                                        : clients.OrderByDescending(c => c.Name),
                "email" => sortAscending ? clients.OrderBy(c => c.Email) 
                                        : clients.OrderByDescending(c => c.Email),
                "registerdate" => sortAscending ? clients.OrderBy(c => c.RegisterDate) 
                                        : clients.OrderByDescending(c => c.RegisterDate),
                "isactive" => sortAscending ? clients.OrderBy(c => c.IsActive) 
                                        : clients.OrderByDescending(c => c.IsActive),
                _ => clients
            };
        }

        public static IQueryable<ClientDashboardDTO> SearchFilterQueryable(IQueryable<ClientDashboardDTO> clients, string searchTerm)
        {
            return clients.Where(c => c.Name.Contains(searchTerm) || c.Email.Contains(searchTerm));
        }

        public static List<string> ValidateClient<T>(T command)
        {
            string name = command.GetType().GetProperty("Name")?.GetValue(command)?.ToString() ?? string.Empty;
            int? age = (int?)(command.GetType().GetProperty("Age")?.GetValue(command));
            string email = command.GetType().GetProperty("Email")?.GetValue(command)?.ToString() ?? string.Empty;
            string adress = command.GetType().GetProperty("Adress")?.GetValue(command)?.ToString() ?? string.Empty;
            string others = command.GetType().GetProperty("Others")?.GetValue(command)?.ToString() ?? string.Empty;
            string interests = command.GetType().GetProperty("Interests")?.GetValue(command)?.ToString() ?? string.Empty;
            string feelings = command.GetType().GetProperty("Feelings")?.GetValue(command)?.ToString() ?? string.Empty;
            string values = command.GetType().GetProperty("Values")?.GetValue(command)?.ToString() ?? string.Empty;

            var errors = new List<string>();

            errors = ValidateName(name, errors);
            errors = ValidateAge(age, errors);
            errors = ValidateEmail(email, errors);
            errors = ValidateAdress(adress, errors);
            errors = ValidateOthers(others, errors);
            errors = ValidateInterests(interests, errors);
            errors = ValidateFeelings(feelings, errors);
            errors = ValidateValues(values, errors);

            return errors;
        }

        public static List<string> ValidateUser<T>(T command)
        {
            string name = command.GetType().GetProperty("Name")?.GetValue(command)?.ToString() ?? string.Empty;
            string email = command.GetType().GetProperty("Email")?.GetValue(command)?.ToString() ?? string.Empty;
            string password = command.GetType().GetProperty("Password")?.GetValue(command)?.ToString() ?? string.Empty;
            var errors = new List<string>();
            errors = ValidateName(name, errors);
            errors = ValidateEmail(email, errors);
            errors = ValidatePassword(password, errors);
            return errors;
        }

        public static List<string> ValidateLog<T>(T command)
        {
            string user = command.GetType().GetProperty("User")?.GetValue(command)?.ToString() ?? string.Empty;
            string action = command.GetType().GetProperty("Action")?.GetValue(command)?.ToString() ?? string.Empty;
            var errors = new List<string>();
            
            errors = ValidateEmail(user, errors);
            errors = ValidateAction(action, errors);
            return errors;
        }

        public static List<string> ValidateName(string name, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add("Name is required");
                return errors;
            }
            if (name.Length < 3)
            {
                errors.Add("Name must be at least 3 characters long");
            }
            if (name.Length > 50)
            {
                errors.Add("Name must be at most 50 characters long");
            }
            return errors;
        }

        public static List<string> ValidateAge(int? age, List<string> errors)
        {
            if (age.HasValue)
            {
                if (age < 18)
                {
                    errors.Add("Age must be at least 18");
                    return errors;
                }
                if (age > 100)
                {
                    errors.Add("Age must be at most 100");
                    return errors;
                }
            }   
            return errors;
        }

        public static List<string> ValidateEmail(string email, List<string> errors)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Email is required");
            }
            else if (!emailRegex.IsMatch(email))
            {
                errors.Add("Email is not in a valid format");
            }
            return errors;
        }

        public static List<string> ValidateAdress(string adress, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(adress))
            {
                return errors;
            }
            if (adress.Length < 5)
            {
                errors.Add("Adress must be at least 5 characters long");
            }
            if (adress.Length > 200)
            {
                errors.Add("Adress must be at most 100 characters long");
            }
            return errors;
        }

        public static List<string> ValidateOthers(string others, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(others))
            {
                return errors;
            }
            if (others.Length > 300)
            {
                errors.Add("Others must be at most 300 characters long");
            }
            return errors;
        }

        public static List<string> ValidateInterests(string interests, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(interests))
            {
                errors.Add("Interests are required");
                return errors;
            }
            if (interests.Length > 300)
            {
                errors.Add("Interests must be at most 300 characters long");
            }
            return errors;
        }

        public static List<string> ValidateFeelings(string feelings, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(feelings))
            {
                errors.Add("Feelings are required");
                return errors;
            }
            if (feelings.Length > 300)
            {
                errors.Add("Feelings must be at most 300 characters long");
            }
            return errors;
        }

        public static List<string> ValidateValues(string values, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(values))
            {
                errors.Add("Values are required");
                return errors;
            }
            if (values.Length > 300)
            {
                errors.Add("Values must be at most 300 characters long");
            }
            return errors;
        }

        public static List<string> ValidatePassword(string password, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add("Password is required");
                return errors;
            }
            if (password.Length < 6)
            {
                errors.Add("Password must be at least 6 characters long");
            }
            if (password.Length > 20)
            {
                errors.Add("Password must be at most 20 characters long");
            }
            return errors;
        }

        public static List<string> ValidateAction(string action, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                errors.Add("Action is required");
                return errors;
            }
            if( action.Length < 3)
            {
                errors.Add("Action must be at least 3 characters long");
            }
            if (action.Length > 100)
            {
                errors.Add("Action must be at most 100 characters long");
            }
            return errors;
        }
    }
}
